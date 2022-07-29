using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Entities
{
    public class Accountactivity
    {
        public int id { get; set; }
        public int accountnoid { get; set; }
        public string info { get; set; }
        public string activititype { get; set; }
        public string taxnumber { get; set; }
        public string reference { get; set; }
        public string amount { get; set; }
        public string balance { get; set; }
        public string transactiondate { get; set; }
        public string valuedate { get; set; }
        public string timestamp { get; set; }
        public string processtype { get; set; }

    }
}
