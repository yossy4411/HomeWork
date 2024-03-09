using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;
using SeIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using ScheduleLib;

namespace ScheduleLib
{
    public class SchedulesObject
    {
        private static readonly JsonSerializerOptions options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        public Properties Properties { get; set; } = new Properties();
        public Subject[] Subjects { get; set; } = [];
        public Note[] Regulars { get; set; } = [];
        public List<Schedule> Schedules { get; set; } = [];
        [JsonIgnore]
        [SeIgnore]
        string FilePath = string.Empty; 
        
        public static SchedulesObject? LoadJson(string filepath)
        {
            SchedulesObject? schedules = JsonConvert.DeserializeObject<SchedulesObject>(File.ReadAllText(filepath));
            if(schedules != null) schedules.FilePath = filepath;
            return schedules;
        }
        public string ToJson() => System.Text.Json.JsonSerializer.Serialize(this, options);
        public static void SaveJson(SchedulesObject json, string filepath)
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

    public class Subject
    {
        public string Id { get; set; } = "unknown";
        public string Name { get; set; } = string.Empty;
        public override string ToString()
        {
            return Name;
        }
    }
    public class Note
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
        public string? Type { get {  return Id?.Split('.')[1]; } }
    }
    public class Schedule
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Type { get; set; } = string.Empty;
        [JsonIgnore]
        [SeIgnore]
        public ScheduleType? ScheduleType
        {
            get => ScdLevel.GetValue<ScheduleType>(Type);
            set => Type = value.ToString()??string.Empty;
        }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Description { get; set; }
        public DateTime Provided {  get; set; }
        public string? Provider {  get; set; }
        public string? Subject { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(SubmissionParser.ColorConverter))]
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
                    if (schedule.Detail[i].CategoryType == SubmissionCategory.Regular)
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
        public static void Finalize(Schedule schedule, Authorizer auth, bool publish = false) {
            schedule.Provided = DateTime.Now;
            if (schedule.ScheduleType != ScheduleLib.ScheduleType.Homework)
            {
                schedule.Detail = null;
                schedule.Subject = null;
                
            }
            if (schedule.ScheduleType == ScheduleLib.ScheduleType.ShortEvent)
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
        public string Int { get; set; } = "00000000000000000000";
    }
    public class Authorizer
    {
        public static Authorizer Create(Properties properties)
        {
            return new() { UserID = properties.Int };
        }
        public static Authorizer Create()
        {
            return new();
        }
        public string? UserID;
    }
    public class Submission
    {
        public string Name { get; set; } = "提出物";
        public string? Description { get; set; }
        public string? Category {  get; set; }
        public string Id { get; set; } = string.Empty;
        public List<string>? Pages { get; set; }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public SubmissionCategory CategoryType
        {
            get { return ScdLevel.GetValue<SubmissionCategory>(Category); }
            set { Category = value.ToString(); }
        }
        public string? Share { get; set; }

        public bool Circling { get; set; } = false;

        public static void ListToText(List<string> origin, IReadOnlyList<int> numbers)
        {
            origin.Clear();
            for (int i = 0; i < numbers.Count; i++)
            {
                int start = numbers[i];
                int end = start;

                while (i + 1 < numbers.Count && numbers[i+1] == end + 1)
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
        [JsonIgnore]
        [SeIgnore]
        public ShareLevel ShareLevel
        {
            get { return ScdLevel.GetValue<ShareLevel>(Share); }
            set { Share = value.ToString(); }
        }
    }
}
