using Newtonsoft.Json;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;
using System.Drawing;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace ScheduleLib.Schedule
{
    public abstract class ScheduleObject { }
    public partial class UserData : ScheduleObject
    {
        private static readonly JsonSerializerSettings options = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            Converters = [new Newtonsoft.Json.Converters.StringEnumConverter()]
        };
        public Properties Properties { get; set; } = new Properties();
        public List<int> Friends { get; set; } = [];
        public Subject[] Subjects { get; set; } = [];
        public Note[] Regulars { get; set; } = [];
        public List<Schedule> Schedules { get; set; } = [];
        
        [JsonIgnore]
        string FilePath = string.Empty;

        public static UserData? LoadJson(string filepath)
        {
            UserData? schedules = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(filepath));
            if (schedules != null) schedules.FilePath = filepath;
            return schedules;
        }
        public string ToJson() => JsonConvert.SerializeObject(this, options);
        public static void SaveJson(UserData json, string filepath)
        {
            File.WriteAllText(filepath, json.ToJson());
        }
        public Subject? LoadSubject(string? id)
        {
            foreach (Subject subject in Subjects)
            {
                if (subject.Id == id) { return subject; }
            }
            return null;
        }
        public Note? LoadRegular(string id)
        {
            foreach (Note note in Regulars)
            {
                if (note.Id == id) { return note; }
            }
            return null;
        }
        public Note[] SearchRegular(string? subject)
        {
            List<Note> notes = [];
            foreach (Note note in Regulars)
            {
                if (note.Subject == subject) { notes.Add(note); }
            }
            return [.. notes];
        }
        public void Save()
        {
            File.WriteAllText(FilePath, ToJson());
        }
        public List<int>[] GetSchedules(DateTime dateStart, DateTime dateFinish)
        {
            var result = new List<int>[(int)dateFinish.Subtract(dateStart).TotalDays];
            for (int i1 = 0; i1 < Schedules.Count; i1++)
            {
                var schedule = Schedules[i1];
                if (schedule.Start.CompareTo(dateFinish) <= 0 &&
                    schedule.End.CompareTo(dateStart) >= 0)
                {

                    int startIndex = Math.Max(0, (int)schedule.Start.Subtract(dateStart).TotalDays);
                    int endIndex = Math.Min(result.Length - 1, (int)schedule.End.Subtract(dateStart).TotalDays);

                    for (int i = startIndex; i < endIndex + 1; i++)
                    {
                        if (result[i] == null) result[i] = [];
                        result[i].Add(i1);
                        Array.Sort(result[i].ToArray());
                    }
                }
            }
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] != null) result[i] = [.. result[i].OrderBy(i => Schedules[i].Start.Ticks)];
            }

            return result;
        }
    }

    public class Subject : ScheduleObject
    {
        public string Id { get; set; } = "unknown";
        public string Name { get; set; } = string.Empty;
        public override string ToString()
        {
            return Name;
        }
    }
    public class Note : ScheduleObject
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string Subject { get; set; } = string.Empty;
        public int Pages { get; set; }
        public string? Alias { get; set; }
        public override string? ToString()
        {
            return Name;
        }
        public string? Type { get { return Id?.Split('.')[1]; } }
    }
    public class Schedule : ScheduleObject
    {
        public string Id { get; set; } = "unknown";
        public string Title { get; set; } = "";
        public string Type { get; set; } = string.Empty;
        [JsonIgnore]
        public ScheduleType? ScheduleType
        {
            get => ScdLevel.GetValue<ScheduleType>(Type);
            set => Type = value.ToString() ?? string.Empty;
        }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Description { get; set; }
        public DateTime Provided { get; set; }
        public int Provider { get; set; }
        public string? Subject { get; set; }
        public Color Color { get; set; }
        public Color GetTextColor() => GetContrastColor(Color);
        public List<Submission>? Detail { get; set; }


        static Color GetContrastColor(Color backgroundColor)
        {
            // 色の輝度を計算 (RGB to YIQ)
            double brightness = (0.299 * backgroundColor.R + 0.587 * backgroundColor.G + 0.114 * backgroundColor.B) / 255;

            // コントラスト比に基づいて白または黒を選択
            return brightness > 0.5 ? Color.Black : Color.White;
        }
        public float GetStartRatio(DateTime date) => date.Day == Start.Day ? (Start.Hour + Start.Minute / 60f) / 24f : 0;
        public float GetFinishRatio(DateTime date) => date.Day == End.Day && !IsStartOfDay() ? (End.Hour + End.Minute / 60f) / 24f : 1;
        public bool IsStartOfDay() => End.Hour == 0 && End.Minute == 0 && End.Second == 0;
        public bool IsStart(DateTime date) => Start.Date == date.Date;
        public bool IsFinish(DateTime date) => End.Date == date.Date;
        public static string? CheckCorrect(Schedule schedule)
        {
            if (schedule.Start.CompareTo(schedule.End) > 0) return "予定の開始時刻は終了時刻より前でないといけません";
            if (schedule.Detail != null)
            {
                for (int i = 0; i < schedule.Detail.Count; i++)
                {
                    if (schedule.Detail[i].Category == SubmissionCategory.Regular)
                    {
                        var pages = schedule.Detail[i].Pages;
                        if (pages == null) return $"ページが指定されていない箇所（提出物#{i}）があります。";
                        else if (pages.Count == 0) return $"提出物#{i}でページが含まれていないことは好ましくありません";
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Setup schedule for publishing.
        /// </summary>
        /// <param name="schedule">Value to setup</param>
        public static void Finalize(Schedule schedule, Authorizer auth, bool publish = false)
        {
            Debug.WriteLine(schedule.Detail);
            var now = DateTime.Now;
            now.AddTicks(-now.Ticks);
            schedule.Provided = now;
            var nowId = now.ToString("yyyyMMdd");
            int id = 1;
            if (auth.scheduleIds != null)
                foreach (var s in auth.scheduleIds)
                {
                    if (Convert.ToInt64(s, 16).ToString().StartsWith(nowId))
                        id++;
                }
            schedule.Id = long.Parse(now.ToString("yyyyMMdd") + id).ToString("X");
            if (schedule.ScheduleType != ScheduleLib.Schedule.ScheduleType.Homework)
            {
                schedule.Detail = null;
                schedule.Subject = null;
            }
            if (schedule.ScheduleType == ScheduleLib.Schedule.ScheduleType.ShortEvent)
            {
                schedule.End = schedule.Start = schedule.Start.Date;
            }
            if (publish)
            {
                schedule.Provider = auth.UserID;
            }
        }
    }
    public class Properties
    {
        public User User { get; set; } = new User();
    }
    public class Authorizer
    {
        private Authorizer() { }
        public static Authorizer Create(UserData properties)
        {
            return new()
            {
                UserID = properties.Properties.User.Id,
                scheduleIds = properties.Schedules.Select(s => s.Id).ToList(),
            };
        }
        public static Authorizer Create() => new();
        public int UserID;
        public List<string>? scheduleIds;
    }
    public class Submission
    {
        public string Name { get; set; } = "提出物";
        public string? Description { get; set; }
        public SubmissionCategory Category { get; set; }
        public string Id { get; set; } = string.Empty;
        public List<string>? Pages { get; set; }
        [JsonIgnore]
        public ShareLevel Share { get; set; }

        public bool Circling { get; set; } = false;

        public static void ListToText(List<string> origin, IReadOnlyList<int> numbers)
        {
            origin.Clear();
            for (int i = 0; i < numbers.Count; i++)
            {
                int start = numbers[i];
                int end = start;

                while (i + 1 < numbers.Count && numbers[i + 1] == end + 1)
                {
                    end = numbers[i + 1];
                    i++;
                }

                if (start != end)
                {
                    origin.Add(start + "-" + end);
                }
                else
                {
                    origin.Add(start.ToString());
                }
            }
        }
    }
}
