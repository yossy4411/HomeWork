using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Reflection;
using HomeWork.schedule;

namespace HomeWork
{
    public partial class Form1 : Form
    {
        private readonly Schedules schedules = ResetF();
        private readonly FontFamily fontFamily;
        private float scroll = 0;
        private int month;
        private List<int>[]? displayingSchedules;
        public Form1()
        {
            InitializeComponent();

            (Font, FontFamily) t = LoadFontFromFile(@"Resources/Font/NotoSansJP-VariableFont_wght.ttf", 8);
            Font = t.Item1;
            fontFamily = t.Item2;
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
        private static Schedules ResetF()
        {
            string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            Schedules? schedules = Schedules.LoadJson(Path.Combine(home, "HWCalendar", "schedules.json"));
            return schedules ?? new Schedules();
        }

        public void Form1_Load_1(object sender, EventArgs e)
        {
            {
                DateTime date = new DateTime(2023, 12, 1);
                year.Text = date.Year + "年" + date.Month + "月";
                date = date.AddDays(-(int)date.DayOfWeek);
                displayingSchedules = schedules.GetSchedules(date, date.AddDays(36));
                for (int i = 0; i < 35; i++)
                {
                    var picture = new PictureBox() { Location = new Point(-5, 15) };
                    Panel flowLayoutPanel = new() { Padding = new Padding(0), Margin = new Padding(0) };

                    flowLayoutPanel.Controls.Add(new Label() { Text = date.AddDays(i).Day.ToString(), AutoSize = true });
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

                                    var schedule = schedules.schedules[displayingSchedules[index][i1]];
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
                    picture.Click += (sender, e) => Detail(displayingSchedules[index], date.AddDays(index));
                }
            }
            detailed.HorizontalScroll.Enabled = false;
          
            calendarGroup.MouseWheel += Calendar_MouseWheel;
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
            displayingSchedules = schedules.GetSchedules(date, date.AddDays(36));
            for (int i = 0; i < 35; i++)
            {
                Panel? flowLayoutPanel = (Panel?)calendar.GetControlFromPosition(i % 7, i / 7);
                if (flowLayoutPanel != null)
                {
                    ((Label)flowLayoutPanel.Controls[0]).Text = date.AddDays(i).Day.ToString();
                    flowLayoutPanel.Controls[1].Invalidate();
                }
            }

        }

        private void Detail(List<int> ints, DateTime date)
        {
            float width = detailed.Width - 5;
            detailGroup.Text = $"{date.Year}年{date.Month}月{date.Day}日（{new string[] { "日", "月", "火", "水", "木", "金", "土" }[(int)date.DayOfWeek]}）の予定";
            foreach (Control control in detailed.Controls) { control.Dispose(); }
            detailed.Controls.Clear();
            if (ints != null)
            {
                for (int i = 0; i < ints.Count; i++)
                {
                    var schedule = schedules.schedules[ints[i]];
                    FlowLayoutPanel container = new()
                    {
                        FlowDirection = FlowDirection.LeftToRight,
                        Size = new Size((int)(width * (schedule.GetFinishRatio(date) - schedule.GetStartRatio(date))), 30),
                        WrapContents = false,
                        AutoSizeMode = AutoSizeMode.GrowOnly,
                        BackColor = schedule.Color,
                        Location = new Point(detailed.Margin.Left + (int)(schedule.GetStartRatio(date) * width), i * 35)
                    };


                    container.Controls.Add(new Label()
                    {
                        Font = new Font(fontFamily, 6, FontStyle.Regular),
                        Text = schedule.Subject,
                        ForeColor = schedule.GetTextColor(),
                        AutoSize = true
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
                    if (schedule.detail != null)
                    {
                        container.Controls.Add(new Label()
                        {
                            Font = new Font(fontFamily, 7f, FontStyle.Regular),
                            Text = "提出物 x" + schedule.detail.Length,
                            ForeColor = schedule.GetTextColor(),
                            AutoEllipsis = true,
                            AutoSize = true
                        });

                    }
                    container.Click += (sender, e) => AddTab(schedule);
                    detailed.Controls.Add(container);
                }
            }
        }

        private void AddTab(Schedule schedule)
        {
            TableContentsPanel tableLayoutPanel = new () { Location = new(0,30), AutoSize = true, AutoSizeMode = AutoSizeMode.GrowOnly};
            TabPage tabPage = new() { Text = schedule.Title, AutoScroll = true }; 
            tab.TabPages.Add(tabPage);
            tabPage.HorizontalScroll.Enabled = false;
            Button button = new() { Text = "close", AutoSize = true };
            button.Click += (sender, e) => { tabPage.Dispose(); tab.TabPages.Remove(tabPage); };
            
            tabPage.Controls.Add(button);
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Absolute, tabPage.Width * 0.3f));
            tableLayoutPanel.ColumnStyles.Add(new(SizeType.Absolute, tabPage.Width * 0.7f));
            tabPage.Controls.Add(tableLayoutPanel);
            switch (schedule.Type)
            {
                case "homework":
                    tableLayoutPanel.AddRow(["タイトル", schedule.Title]);
                    tableLayoutPanel.AddRow(["配布日", schedule.Start.ToString("f")]);
                    tableLayoutPanel.AddRow(["提出日", schedule.End.ToString("f")]);
                    if(schedule.Description!=null) tableLayoutPanel.AddRow(["メモ", schedule.Description]);
                    break;
                default:
                    break;
            }
            
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


        private void Panel2_Paint(object sender, PaintEventArgs e)
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


    }
}
