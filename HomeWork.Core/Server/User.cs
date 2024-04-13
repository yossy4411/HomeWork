using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Core.Server
{
    public class User
    {
        public string Class { get; set; } = "A123456789012-00000";


        public int Id { get; set; } = -1;


        [JsonIgnore]
        public string SchoolId => Class[..13];

        public string? Name { get; set; }

        public string? NameKana { get; set; }

        public bool Creator { get; set; } = false;


        public Proficiency[] Proficiencies { get; set; } = [];


        public string Memo { get; set; } = string.Empty;


        public List<string> Links { get; set; } = [];

    }
}
