﻿using System;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using HomeWork.Views;
using System.DirectoryServices;
using ScheduleLib.Schedule;
using ScheduleLib.Linq;
using System.Collections.Generic;

namespace HomeWork
{
    public partial class MainForm : Form
    {
        private readonly UserData userData;
        private readonly FontFamily fontFamily = FontFamily.GenericSansSerif;
        private readonly IList<Event> holidays;
        private float scroll = 0;
        private int month;
        private DateTime monthDate;
        private List<int>[]? displayingSchedules;
        private DateTime? detailDate = null;
        private readonly Authorizer authorizer;
        public MainForm()
        {
            InitializeComponent();
            /*var service = new CalendarService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyACDT2kmVWYcSNd_yS2xKSBXj71EwA5iNw",
            });
            //負荷と金がかかるので無効化中
            holidays = service.Events.List("ja.japanese#holiday@group.v.calendar.google.com").Execute().Items;*/
            holidays = [];
            (Font, FontFamily) t = LoadFontFromFile(@"Resources/Font/NotoSansJP-VariableFont_wght.ttf", 8);
            Font = t.Item1;
            fontFamily = t.Item2;
            menuStrip1.Font = Font;
            var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var t2 = LoadFile(Path.Combine(home, "HWCalendar", "schedules.json"));
            userData = t2.Item1;
            authorizer = t2.Item2;
        }


        private static (Font, FontFamily) LoadFontFromFile(string fontFilePath, float size)
        {

            using PrivateFontCollection privateFontCollection = new();
            {

                privateFontCollection.AddFontFile(fontFilePath);
                FontFamily fontFamily = privateFontCollection.Families[0];

                // フォントを作成して返す
                return (new Font(fontFamily, size), fontFamily);
            }
        }
        private static (UserData, Authorizer) LoadFile(string path)
        {
            UserData? schedules = UserData.LoadJson(path);
            return schedules != null ? (schedules, Authorizer.Create(schedules)) : (new UserData(), Authorizer.Create());
        }
        private static Event? SearchEvent(IList<Event> events, DateTime date)
        {
            foreach (var event1 in events)
            {
                if (event1.Start.Date == date.Date.ToString("yyyy-MM-dd")) return event1;
            }
            return null;
        }

