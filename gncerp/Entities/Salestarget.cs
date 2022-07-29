using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Entities
{
    public class Salestarget
    {
        public int year { get; set; }
        public int month { get; set; }
        public double target { get; set; }
        public double ciro { get; set; }
        public int appuserid { get; set; }
        public string username { get; set; }
        public string tel { get; set; }
    }
}
