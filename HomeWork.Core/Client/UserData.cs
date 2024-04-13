using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;

namespace HomeWork.Core.Client
{
    public class UserData
    {
        private static readonly JsonSerializerSettings options = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            Converters = [new StringEnumConverter()]
        };

        [JsonIgnore]
        private string FilePath = string.Empty;

        public Properties Properties { get; set; } = new Properties();


        public List<int> Friends { get; set; } = [];


        public Subject[] Subjects { get; set; } = [];


        public Note[] Regulars { get; set; } = [];


        public List<Schedule> Schedules { get; set; } = [];


        public static UserData? LoadJson(string filepath)
        {
            UserData? userData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(filepath));
            if (userData != null)
            {
                userData.FilePath = filepath;
            }

            return userData;
        }

        public static UserData? LoadDefault()
        {
            string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "HomeWork", "schedules.json");
            UserData userData;
            try
            {
                userData = JsonConvert.DeserializeObject<UserData>(File.ReadAllText(text)) ?? new UserData();
            }
            catch
            {
                Debug.WriteLine("ファイルが存在しないので作成します");
                userData = new UserData();
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "HomeWork");
                if (!Directory.Exists(path))
                {
                    Debug.WriteLine("ディレクトリが存在しないので作成します");
                    Directory.CreateDirectory(path);
                }

                userData.Save(text);
            }

            return userData;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, options);
        }

        public static void SaveJson(UserData json, string filepath)
        {
            File.WriteAllText(filepath, json.ToJson());
        }

        public Subject? GetSubject(string? id)
        {
            Subject[] subjects = Subjects;
            foreach (Subject subject in subjects)
            {
                if (subject.Id == id)
                {
                    return subject;
                }
            }

            return null;
        }

        public Note? LoadRegular(string id)
        {
            Note[] regulars = Regulars;
            foreach (Note note in regulars)
            {
                if (note.Id == id)
                {
                    return note;
                }
            }

            return null;
        }

        public Note[] SearchRegular(string? subject)
        {
            List<Note> list = [];
            Note[] regulars = Regulars;
            foreach (Note note in regulars)
            {
                if (note.Subject == subject)
                {
                    list.Add(note);
                }
            }

            return [.. list];
        }

        public void Save()
        {
            File.WriteAllText(FilePath, ToJson());
        }

        public void Save(string filepath)
        {
            File.WriteAllText(filepath, ToJson());
        }

        public List<int>[] GetSchedules(DateTime dateStart, DateTime dateFinish)
        {
            List<int>[] array = new List<int>[(int)dateFinish.Subtract(dateStart).TotalDays];
            for (int j = 0; j < Schedules.Count; j++)
            {
                Schedule schedule = Schedules[j];
                if (schedule.Start.CompareTo(dateFinish) > 0 || schedule.End.CompareTo(dateStart) < 0)
                {
                    continue;
                }

                int num = Math.Max(0, (int)schedule.Start.Subtract(dateStart).TotalDays);
                int num2 = Math.Min(array.Length - 1, (int)schedule.End.Subtract(dateStart).TotalDays);
                for (int k = num; k < num2 + 1; k++)
                {
                    if (array[k] == null)
                    {
                        array[k] = [];
                    }

                    array[k].Add(j);
                    Array.Sort(array[k].ToArray());
                }
            }

            for (int l = 0; l < array.Length; l++)
            {
                if (array[l] != null)
                {
                    int num3 = l;
                    List<int> list = [.. array[l].OrderBy((i) => Schedules[i].Start.Ticks)];
                    array[num3] = list;
                }
            }

            return array;
        }
    }
}
