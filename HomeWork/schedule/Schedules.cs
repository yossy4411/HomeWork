using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace HomeWork.schedule
{
    internal class Schedules
    {
        public Subject[] subjects { get; set; } = [];
        public Note[] regulars { get; set; } = [];
        public Schedule[] schedules { get; set; } = [];
        public static Schedules? LoadJson(string filepath)
        {
            return JsonConvert.DeserializeObject<Schedules>(File.ReadAllText(filepath));
        }
        public Subject? LoadSubject(string id)
        {
            foreach (Subject subject in subjects)
            {
                if (subject.Id == id) { return subject; }
            }
            return null;
        }
        public Note? LoadRegular(string id)
        {
            foreach (Note note in regulars)
            {
                if (note.Id == id) { return note; }
            }
            return null;
        }
        public List<int>[] GetSchedules(DateTime dateStart, DateTime dateFinish)
        {
            var result = new List<int>[(int)dateFinish.Subtract(dateStart).TotalDays];
            for (int i1 = 0; i1 < schedules.Length; i1++)
            {
                var schedule = schedules[i1];
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
                if (result[i] != null) result[i] = [.. result[i].OrderBy(i => schedules[i].Start.Ticks)];
            }

            return result;
        }
    }

    public class Subject
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
    public class Note
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public int Pages { get; set; }
        public string? Alias { get; set; }
    }
    public class Schedule
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Type { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Description { get; set; }
        public string? Subject { get; set; }
        public Color Color { get; set; }
        public Color GetTextColor() => GetContrastColor(Color);
        public Submission[]? detail { get; set; }

        public static Color GetContrastColor(Color originalColor)
        {
            double originalContrast = CalculateContrast(originalColor, Color.Black);
            double invertedContrast = CalculateContrast(originalColor, Color.White);

            return originalContrast > invertedContrast ? Color.Black : Color.White;
        }

        private static double CalculateContrast(Color color1, Color color2)
        {
            double luminance1 = (0.299 * color1.R + 0.587 * color1.G + 0.114 * color1.B) / 255.0;
            double luminance2 = (0.299 * color2.R + 0.587 * color2.G + 0.114 * color2.B) / 255.0;

            // 相対輝度の計算
            return (Math.Max(luminance1, luminance2) + 0.05) / (Math.Min(luminance1, luminance2) + 0.05);
        }
        public float GetStartRatio(DateTime date) => date.Day == Start.Day ? (Start.Hour + Start.Minute / 60f) / 24f : 0;
        public float GetFinishRatio(DateTime date) => date.Day == End.Day && !IsStartOfDay() ? (End.Hour + End.Minute / 60f) / 24f : 1;
        public bool IsStartOfDay() => End.Hour == 0 && End.Minute == 0 && End.Second == 0;
        public bool IsStart(DateTime date) => Start.Date == date.Date;
        public bool IsFinish(DateTime date) => End.Date == date.Date;
    }
    public class Submission
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category {  get; set; }
        public string? Id { get; set; }
        private string[]? Pages {  get; set; }
        public readonly List<int> PageList = [];
        public Submission()
        {
            if (Pages != null)
            {
                foreach (string page in Pages)
                {
                    if (page.Contains('-'))
                    {
                        string[] strings = page.Split('-');
                        PageList.AddRange(Enumerable.Range(int.Parse(strings[0]), int.Parse(strings[1]) + 1));
                    }
                    else
                    {
                        PageList.Add(int.Parse(page));
                    }
                }
            }
        }
    }
}
