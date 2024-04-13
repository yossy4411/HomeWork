using HomeWork.Core.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Core.Converter
{
    internal static class EnumTextConverter
    {
        public static string GetJapaneseString(this Enum day)
        {
            FieldInfo? field = day.GetType().GetField(day.ToString());
            if (field is not null)
            {
                DisplayAttribute? attribute = (DisplayAttribute?)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
                return (attribute is null) ? day.ToString() : attribute.Japanese;
            }
            return day.ToString();
        }

        public static string[] GetJapaneseStrings<T>() where T : Enum
        {
            return ((T[])Enum.GetValues(typeof(T))).Select((T v) => GetJapaneseString(v)).ToArray();
        }

        public static ScheduleLevel<T>[] GetEnumValues<T>() where T : Enum
        {
            return ((T[])Enum.GetValues(typeof(T))).Select((T v) => new ScheduleLevel<T>(v, GetJapaneseString(v))).ToArray();
        }

        public static T GetValue<T>(string? name) where T : Enum
        {
            if (name is null)
            {
                return GetEnumValues<T>()[0].Value;
            }
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
