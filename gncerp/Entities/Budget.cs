using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Entities
{
    public class Budget
    {
        public int id { get; set; }
        public int cost { get; set; }
        public int accountcodemappingid { get; set; }
        public string accountcode { get; set; }
        public int moon { get; set; }
        public int year { get; set; }

    }


}
