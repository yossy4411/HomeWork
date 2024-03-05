
using System.ComponentModel;
using System.Linq;

namespace HomeWork.schedule
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class DisplayAttribute(string japaneseDay) : Attribute
    {
        public string Japanese { get; } = japaneseDay;
    }
    public enum Share
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
    public enum SchType
    {
        [Display("宿題")]
        Homework,
        [Display("イベント（期間）")]
        LongEvent,
        [Display("イベント（１日間）")]
        ShortEvent
    }
    public class ScheduleLevel<T>(T id, string name) where T : Enum
    {
        public T Id = id;
        public string Name = name;
        public override string ToString()
        {
            return Name;
        }
    }
    public abstract class ScdLevel(string id, string name)
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
        public static T GetValue<T>(string name) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), name);
        }
        public string Name { get; private set; } = name;

        public string Id { get; private set; } = id;

        protected static ScdLevel GetLevel(string? id, ScdLevel[] levels)
        {

            foreach (var shareLevel in levels)
            {
                if (shareLevel.Id == id) return shareLevel;
            }
            return levels[0];
        }
        public override string ToString() { return Name; }
    }
    public class ShareLevel : ScdLevel
    {
       
        private ShareLevel(string a, string b) : base(a,b) { }
        public static readonly ShareLevel None = new("none", "非公開");
        public static readonly ShareLevel SchoolFriendOnly = new("sclfriend", "学校内の友達のみ");
        public static readonly ShareLevel InClass = new("class", "クラス内共有");
        public static readonly ShareLevel InGrade = new("grade", "学年内共有");
        public static readonly ShareLevel InSchool = new("school", "学校内共有");
        public static readonly ShareLevel CityFriendOnly = new("cityfriend", "市内の友達のみ");
        public static readonly ShareLevel AllFriend = new("friends", "全ての友達");
        public static readonly ShareLevel[] Levels = [None, SchoolFriendOnly, InClass, InGrade, InSchool, CityFriendOnly, AllFriend];
        public static ShareLevel GetLevel(string? id) => (ShareLevel)GetLevel(id, Levels);

    }
    public class ScheduleType : ScdLevel
    {
        private ScheduleType(string a, string b) : base(a, b) { }
        public static readonly ScheduleType Homework = new("homework", "宿題");
        public static readonly ScheduleType LongEvent = new("event", "イベント（期間指定）");
        public static readonly ScheduleType ShortEvent = new("dayevent", "イベント（１日間）");
        public static readonly ScheduleType[] Levels = [Homework, LongEvent, ShortEvent ];
        public static ScheduleType GetLevel(string? id) => (ScheduleType)GetLevel(id, Levels);
    }
    public class SubmissionType : ScdLevel
    {
        private SubmissionType(string a, string b) : base(a,b) { }
        public static readonly SubmissionType Regular = new("regular", "定期");
        public static readonly SubmissionType Irregular = new("regular", "不定期");
        public static readonly SubmissionType Fix = new("fix", "修正");
        public static readonly SubmissionType[] Levels = [Regular, Irregular, Fix];
    }
}
