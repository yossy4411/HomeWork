using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Core.Client
{

    public class Note : ScheduleObject
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string Subject { get; set; } = string.Empty;


        public int Pages { get; set; }

        public string? Alias { get; set; }

        public string? Type
        {
            get
            {
                string? id = Id;
                return id?.Split('.')[1];
            }
        }

        public override string? ToString()
        {
            return Name;
        }
    }
}
