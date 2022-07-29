using gncerp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Models
{
    public class pazaryeriraporModel
    {
        public string pazaryeri { get; set; }
        public int marka { get; set; }
        public double adet { get; set; }
        public double ciro { get; set; }
        public double kar { get; set; }
        public int cesit { get; set; }

    }
}