        public void Form1_Load_1(object sender, EventArgs e)
        {
            DrawCalendar();
            detailed.HorizontalScroll.Enabled = false;
            addSchedule.DropDownStyle = ComboBoxStyle.DropDownList;
            addSchedule.SelectedIndex = 0;
            calendarGroup.MouseWheel += Calendar_MouseWheel;

        }
        private void AddTab(Schedule schedule)
        {
            TableContentsPanel tableLayoutPanel = new() { Location = new(0, 30), AutoSize = true, AutoSizeMode = AutoSizeMode.GrowOnly };
            TabPage tabPage = new() { Text = schedule.Title, AutoScroll = true };
            tabs.TabPages.Insert(0, tabPage);
            tabs.SelectedTab = tabPage;
            tabPage.HorizontalScroll.Enabled = false;
            Button button = new() { Text = "閉じる", AutoSize = true };
            button.Click += (sender, e) => { tabPage.Dispose(); tabs.TabPages.Remove(tabPage); };

            tabPage.Controls.Add(button);
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Absolute, tabPage.Width * 0.3f));
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Absolute, tabPage.Width * 0.7f - 25));
            tabPage.Controls.Add(tableLayoutPanel);
            tableLayoutPanel.AddTextRow("タイトル", schedule.Title);
            switch (schedule.ScheduleType)
            {
                case ScheduleType.Homework:
                    tableLayoutPanel.AddTextRow("配布日", schedule.Start.ToString("f"));
                    tableLayoutPanel.AddTextRow("提出日", schedule.End.ToString("f"));
                    if (schedule.Description != null) tableLayoutPanel.AddTextRow("メモ", schedule.Description);
                    {
                        Subject? subj = userData.LoadSubject(schedule.Subject);
                        tableLayoutPanel.AddLinkRow("教科", subj != null ? subj.Name : "不明", (sender,e) => AddSearchedList(SearchType.Subject, subj));
                    }
                    FlowLayoutPanel accordion = new()
                    {
                        FlowDirection = FlowDirection.TopDown,
                        Width = tableLayoutPanel.ContWidth,
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowOnly,
                        WrapContents = false
                    };
                    tableLayoutPanel.AddCustomRow("提出物", accordion);
                    foreach (Submission submission in schedule.Detail ?? Enumerable.Empty<Submission>())
                    {
                        TableContentsPanel table = new() { AutoSize = true };
                        accordion.Controls.Add(new TitledPanel(table)
                        {
                            Text = submission.Name,
                            Margin = new(0),
                            MinimumSize = new(tableLayoutPanel.ContWidth, 25),
                            AutoSizeMode = AutoSizeMode.GrowOnly,
                            AutoSize = true,
                        });
                        table.ColumnStyles.Add(new(SizeType.Absolute, tableLayoutPanel.ContWidth * 0.4f));
                        table.ColumnStyles.Add(new(SizeType.Absolute, tableLayoutPanel.ContWidth * 0.6f));

                        table.AddTextRow("タイトル", submission.Name);
                        if (submission.Description != null) table.AddTextRow("説明", submission.Description);
                        switch (submission.Category)
                        {
                            case SubmissionCategory.Regular:
                                Note? note = userData.LoadRegular(submission.Id);
                                if (note == null)
                                {
                                    table.AddTextRow("提出物", $"[{submission.Id}が見つかりませんでした。\nこのIDのノートが存在するか確認してください。]");
                                }
                                else
                                {
                                    table.AddLinkRow("提出物", note.Name ?? "[提出物を読み込めませんでした]", (sender, e) => { Debug.WriteLine("クリックされました"); });
                                }
                                FlowLayoutPanel pages = new() { FlowDirection = FlowDirection.TopDown, AutoSize = true, Margin = new(0) };
                                if (submission.Pages != null) pages.Controls.AddRange(submission.Pages.Select(o => new Label() { Text = o }).ToArray());

                                table.AddCustomRow("ページ", pages);
                                break;
                            case SubmissionCategory.Irregular:

                                break;
                            case SubmissionCategory.Fix:
                                FlowLayoutPanel links = new() { FlowDirection = FlowDirection.TopDown, AutoSize = true, Margin = new(0) };
                                if(submission.Pages != null) 
                                    links.Controls.AddRange(submission.Pages.Select(o =>
                                    {
                                        Schedule? schj;
                                        LinkLabel linkSubm = new();
                                        if ((schj = userData.Schedules.GetSchedule(o)) != null) {
                                            linkSubm.Text = schj.Title;
                                            linkSubm.Click += (sender,e)=> AddTab(schj);
                                        }
                                        return linkSubm;
                                    }).ToArray());
                                table.AddCustomRow("リンク先", links);
                                break;
                            default:
                                break;
                        }
                        table.AddTextRow("丸付け", submission.Circling ? "あり" : "なし");
                        table.AddTextRow("公開レベル", ScdLevel.GetJapaneseString(submission.Share));
                    }
                    break;

                case ScheduleType.LongEvent:
                case ScheduleType.ShortEvent:
                    tableLayoutPanel.AddTextRow("開始日", schedule.Start.ToString("f"));
                    tableLayoutPanel.AddTextRow("終了日", schedule.End.ToString("f"));
                    if (schedule.Description != null) tableLayoutPanel.AddTextRow("メモ", schedule.Description);
                    break;

            }
            tableLayoutPanel.AddTextRow("提供:", $"{schedule.Provided:G}頃\nby{(schedule.Provider == userData.Properties.User.Id ? "自分" : schedule.Provider)}");

        }
        private enum SearchType
        {
            Subject
        }
        private void AddSearchedList(SearchType type,ScheduleObject? schedule)
        {
            TabPage tab = new();
            FlowLayoutPanel flowLayout = new() { FlowDirection = FlowDirection.TopDown };
            tab.Controls.Add(flowLayout);
            tabs.Controls.Add(tab);
            flowLayout.Controls.Add(new Button() { Text = "閉じる", AutoSize = true, Margin = new(0) });
            ListView list = new();
            list.Columns.Add(new ColumnHeader() { Text = "名前", Width = 200 });
            list.Columns.Add(new ColumnHeader() { Text = "開始日", Width = 100 });
            list.Columns.Add(new ColumnHeader() { Text = "終了日", Width = 100 });
            list.Columns.Add(new ColumnHeader() { Text = "ID", Width = 120 });
            flowLayout.Controls.Add(list);
            switch (type)
            {
                
                case SearchType.Subject:
                    AddSchedulesOnSubject(((Subject?)schedule)?.Id??string.Empty, list);
                    break;
            }
        }
        //目盛りの部分
        private void DetailTime_Paint(object sender, PaintEventArgs e)
        {
            using Graphics g = e.Graphics;
            {
                using Pen pen = new(Color.Black, 1);
                {
                    for (int i = 0; i < 25; i++)
                    {
                        float x = detailed.Margin.Left + (detailed.Width - detailed.Margin.Left - detailed.Margin.Right) / 24f * i;
                        g.DrawLine(pen, x, detailTime.Height - (i % 2 == 0 ? 7 : 5), x, detailTime.Height);
                        if (i % 2 == 0) { g.DrawString(i.ToString(), new Font(fontFamily, 6), pen.Brush, x / 1.007f - 5, detailTime.Height - 21); }
                    }
                }
            }
        }

        private void AddSchedule(object sender, EventArgs e)
        {
            FlowLayoutPanel parent = new() { FlowDirection = FlowDirection.TopDown, WrapContents = false, Margin = new(0), AutoScroll = true };
            TableContentsPanel tableLayoutPanel = new() { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowOnly };
            TabPage tabPage = new() { Text = "新しい予定" };
            tabPage.Controls.Add(parent);
            tabs.TabPages.Insert(0, tabPage);
            parent.Size = tabPage.Size;
            tabs.SelectedTab = tabPage;
            Button button = new() { Text = "閉じる", AutoSize = true };
            parent.HorizontalScroll.Enabled = false;
            parent.HorizontalScroll.Visible = false;
            button.Click += (sender, e) => { tabPage.Dispose(); tabs.TabPages.Remove(tabPage); };
            Schedule schedule = new();
            parent.Controls.Add(button);
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Absolute, tabPage.Width * 0.3f));
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Absolute, tabPage.Width * 0.7f - 27));
            parent.Controls.Add(tableLayoutPanel);
            ComboBox scheduleBox = new() { DropDownStyle = ComboBoxStyle.DropDownList };
            scheduleBox.Items.AddRange(ScdLevel.GetEnumValues<ScheduleType>());
            tableLayoutPanel.AddCustomRow("種類", scheduleBox, true);
            schedule.Title = "新しい予定";
            tableLayoutPanel.AddTextInput("タイトル", "新しい予定", eventHandler: (sender, e) => schedule.Title = schedule.Title = ((TextBox?)sender)?.Text ?? string.Empty);
            tableLayoutPanel.AddTextInput("説明", field: true, eventHandler: (sender, e) => schedule.Description = ((TextBox?)sender)?.Text);
            ComboBox subjBox = new() { DropDownStyle = ComboBoxStyle.DropDownList };
            subjBox.Items.AddRange(userData.Subjects);
            subjBox.SelectedValueChanged += (sender, e) => schedule.Subject = (((Subject?)subjBox.SelectedItem) ?? new Subject() { Name = "未指定", Id = "unknown" }).Id;
            subjBox.SelectedIndex = 0;
            tableLayoutPanel.AddCustomRow("教科", subjBox, out Label subjLabel, true);
            Control selected = new() { Width = 10, Height = 10, BackColor = Color.BlueViolet };
            tableLayoutPanel.AddCustomRow("色", selected);
            Button colorButton = new() { Text = "編集", Height = 30 };
            schedule.Color = Color.BlueViolet;
            colorButton.Click += (sender, e) =>
            {
                DialogResult result = colorPicker.ShowDialog();
                if (result == DialogResult.OK)
                {
                    schedule.Color = colorPicker.Color;
                    selected.BackColor = colorPicker.Color;
                }
            };
            tableLayoutPanel.AddCustomRow("色の変更", colorButton);

            DateTimePicker startDT = new()
            {
                Format = DateTimePickerFormat.Short,
                CustomFormat = "yyyy/MM/dd HH:mm:ss",
                Width = tableLayoutPanel.ContWidth - 20,
            };

            FlowLayoutPanel panel = new() { AutoSize = true };
            panel.Controls.Add(startDT);
            CheckBox checkbox1 = new() { Text = "終日", Checked = true };
            checkbox1.CheckedChanged += (sender, e) => startDT.Format = checkbox1.Checked ? DateTimePickerFormat.Short : DateTimePickerFormat.Custom;
            startDT.ValueChanged += (sender, e) => schedule.Start = startDT.Value;
            startDT.Value = detailDate ?? DateTime.Now.Date;
            panel.Controls.Add(checkbox1);
            tableLayoutPanel.AddCustomRow("開始時刻", panel, out Label startLabel, true);
            DateTimePicker finishDT = new()
            {
                Format = DateTimePickerFormat.Short,
                CustomFormat = "yyyy/MM/dd HH:mm:ss",
                Width = tableLayoutPanel.ContWidth - 20,
            };


            FlowLayoutPanel panel1 = new() { AutoSize = true };
            panel1.Controls.Add(finishDT);
            finishDT.ValueChanged += (sender, e) => schedule.End = finishDT.Value;
            finishDT.Value = detailDate ?? DateTime.Now.Date;
            CheckBox checkbox2 = new() { Text = "終日", Checked = true };
            checkbox2.CheckedChanged += (sender, e) => finishDT.Format = checkbox2.Checked ? DateTimePickerFormat.Short : DateTimePickerFormat.Custom;
            panel1.Controls.Add(checkbox2);
            tableLayoutPanel.AddCustomRow("終了時刻", panel1, out Label finishLabel, fit: true);

            FlowLayoutPanel submissionsView = new() { AutoSize = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };
            parent.Controls.Add(submissionsView);
            AddSubmission(subjBox, submissionsView, tabPage.Width, schedule);
            Button newSubmission = new() { Text = "新しい提出物", AutoSize = true };
            newSubmission.Click += (sender, e) => AddSubmission(subjBox, submissionsView, tabPage.Width, schedule);
            parent.Controls.Add(newSubmission);
            Button submitSchedule = new() { Text = "確定してカレンダーに追加", AutoSize = true };
            Label errorlabel = new() { AutoSize = true, ForeColor = Color.Red };
            submitSchedule.Click += (sender, e) =>
            {
                string? error = Schedule.CheckCorrect(schedule);
                if (error == null)
                {
                    tabPage.Dispose();
                    tabs.TabPages.Remove(tabPage);
                    userData.Schedules.Add(schedule);
                    Schedule.Finalize(schedule, authorizer);
                    userData.Save();
                    SetCalendar();
                }
                else
                {
                    errorlabel.Text = error;
                }
            };
            parent.Controls.Add(submitSchedule);
            parent.Controls.Add(errorlabel);
            scheduleBox.SelectedValueChanged += (sender, e) =>
            {
                switch (schedule.ScheduleType = (ScheduleLevel<ScheduleType>?)scheduleBox.SelectedItem)
                {
                    case ScheduleType.Homework:
                        subjLabel.Visible = subjBox.Visible = submissionsView.Visible = true;
                        finishLabel.Visible = panel1.Visible = true;
                        checkbox2.Visible = checkbox1.Visible = false;
                        startLabel.Text = "配布日";
                        finishLabel.Text = "提出日";
                        break;

                    case ScheduleType.LongEvent:
                        subjLabel.Visible = subjBox.Visible = submissionsView.Visible = false;
                        finishLabel.Visible = panel1.Visible = true;
                        checkbox2.Visible = checkbox1.Visible = true;
                        startLabel.Text = "開始時刻";
                        finishLabel.Text = "終了時刻";
                        break;

                    case ScheduleType.ShortEvent:
                        subjLabel.Visible = subjBox.Visible = submissionsView.Visible = false;
                        finishLabel.Visible = panel1.Visible = false;
                        checkbox1.Visible = false;
                        startLabel.Text = "開始日";
                        break;
                }

            };
            scheduleBox.SelectedIndex = addSchedule.SelectedIndex;
        }

        private void AddSubmission(ComboBox subjBox, FlowLayoutPanel panel, int width, Schedule schedule)
        {
            Submission submission = new();
            schedule.Detail = [];
            schedule.Detail.Add(submission);
            TableContentsPanel tableLayoutPanel = new() { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowOnly, Margin = new(0) };
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Absolute, width * 0.3f));
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Absolute, width * 0.7f - 35));

            TitledPanel titledPanel = new(tableLayoutPanel)
            {
                AutoSizeMode = AutoSizeMode.GrowOnly,
                AutoSize = true,
                MinimumSize = new(width - 35, 25),
                MaximumSize = new(width - 35, 0),
                Text = "提出物",
            };
            panel.Controls.Add(titledPanel);
            Button remthis = new() { Text = "削除", Height = 30 };
            remthis.Click += (sender, e) =>
            {
                titledPanel.Dispose();
                panel.Controls.Remove(titledPanel);
                schedule.Detail.Remove(submission);
                GC.Collect();
            };
            tableLayoutPanel.AddCustomRow("この提出物を", remthis, fit: true);
            ComboBox share = new() { DropDownStyle = ComboBoxStyle.DropDownList };
            share.Items.AddRange(ScdLevel.GetEnumValues<ShareLevel>());
            share.SelectedValueChanged += (sender, e) => submission.Share = (ScheduleLevel<ShareLevel>?)share.SelectedItem;
            share.SelectedIndex = 0;
            tableLayoutPanel.AddCustomRow("公開", share, fit: true);
            tableLayoutPanel.AddTextInput("タイトル", "提出物", eventHandler: new((sender, e) => submission.Name = ((TextBox?)sender)?.Text ?? "提出物"));
            FlowLayoutPanel flowLayoutPanel = new() { AutoSize = true, Margin = new(0) };
            ComboBox box = new() { DropDownStyle = ComboBoxStyle.DropDownList, Width = tableLayoutPanel.ContWidth - 5 };
            box.Items.AddRange(ScdLevel.GetEnumValues<SubmissionCategory>());
            box.SelectedIndex = 0;
            flowLayoutPanel.Controls.Add(box);
            CheckBox checkBox = new()
            {
                Text = "丸付け",
                Checked = true
            };
            checkBox.CheckedChanged += (sender, e) => submission.Circling = checkBox.Checked;

            flowLayoutPanel.Controls.Add(checkBox);
            tableLayoutPanel.AddCustomRow("種別", flowLayoutPanel, fit: true);
            submission.Category = SubmissionCategory.Regular;
            EventHandler? handler = AddRegular(schedule, submission, tableLayoutPanel);
            subjBox.SelectedValueChanged += handler;
            box.SelectedValueChanged += (sender, e) =>
            {
                ResetTable(tableLayoutPanel, 4);
                subjBox.SelectedIndexChanged -= handler;
                switch (submission.Category = (ScheduleLevel<SubmissionCategory>?)box.SelectedItem)
                {
                    case SubmissionCategory.Regular:
                        handler = AddRegular(schedule, submission, tableLayoutPanel);
                        break;

                    case SubmissionCategory.Irregular:
                        handler = AddIrregular(submission, tableLayoutPanel);
                        break;

                    case SubmissionCategory.Fix:
                        handler = AddFix(schedule, submission, tableLayoutPanel);
                        break;
                }
                subjBox.SelectedIndexChanged += handler;
            };
        }
        private static void ResetTable(TableLayoutPanel tableLayoutPanel, int startRowIndex)
        {

            if (startRowIndex < 0 || startRowIndex >= tableLayoutPanel.RowCount)
            {
                // 無効な行インデックス
                return;
            }

            // 削除対象の行のコントロールをクリア
            for (int i = tableLayoutPanel.RowCount - 1; i >= startRowIndex; i--)
            {
                foreach (Control control in tableLayoutPanel.Controls.OfType<Control>().Where(c => tableLayoutPanel.GetRow(c) == i).ToList())
                {
                    tableLayoutPanel.Controls.Remove(control);
                    control.Dispose(); // 必要に応じて
                }
            }
        }

        private EventHandler AddRegular(Schedule schedule, Submission submission, TableContentsPanel tableLayoutPanel)
        {
            submission.Description = null;
            var startPage = new TrackBar() { Width = tableLayoutPanel.ContWidth };
            var endPage = new TrackBar() { Width = tableLayoutPanel.ContWidth };
            ComboBox submiBox = new() { DropDownStyle = ComboBoxStyle.DropDownList };
            submiBox.Items.AddRange(userData.SearchRegular(schedule.Subject));
            submiBox.SelectedValueChanged += (sender, e) =>
            {
                Note? note = (Note?)submiBox.SelectedItem;
                startPage.Maximum = (note?.Pages) ?? 100;
                endPage.Maximum = (note?.Pages) ?? 100;
                submission.Id = note?.Id ?? string.Empty;
            };

            submiBox.SelectedIndex = 0;
            startPage.ValueChanged += (sender, e) => { if (startPage.Value > endPage.Value) endPage.Value = startPage.Value; toolTip.SetToolTip(startPage, startPage.Value.ToString()); };
            endPage.ValueChanged += (sender, e) => { if (startPage.Value > endPage.Value) startPage.Value = endPage.Value; toolTip.SetToolTip(endPage, endPage.Value.ToString()); };
            toolTip.SetToolTip(startPage, startPage.Value.ToString());
            toolTip.SetToolTip(endPage, endPage.Value.ToString());
            tableLayoutPanel.AddCustomRow("提出物", submiBox, fit: true);
            EventHandler hander = new((sender, e) =>
            {
                submiBox.Items.Clear();
                submiBox.Items.AddRange(userData.SearchRegular(schedule.Subject));
                submiBox.SelectedIndex = 0;
            });
            {
                var flow = new FlowLayoutPanel() { FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoSize = true };
                tableLayoutPanel.AddCustomRow("ページ", flow, fit: true);
                flow.Controls.Add(startPage);
                flow.Controls.Add(endPage);
                Button addButton = new() { Text = "追加する", AutoSize = true };
                Button remButton = new() { Text = "削除する", AutoSize = true };
                flow.Controls.Add(addButton);
                flow.Controls.Add(remButton);
                submission.Pages = [];
                ListBox list = new() { Width = tableLayoutPanel.ContWidth - 10 };
                addButton.Click += (sender, e) =>
                {
                    for (int v = startPage.Value; v < endPage.Value + 1; v++)
                    {
                        if (!list.Items.Contains(v))
                        {
                            list.Items.Add(v);
                        }
                    }
                    List<int> ints = list.Items.Cast<int>().ToList();
                    ints.Sort();
                    Submission.ListToText(submission.Pages, ints);
                    flow.Controls.Add(list);
                };
                remButton.Click += (sender, e) =>
                {
                    for (int v = startPage.Value; v < endPage.Value + 1; v++)
                    {
                        if (list.Items.Contains(v))
                        {
                            list.Items.Remove(v);
                        }
                    }
                    List<int> ints = list.Items.Cast<int>().ToList();
                    ints.Sort();
                    Submission.ListToText(submission.Pages, ints);
                    flow.Controls.Add(list);
                };

            }

            return hander;
        }
        private static EventHandler? AddIrregular(Submission submission, TableContentsPanel tableLayoutPanel)
        {
            submission.Pages = null;
            tableLayoutPanel.AddTextInput("説明", text: "プリント", field: true, eventHandler: (sender, e) => submission.Description = ((TextBox?)sender)?.Text);
            submission.Description = "プリント";
            return null;
        }
        private EventHandler AddFix(Schedule schedule, Submission submission, TableContentsPanel tableLayoutPanel)
        {
            submission.Description = null;
            ListView list = new()
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Height = 120,
            };
            ListView selectedList = new()
            {
                View = View.List,
                FullRowSelect = true,
                GridLines = true,
                Height = 120,
            };
            IList<int> ints = [];
            list.Columns.Add(new ColumnHeader() { Text = "名前", Width = 200 });
            list.Columns.Add(new ColumnHeader() { Text = "開始日", Width = 100 });
            list.Columns.Add(new ColumnHeader() { Text = "終了日", Width = 100 });
            list.Columns.Add(new ColumnHeader() { Text = "ID", Width = 60 });
            submission.Pages ??= [];
            AddSchedulesOnSubject(schedule.Subject ?? string.Empty, list);
            tableLayoutPanel.AddCustomRow("選択可能\n（ダブルクリックで追加／削除）", list, fit: true);
            tableLayoutPanel.AddCustomRow("修正する予定", selectedList, fit: true);
            list.DoubleClick += (sender, e) =>
            {
                if (ints.Contains(list.SelectedIndices[0]))
                {
                    selectedList.Items.RemoveAt(ints.IndexOf(list.SelectedIndices[0]));
                    ints.Remove(list.SelectedIndices[0]);
                    submission.Pages.Remove(list.SelectedItems[0].SubItems[3].Text);
                }
                else
                {
                    ints.Add(list.SelectedIndices[0]);
                    selectedList.Items.Add((ListViewItem)list.SelectedItems[0].Clone());
                    submission.Pages.Add(list.SelectedItems[0].SubItems[3].Text);
                }
            };

            return new EventHandler((sender, e) =>
            {
                list.Items.Clear();
                ints.Clear();
                selectedList.Items.Clear();
                AddSchedulesOnSubject(schedule.Subject ?? string.Empty, list);
            });


        }

        private void AddSchedulesOnSubject(string subject, ListView list)
        {
            foreach (Schedule schedule in userData.Schedules)
            {
                if (schedule.Subject == subject)
                {
                    ListViewItem item = new([schedule.Title, schedule.Start.ToShortDateString(), schedule.End.ToShortDateString(), schedule.Id.ToString()])
                    {
                        BackColor = schedule.Color,
                        ForeColor = schedule.GetTextColor()
                    };
                    list.Items.Add(item);
                }
            }
        }
    }

}
