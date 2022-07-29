using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Entities
{
    public class Fiyatlistesi
    {
        public string malzemekodu { get; set; }
        public string malzemeadi { get; set; }
        public string malzemedrupkodu { get; set; }
        public string markakodu { get; set; }
        public string model { get; set; }
        public string fiyatlistname { get; set; }
        public string firstchart { get; set; }
        public decimal fiyatlist { get; set; }
        public double stok { get; set; }

    }
}
