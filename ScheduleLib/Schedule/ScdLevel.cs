using System.ComponentModel.DataAnnotations;

namespace ScheduleLib.Schedule
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class DisplayAttribute(string japaneseDay) : Attribute
    {
        public string Japanese { get; } = japaneseDay;
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
    public enum ScheduleType
    {
        [Display("宿題")]
        Homework,
        [Display("イベント（期間）")]
        LongEvent,
        [Display("イベント（１日間）")]
        ShortEvent
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
    public enum CertType
    {
        [Display("検定")]
        Certifications,
        [Display("地位")]
        Position
    }
    public class ScheduleLevel<T>(T id, string name) where T : Enum
    {
        public T Value = id;
        public string Name = name;
        public override string ToString()
        {
            return Name;
        }
        public static implicit operator T(ScheduleLevel<T>? value)
        {
            return value == null ? (T)Enum.ToObject(typeof(T), 0) : value.Value;
        }
    }
    public class ScdLevel()
    {

        public static string GetJapaneseString(Enum day)
        {
            var field = day.GetType().GetField(day.ToString());

            if (field != null)
            {
                DisplayAttribute? attribute = (DisplayAttribute?)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
                return attribute == null ? day.ToString() : attribute.Japanese;
            }
            return day.ToString();
        }
        public static string[] GetJapaneseStrings<T>() where T : Enum
        {
            return ((T[])Enum.GetValues(typeof(T))).Select(v => GetJapaneseString(v)).ToArray();
        }
        public static ScheduleLevel<T>[] GetEnumValues<T>() where T : Enum
        {
            return ((T[])Enum.GetValues(typeof(T))).Select(v => new ScheduleLevel<T>(v, GetJapaneseString(v))).ToArray();
        }
        public static T GetValue<T>(string? name) where T : Enum
        {
            if (name == null) return GetEnumValues<T>()[0].Value;
            try
            {
                return (T)Enum.Parse(typeof(T), name);
            }
            catch (Exception)
            {
                try
                {
                    return (T)Enum.ToObject(typeof(T), 0);
                }
                catch (Exception)
                {
                    return GetEnumValues<T>()[0].Value;
                }
            }
        }
    }
}
