using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Entities
{
    public class Commenttask
    {
        public int id { get; set; }
        public int appuserid { get; set; }
        public string text { get; set; }
        public int taskid { get; set; }
        public DateTime recorddate { get; set; }

    }
}
