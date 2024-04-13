using HomeWork.Core.Converter;
using System.Diagnostics;
using System.Drawing;
using System.Text.Json.Serialization;

namespace HomeWork.Core.Client
{
    public abstract class ScheduleObject;

    public class Schedule
    {
        public string Id { get; set; } = "unknown";


        public string Title { get; set; } = "";


        public string Type { get; set; } = string.Empty;


        [JsonIgnore]
        public ScheduleType? ScheduleType
        {
            get
            {
                return EnumTextConverter.GetValue<ScheduleType>(Type);
            }
            set
            {
                Type = value.ToString() ?? string.Empty;
            }
        }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string? Description { get; set; }

        public DateTime Provided { get; set; }

        public int Provider { get; set; }

        public string? Subject { get; set; }

        public string Color { get; set; } = "#000000";


        [JsonIgnore]
        public Color SystemColor
        {
            get
            {
                return ColorTranslator.FromHtml(Color);
            }
            set
            {
                Color = ColorTranslator.ToHtml(value);
            }
        }

        public List<Submission>? Detail { get; set; }

        public Color GetTextColor()
        {
            return GetContrastColor(SystemColor);
        }

        private static Color GetContrastColor(Color backgroundColor)
        {
            double num = (0.299 * (int)backgroundColor.R + 0.587 * (int)backgroundColor.G + 0.114 * (int)backgroundColor.B) / 255.0;
            return (num > 0.5) ? System.Drawing.Color.Black : System.Drawing.Color.White;
        }

        public float GetStartRatio(DateTime date)
        {
            return (date.Day == Start.Day) ? ((Start.Hour + Start.Minute / 60f) / 24f) : 0f;
        }

        public float GetFinishRatio(DateTime date)
        {
            return (date.Day == End.Day && !IsStartOfDay()) ? ((End.Hour + End.Minute / 60f) / 24f) : 1f;
        }

        public bool IsStartOfDay()
        {
            return End.Hour == 0 && End.Minute == 0 && End.Second == 0;
        }

        public bool IsStart(DateTime date)
        {
            return Start.Date == date.Date;
        }

        public bool IsFinish(DateTime date)
        {
            return End.Date == date.Date;
        }

        public override string ToString()
        {
            return $"[{Title} by {Provider}(id)]\r\nId: {Id}\r\nStart: {Start}\r\nEnd: {End}";
        }

        public static string? CheckCorrect(Schedule schedule)
        {
            if (schedule.Start.CompareTo(schedule.End) > 0)
            {
                return "予定の開始時刻は終了時刻より前でないといけません";
            }

            if (schedule.Detail is not null)
            {
                for (int i = 0; i < schedule.Detail.Count; i++)
                {
                    if (schedule.Detail[i].Category is SubmissionCategory.Regular)
                    {
                        List<string>? pages = schedule.Detail[i].Pages;
                        if (pages == null)
                        {
                            return $"ページが指定されていない箇所（提出物#{i}）があります。";
                        }

                        if (pages.Count == 0)
                        {
                            return $"提出物#{i}でページが含まれていないことは好ましくありません";
                        }
                    }
                }
            }

            return null;
        }

        public static void Finalize(Schedule schedule, Authorizer auth, bool publish = false)
        {
            Debug.WriteLine(schedule.Detail);
            DateTime now = DateTime.Now;
            now.AddTicks(-now.Ticks);
            schedule.Provided = now;
            string value = now.ToString("yyyyMMdd");
            int num = 1;
            if (auth.scheduleIds != null)
            {
                foreach (string scheduleId in auth.scheduleIds)
                {
                    if (Convert.ToInt64(scheduleId, 16).ToString().StartsWith(value))
                    {
                        num++;
                    }
                }
            }

            schedule.Id = long.Parse(now.ToString("yyyyMMdd") + num).ToString("X");
            if (schedule.ScheduleType is not Client.ScheduleType.Homework)
            {
                schedule.Detail = null;
                schedule.Subject = null;
            }

            if (schedule.ScheduleType is Client.ScheduleType.ShortEvent)
            {
                DateTime end = (schedule.Start = schedule.Start.Date);
                schedule.End = end;
            }

            if (publish)
            {
                schedule.Provider = auth.UserID;
            }
        }
    }
    public class Authorizer
    {
        public int UserID;

        public List<string>? scheduleIds;

        private Authorizer()
        {
        }

        public static Authorizer Create(UserData properties)
        {
            return new Authorizer
            {
                UserID = properties.Properties.User.Id,
                scheduleIds = properties.Schedules.Select((Schedule s) => s.Id).ToList()
            };
        }

        public static Authorizer Create()
        {
            return new Authorizer();
        }
    }
    public enum ScheduleType
    {
        [Display("宿題")]
        Homework,
        [Display("イベント（期間）")]
        LongEvent,
        [Display("イベント（１日間）")]
        ShortEvent
    }
    public enum CertType
    {
        [Display("検定")]
        Certifications,
        [Display("地位")]
        Position
    }
    public enum SubmissionCategory
    {
        [Display("定期")]
        Regular,
        [Display("不定期")]
        Irregular,
        [Display("修正・再提出")]
        Fix
    }
    public enum ShareLevel
    {
        [Display("なし")]
        None,
        [Display("学校内の友達のみ")]
        SchoolFriendOnly,
        [Display("クラス内のみ")]
        InClass,
        [Display("学年内")]
        InGrade,
        [Display("学校内")]
        InSchool,
        [Display("すべての友達")]
        AllFriend
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    internal class DisplayAttribute(string japaneseDay) : Attribute()
    {
        public string Japanese { get; } = japaneseDay;
    }

}
