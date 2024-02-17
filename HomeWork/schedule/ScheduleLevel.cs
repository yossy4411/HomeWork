using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.schedule
{
    public abstract class ScheduleLevel(string id, string name)
    {
        public string Name { get; private set; } = name;

        public string Id { get; private set; } = id;

        protected static ScheduleLevel GetLevel(string? id, ScheduleLevel[] levels)
        {

            foreach (var shareLevel in levels)
            {
                if (shareLevel.Id == id) return shareLevel;
            }
            return levels[0];
        }
        public override string ToString() { return Name; }
    }
    public class ShareLevel : ScheduleLevel
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
    public class ScheduleType : ScheduleLevel
    {
        private ScheduleType(string a, string b) : base(a, b) { }
        public static readonly ScheduleType Homework = new("homework", "宿題");
        public static readonly ScheduleType LongEvent = new("event", "イベント（期間指定）");
        public static readonly ScheduleType ShortEvent = new("dayevent", "イベント（１日間）");
        public static readonly ScheduleType[] Levels = [Homework, LongEvent, ShortEvent ];
        public static ScheduleType GetLevel(string? id) => (ScheduleType)GetLevel(id, Levels);
    }
    public class SubmissionType : ScheduleLevel
    {
        private SubmissionType(string a, string b) : base(a,b) { }
        public static readonly SubmissionType Regular = new("regular", "定期");
        public static readonly SubmissionType Irregular = new("regular", "不定期");
        public static readonly SubmissionType Fix = new("fix", "修正");
        public static readonly SubmissionType[] Levels = [Regular, Irregular, Fix];
    }
}
