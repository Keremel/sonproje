using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Entities
{
    public class Appuser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string tel { get; set; }
        public string password { get; set; }
        public bool ispsv { get; set; }
        public bool isdel { get; set; }
       
    }
}
