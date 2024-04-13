using Newtonsoft.Json;

namespace HomeWork.Core.Client
{
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


        public bool Completed { get; set; } = false;


        public static void ListToText(List<string> origin, IReadOnlyList<int> numbers)
        {
            origin.Clear();
            for (int i = 0; i < numbers.Count; i++)
            {
                int start = numbers[i];
                int end;
                for (end = start; i + 1 < numbers.Count && numbers[i + 1] == end + 1; i++)
                {
                    end = numbers[i + 1];
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
