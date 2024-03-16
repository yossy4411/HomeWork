using Google.Apis.Calendar.v3.Data;
using ScheduleLib.Schedule;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    public partial class MainForm
    {
        private void DrawCalendar()
        {
            monthDate = DateTime.Now;
            monthDate = new(monthDate.Year, monthDate.Month, 1);


            year.Text = monthDate.Year + "年" + monthDate.Month + "月";
            monthDate = monthDate.AddDays(-(int)monthDate.DayOfWeek);
            displayingSchedules = userData.GetSchedules(monthDate, monthDate.AddDays(36));
            using var calendarFont = new Font(fontFamily, 4.5f);
            for (int i = 0; i < 35; i++)
            {
                var picture = new PictureBox() { Location = new Point(-5, 15) };
                Panel flowLayoutPanel = new() { Padding = new Padding(0), Margin = new Padding(0) };
                var today = monthDate.AddDays(i);
                Event? holiday = SearchEvent(holidays, today);
                flowLayoutPanel.Controls.Add(new Label() { Text = today.Day.ToString(), AutoSize = true, ForeColor = DayColor(today, holiday) });

                flowLayoutPanel.Controls.Add(new Label() { Text = holiday?.Summary, AutoSize = true, Location = new(24, 0), ForeColor = Color.Red, Font = calendarFont, MaximumSize = new(picture.Width - 30, 0) });
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

                                var schedule = userData.Schedules[displayingSchedules[index][i1]];
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
                SetCalendar();
            }

        }
        private void NextMonth_Click(object sender, EventArgs e)
        {
            scroll += 1;
            SetCalendar();
        }

        private void PreviousMonth_Click(object sender, EventArgs e)
        {
            scroll -= 1;
            SetCalendar();
        }

        private void SetCalendar()
        {
            var date = DateTime.Now.AddMonths((int)scroll);
            year.Text = date.Year + "年" + date.Month + "月";
            date = new DateTime(date.Year, date.Month, 1);
            date = date.AddDays(-(int)date.DayOfWeek);
            monthDate = date;
            displayingSchedules = userData.GetSchedules(date, date.AddDays(36));
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
            addSchedule.Items[0] = $"{date:d} に {ScdLevel.GetJapaneseString(ScheduleType.Homework)} を追加";
            addSchedule.Items[1] = $"{date:d} に {ScdLevel.GetJapaneseString(ScheduleType.LongEvent)} を追加";
            addSchedule.Items[2] = $"{date:d} に {ScdLevel.GetJapaneseString(ScheduleType.ShortEvent)} を追加";
            detailed.Controls.Clear();
            var holiday = SearchEvent(holidays, date);
            int f = 0;
            using var ninefont = new Font(fontFamily, 9, FontStyle.Bold);
            using var sixfont = new Font(fontFamily, 6, FontStyle.Bold);
            using var sevenfont = new Font(fontFamily, 7, FontStyle.Bold);

            if (holiday != null)
            {

                Label container = new()
                {
                    Size = new Size((int)width, 30),
                    BackColor = Color.Red,
                    Font = ninefont,
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
                    var schedule = userData.Schedules[ints[i]];
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
                        Font = sixfont,
                        Text = schedule.ScheduleType == ScheduleType.Homework ? schedule.Subject : "event",
                        ForeColor = schedule.GetTextColor(),
                        AutoSize = true,
                        Dock = DockStyle.Fill,
                    });
                    container.Controls.Add(new Label()
                    {
                        Font = ninefont,
                        Text = schedule.Title,
                        ForeColor = schedule.GetTextColor(),
                        AutoEllipsis = true,
                        AutoSize = true
                    });
                    container.Controls.Add(new Label()
                    {
                        Font = sevenfont,
                        Text = (schedule.IsFinish(date) ? schedule.IsStartOfDay() ? "今日で" : "今日、" + schedule.End.ToShortTimeString() + "に" : schedule.End.Date.ToShortDateString() + "に") + "終了",
                        ForeColor = schedule.GetTextColor(),
                        AutoEllipsis = true,
                        AutoSize = true
                    });
                    if (schedule.Detail != null)
                    {
                        container.Controls.Add(new Label()
                        {
                            Font = sevenfont,
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
    }
}
