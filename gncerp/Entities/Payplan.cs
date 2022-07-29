using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Entities
{
    public class Payplan
    {
        public int id { get; set; }
        public int paymentmodeid { get; set; }
        public string institution { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }


        public int day { get; set; }
        public float bankrate { get; set; }
        public decimal creditmainmoney { get; set; }
        public decimal creditinterest { get; set; }
        public decimal creditbsmv { get; set; }
        public decimal continuationrate { get; set; }
        public decimal continuationmainmoney { get; set; }
        public decimal continuationbsmv { get; set; }
        public decimal continuationtotal { get; set; }
        public decimal generaltotal { get; set; }


    }
}
