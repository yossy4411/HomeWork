using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Core.Client
{

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
            return (value == null) ? (T)Enum.ToObject(typeof(T), 0) : value.Value;
        }
    }

}
