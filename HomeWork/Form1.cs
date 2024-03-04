using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using HomeWork.schedule;
using HomeWork.Views;
using Newtonsoft.Json.Linq;

namespace HomeWork
{
    public partial class Form1 : Form
    {
        private readonly SchedulesObject schedules = ResetF();
        private readonly FontFamily fontFamily = FontFamily.GenericSansSerif;
        private readonly IList<Event> holidays;
        private float scroll = 0;
        private int month;
        private DateTime monthDate;
        private List<int>[]? displayingSchedules;
        private DateTime? detailDate = null;

        public Form1()
        {
            InitializeComponent();
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyACDT2kmVWYcSNd_yS2xKSBXj71EwA5iNw",
            });

            // カレンダーイベントの取得
            holidays = service.Events.List("ja.japanese#holiday@group.v.calendar.google.com").Execute().Items;
            (Font, FontFamily) t = LoadFontFromFile(@"Resources/Font/NotoSansJP-VariableFont_wght.ttf", 8);
            Font = t.Item1;
            fontFamily = t.Item2;
            menuStrip1.Font = Font;
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
        private static SchedulesObject ResetF()
        {
            string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            SchedulesObject? schedules = SchedulesObject.LoadJson(Path.Combine(home, "HWCalendar", "schedules.json"));
            return schedules ?? new SchedulesObject();
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
        private void DrawCalendar()
        {
            monthDate = DateTime.Now;
            monthDate = new(monthDate.Year, monthDate.Month, 1);


            year.Text = monthDate.Year + "年" + monthDate.Month + "月";
            monthDate = monthDate.AddDays(-(int)monthDate.DayOfWeek);
            displayingSchedules = schedules.GetSchedules(monthDate, monthDate.AddDays(36));
            for (int i = 0; i < 35; i++)
            {
                var picture = new PictureBox() { Location = new Point(-5, 15) };
                Panel flowLayoutPanel = new() { Padding = new Padding(0), Margin = new Padding(0) };
                var today = monthDate.AddDays(i);
                Event? holiday = SearchEvent(holidays, today);
                flowLayoutPanel.Controls.Add(new Label() { Text = today.Day.ToString(), AutoSize = true, ForeColor = DayColor(today, holiday) });

                flowLayoutPanel.Controls.Add(new Label() { Text = holiday?.Summary, AutoSize = true, Location = new(24, 0), ForeColor = Color.Red, Font = new(fontFamily, 4.5f), MaximumSize = new(picture.Width - 30, 0) });
                flowLayoutPanel.Controls.Add(picture);
                calendar.Controls.Add(flowLayoutPanel, i % 7, i / 7);
                int index = i;


                picture.Paint += (obj, e) =>
                {
                    if (displayingSchedules[index] != null)
                    {
                        {
                            int p = 0;
                            for (int i1 = 0; i1 < displayingSchedules[index].Count; i1++)
                            {

                                var schedule = schedules.Schedules[displayingSchedules[index][i1]];
                                using Pen pen = new(schedule.Color, 6);

                                using GraphicsPath path = new();
                                path.AddLine(0, 10 + 6 * i1, 2, 10 + 6 * i1);
                                if (displayingSchedules[index + 1] != null && displayingSchedules[index + 1].Contains(displayingSchedules[index][i1]))
                                {
                                    path.AddLine(2, 10 + 6 * i1, picture.Width, 10 + 6 * displayingSchedules[index + 1].IndexOf(displayingSchedules[index][i1]));
                                    p++;
                                }
                                else
                                {
                                    path.AddLine(2, 10 + 6 * i1, picture.Width, 10 + 6 * p);
                                }


                                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                                e.Graphics.DrawPath(pen, path);

                            }
                        }
                    }
                };
                picture.Click += (sender, e) => Detail(displayingSchedules[index], monthDate.AddDays(index));
            }
        }

        private static Color DayColor(DateTime today, Event? holiday)
        {
            return holiday != null || today.DayOfWeek == DayOfWeek.Sunday ? Color.Red : today.DayOfWeek == DayOfWeek.Saturday ? Color.Blue : Color.Black;
        }

        private void Calendar_MouseWheel(object? sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
            scroll += Math.Min(2, e.Delta / -120f);
            if (month != (int)scroll)
            {
                month = (int)scroll;
                var date = DateTime.Now.AddMonths((int)scroll);
                SetDate(date);
            }

        }

        private void SetDate(DateTime date)
        {

            year.Text = date.Year + "年" + date.Month + "月";
            date = new DateTime(date.Year, date.Month, 1);
            date = date.AddDays(-(int)date.DayOfWeek);
            monthDate = date;
            displayingSchedules = schedules.GetSchedules(date, date.AddDays(36));
            for (int i = 0; i < 35; i++)
            {
                Panel? flowLayoutPanel = (Panel?)calendar.GetControlFromPosition(i % 7, i / 7);
                if (flowLayoutPanel != null)
                {
                    var today = date.AddDays(i);
                    Event? holiday = SearchEvent(holidays, today);
                    ((Label)flowLayoutPanel.Controls[0]).Text = today.Day.ToString();
                    ((Label)flowLayoutPanel.Controls[0]).ForeColor = DayColor(today, holiday);
                    ((Label)flowLayoutPanel.Controls[1]).Text = holiday?.Summary;
                    flowLayoutPanel.Controls[2].Invalidate();
                }
            }
        }

        private void Detail(List<int> ints, DateTime date)
        {
            detailDate = date;
            float width = detailed.Width - 5;
            detailGroup.Text = $"{date:D}（{date:ddd}）の予定";
            foreach (Control control in detailed.Controls) { control.Dispose(); }
            addSchedule.Items[0] = $"{date:d} に {ScheduleType.Levels[0]} を追加";
            addSchedule.Items[1] = $"{date:d} に {ScheduleType.Levels[1]} を追加";
            addSchedule.Items[2] = $"{date:d} に {ScheduleType.Levels[2]} を追加";

            detailed.Controls.Clear();
            var holiday = SearchEvent(holidays, date);
            int f = 0;
            if (holiday != null)
            {

                Label container = new()
                {
                    Size = new Size((int)width, 30),
                    BackColor = Color.Red,
                    Font = new Font(fontFamily, 9, FontStyle.Bold),
                    Text = holiday.Summary,
                    Location = new Point(detailed.Margin.Left, 0),
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleLeft
                };

                f++;
                detailed.Controls.Add(container);
            }
            if (ints != null)
            {
                for (int i = 0; i < ints.Count; i++)
                {
                    var schedule = schedules.Schedules[ints[i]];
                    FlowLayoutPanel container = new()
                    {
                        FlowDirection = FlowDirection.LeftToRight,
                        Size = new Size((int)(width * (schedule.GetFinishRatio(date) - schedule.GetStartRatio(date))), 30),
                        WrapContents = false,
                        AutoSizeMode = AutoSizeMode.GrowOnly,
                        BackColor = schedule.Color,
                        Location = new Point(detailed.Margin.Left + (int)(schedule.GetStartRatio(date) * width), (i + f) * 35)
                    };


                    container.Controls.Add(new Label()
                    {
                        Font = new Font(fontFamily, 6, FontStyle.Regular),
                        Text = schedule.Subject,
                        ForeColor = schedule.GetTextColor(),
                        AutoSize = true,
                        Dock = DockStyle.Fill,
                    });
                    container.Controls.Add(new Label()
                    {
                        Font = new Font(fontFamily, 9, FontStyle.Bold),
                        Text = schedule.Title,
                        ForeColor = schedule.GetTextColor(),
                        AutoEllipsis = true,
                        AutoSize = true
                    });
                    container.Controls.Add(new Label()
                    {
                        Font = new Font(fontFamily, 7, FontStyle.Regular),
                        Text = (schedule.IsFinish(date) ? schedule.IsStartOfDay() ? "今日で" : "今日、" + schedule.End.ToShortTimeString() + "に" : schedule.End.Date.ToShortDateString() + "に") + "終了",
                        ForeColor = schedule.GetTextColor(),
                        AutoEllipsis = true,
                        AutoSize = true
                    });
                    if (schedule.Detail != null)
                    {
                        container.Controls.Add(new Label()
                        {
                            Font = new Font(fontFamily, 7f, FontStyle.Regular),
                            Text = "提出物 x" + schedule.Detail.Count,
                            ForeColor = schedule.GetTextColor(),
                            AutoEllipsis = true,
                            AutoSize = true,
                            Dock = DockStyle.None,
                        });

                    }
                    container.Click += (sender, e) => AddTab(schedule);

                    detailed.Controls.Add(container);
                }
            }
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
            switch (schedule.Type)
            {
                case "homework":

                    tableLayoutPanel.AddTextRow("配布日", schedule.Start.ToString("f"));
                    tableLayoutPanel.AddTextRow("提出日", schedule.End.ToString("f"));
                    if (schedule.Description != null) tableLayoutPanel.AddTextRow("メモ", schedule.Description);
                    {
                        Subject? subj = schedules.LoadSubject(schedule.Subject);
                        tableLayoutPanel.AddTextRow("教科", subj != null ? subj.Name : "?");
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
                    foreach (Submission submission in schedule.Detail)
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
                        table.ColumnStyles.Add(new(SizeType.Absolute, tableLayoutPanel.ContWidth * 0.32f));
                        table.ColumnStyles.Add(new(SizeType.Absolute, tableLayoutPanel.ContWidth * 0.68f));

                        table.AddTextRow("タイトル", submission.Name);
                        if (submission.Description != null) table.AddTextRow("説明", submission.Description);
                        switch (submission.Category)
                        {
                            case "regular":
                                Note? note = schedules.LoadRegular(submission.Id);
                                if (note == null)
                                {
                                    table.AddTextRow("提出物", $"[{submission.Id}が見つかりませんでした。\nこのIDのノートが存在するか確認してください。]");
                                }
                                else
                                {
                                    table.AddLinkRow("提出物", note.Name ?? "[提出物を読み込めませんでした]", (sender, e) => { Debug.WriteLine("クリックされました"); });
                                }
                                FlowLayoutPanel pages = new() { FlowDirection = FlowDirection.TopDown, AutoSize = true };
                                pages.Controls.AddRange(submission.PageLabel());

                                table.AddCustomRow("ページ", pages);
                                break;
                            case "irregular":
                                break;
                            case "fix":
                                break;
                            default:
                                break;
                        }
                        table.AddTextRow("丸付け", submission.Circling ? "あり" : "なし");
                        table.AddTextRow("公開レベル", submission.ShareLevel.Name);
                    }
                    break;
                default:
                    break;
            }
            tableLayoutPanel.AddTextRow("提供:", $"{schedule.Provided:G}頃\nby{schedule.Provider ?? "自分"}");

        }
        private void NextMonth_Click(object sender, EventArgs e)
        {
            scroll += 1;
            var date = DateTime.Now.AddMonths((int)scroll);
            SetDate(date);
        }

        private void PreviousMonth_Click(object sender, EventArgs e)
        {
            scroll -= 1;
            var date = DateTime.Now.AddMonths((int)scroll);
            SetDate(date);
        }


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

        private void AddScheduleButton_Click(object sender, EventArgs e)
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
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Absolute, tabPage.Width * 0.4f));
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Absolute, tabPage.Width * 0.6f - 27));
            parent.Controls.Add(tableLayoutPanel);
            {
                ComboBox box = new() { DropDownStyle = ComboBoxStyle.DropDownList };
                box.Items.AddRange(ScheduleType.Levels);
                box.SelectedValueChanged += (sender, e) => schedule.ScheduleType = ((ScheduleType?)box.SelectedItem) ?? ScheduleType.Levels[0];
                box.SelectedIndex = addSchedule.SelectedIndex;
                tableLayoutPanel.AddCustomRow("種類", box, true);
            }
            schedule.Title = "新しい予定";
            tableLayoutPanel.AddTextInput("タイトル", "新しい予定", eventHandler:(sender,e)=>schedule.Title= schedule.Title = ((TextBox?)sender)?.Text??string.Empty);
            tableLayoutPanel.AddTextInput("説明", field: true);
            ComboBox subjBox = new() { DropDownStyle = ComboBoxStyle.DropDownList };
            subjBox.Items.AddRange(schedules.Subjects);
            subjBox.SelectedValueChanged += (sender, e) => schedule.Subject = (((Subject?)subjBox.SelectedItem) ?? new Subject() { Name = "未指定", Id = "unknown" }).Id;
            subjBox.SelectedIndex = 0;
            tableLayoutPanel.AddCustomRow("教科", subjBox, true);
            Control selected = new() { Width =10,Height=10, BackColor = Color.BlueViolet};
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

            {
                DateTimePicker dateTime = new()
                {
                    Format = DateTimePickerFormat.Short,
                    CustomFormat = "yyyy/MM/dd HH:mm:ss",
                    Width = tableLayoutPanel.ContWidth - 20,
                };
                
                FlowLayoutPanel panel = new() { AutoSize = true };
                panel.Controls.Add(dateTime);
                CheckBox box1 = new() { Text = "終日", Checked = true };
                box1.CheckedChanged += (sender, e) => dateTime.Format = box1.Checked ? DateTimePickerFormat.Short : DateTimePickerFormat.Custom;
                dateTime.ValueChanged += (sender, e) => schedule.Start = dateTime.Value;
                dateTime.Value = detailDate ?? DateTime.Now.Date;
                panel.Controls.Add(box1);
                tableLayoutPanel.AddCustomRow("開始時刻", panel, true);
            }
            {
                DateTimePicker dateTime = new()
                {
                    Format = DateTimePickerFormat.Short,
                    CustomFormat = "yyyy/MM/dd HH:mm:ss",
                    Width = tableLayoutPanel.ContWidth - 20,
                };
                

                FlowLayoutPanel panel = new() { AutoSize = true };
                panel.Controls.Add(dateTime);
                dateTime.ValueChanged += (sender, e) => schedule.End = dateTime.Value;
                dateTime.Value = detailDate ?? DateTime.Now.Date;
                CheckBox box1 = new() { Text = "終日", Checked = true };
                box1.CheckedChanged += (sender, e) => dateTime.Format = box1.Checked ? DateTimePickerFormat.Short : DateTimePickerFormat.Custom;
                panel.Controls.Add(box1);
                tableLayoutPanel.AddCustomRow("終了時刻", panel, true);
            }
            FlowLayoutPanel flowLayoutPanel = new() { AutoSize = true, FlowDirection = FlowDirection.TopDown, WrapContents = false };
            parent.Controls.Add(flowLayoutPanel);
            AddSubmission(subjBox,flowLayoutPanel, tabPage.Width, schedule);
            Button newSubmission = new() { Text = "新しい提出物", AutoSize = true };
            newSubmission.Click += (sender,e) => AddSubmission(subjBox, flowLayoutPanel, tabPage.Width, schedule);
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
                }
                else
                {
                    errorlabel.Text = error;
                }
            };
            parent.Controls.Add(submitSchedule);
            parent.Controls.Add(errorlabel);
        }
        private void AddSubmission(ComboBox subjBox,FlowLayoutPanel panel, int width, Schedule schedule)
        {
            Submission submission = new();
            schedule.Detail.Add(submission);
            TableContentsPanel tableLayoutPanel = new() { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowOnly, Margin = new(0) };
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Absolute, width * 0.4f));
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Absolute, width * 0.6f - 35));
            TitledPanel titledPanel = new(tableLayoutPanel)
            {
                AutoSizeMode = AutoSizeMode.GrowOnly,
                AutoSize = true,
                MinimumSize = new(width - 35, 25),
                MaximumSize = new(width - 35, 0),
                Text = "提出物",
            };
            panel.Controls.Add(titledPanel);
            Button remthis = new() { Text = "削除",Height=30 };
            remthis.Click += (sender, e) =>
            {
                titledPanel.Dispose();
                panel.Controls.Remove(titledPanel);
                schedule.Detail.Remove(submission);
                GC.Collect();
            };

            tableLayoutPanel.AddCustomRow("この提出物を", remthis, fit:true);
            tableLayoutPanel.AddTextInput("タイトル", "提出物", eventHandler: new((sender, e) => submission.Name = ((TextBox?)sender)?.Text ?? "提出物"));
            FlowLayoutPanel flowLayoutPanel = new() { AutoSize = true };
            ComboBox box = new();
            box.Items.AddRange(SubmissionType.Levels);
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
            EventHandler? handler = AddRegular(schedule,submission, tableLayoutPanel);
            subjBox.SelectedValueChanged += handler;
            box.SelectedValueChanged += (sender, e) =>
            {
                SubmissionType? submType = (SubmissionType?)box.SelectedItem;
                submission.Category = (submType)?.Id;
                ResetTable(tableLayoutPanel, 3);
                subjBox.SelectedIndexChanged -= handler;
                if (submType == SubmissionType.Regular)
                {
                    handler = AddRegular(schedule, submission, tableLayoutPanel);
                }
                if (submType == SubmissionType.Irregular)
                {
                    handler = AddIrregular(submission, tableLayoutPanel);
                }
                if (submType == SubmissionType.Fix)
                {
                    handler = AddFix(schedule, submission, tableLayoutPanel);
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
            var startPage = new TrackBar() { Width = tableLayoutPanel.ContWidth };
            var endPage = new TrackBar() { Width = tableLayoutPanel.ContWidth };
            ComboBox submiBox = new() { DropDownStyle = ComboBoxStyle.DropDownList };
            submiBox.Items.AddRange(schedules.SearchRegular(schedule.Subject));
            submiBox.SelectedValueChanged += (sender, e) =>
            {
                Note? note = (Note?)submiBox.SelectedItem;
                startPage.Maximum = (note?.Pages) ?? 100;
                endPage.Maximum = (note?.Pages) ?? 100;
                submission.Id = note?.Id??string.Empty;
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
                submiBox.Items.AddRange(schedules.SearchRegular(schedule.Subject));
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
            tableLayoutPanel.AddTextInput("説明", text: "プリント", field: true, eventHandler: (sender,e)=> submission.Description = ((TextBox?)sender)?.Text);
            return null;
        }
        private EventHandler AddFix(Schedule schedule, Submission submission, TableContentsPanel tableLayoutPanel)
        {
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
            AddSchedulesOnSubject(schedule.Subject, list);
            tableLayoutPanel.AddCustomRow("選択可能", list, fit: true);
            tableLayoutPanel.AddCustomRow("修正する予定", selectedList, fit: true);
            list.DoubleClick += (sender, e) => {
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
                AddSchedulesOnSubject(schedule.Subject, list);
            });
            

        }

        private void AddSchedulesOnSubject(string subject, ListView list)
        {
            foreach (Schedule schedule in schedules.Schedules)
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
