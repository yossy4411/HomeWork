using HomeWork.Core.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Core.Server
{
    public class Proficiency
    {
        public CertType Type { get; set; }

        public string Name { get; set; } = string.Empty;


        public string Text { get; set; } = string.Empty;


        public float Number { get; set; } = 1f;

    }
}
