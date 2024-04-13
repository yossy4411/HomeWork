using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Core.Client
{
    public class Subject : ScheduleObject
    {
        public string Id { get; set; } = "unknown";


        public string Name { get; set; } = string.Empty;


        public override string ToString()
        {
            return "教科" + Name;
        }
    }

}
