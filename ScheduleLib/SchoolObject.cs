using ScheduleLib.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleLib
{
    public class SchoolObject
    {
        public string Id { get; set; } = "A123456789012";
        public string? Name { get; set; } = "学校";
        public string Post { get; set; } = "0000000"; 
        
        public string? Address { get; set; }
    }
    public class User
    {
        public string Id { get; set; } = "A123456789012-3-01-33";
        public string? Name { get; set; }
        public string? NameKana { get; set; }
        public bool Creator { get; set; } = false;
        public Proficiency[] Proficiencies { get; set; } = [];
        public string Note { get; set; } = string.Empty;
    }
    public class Proficiency
    {
        public CertType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public float Number { get; set; } = 1;
    }
}
