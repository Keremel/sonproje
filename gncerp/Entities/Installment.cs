using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Entities
{
    public class Installment
    {
        public int id { get; set; }
        public DateTime paymentdate { get; set; }
        public int orders { get; set; }
        public decimal amount { get; set; }
        public int payplanid { get; set; }
    }
}
