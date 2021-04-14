using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CardProcessing.VO
{
    class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public int team { get; set; }
        public int age { get; set; }

    }
}
