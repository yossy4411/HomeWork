

namespace ScheduleLib.Linq
{
    public static class ScheduleList
    {
        public static Schedule.Schedule? GetSchedule(this List<Schedule.Schedule> schedules, string id)
        {
            foreach (Schedule.Schedule schedule in schedules)
            {
                if (schedule.Id == id) return schedule;
            }
            return null;
        }
    }
}
