using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Entities
{
    public class Taskdb
    {
        public int id { get; set; }
        public string title { get; set; }
        public string information { get; set; }
        public int importance { get; set; }
        public int taskstatus { get; set; }
        public DateTime startdate { get; set; }
        public DateTime completedate { get; set; }
        public int appuserid { get; set; }
        public int appuserassignid { get; set; }


    }
}
