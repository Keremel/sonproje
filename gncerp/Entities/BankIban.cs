using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Entities
{
    public class BankIban
    {
        public int id { get; set; }
        public string bankname { get; set; }
        public string iban { get; set; }
        public string subecodu { get; set; }
        public string accountno { get; set; }
    }

}
