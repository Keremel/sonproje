using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Entities
{
    public class Personelperformas
    {
        public string kullanici { get; set; }
        public DateTime tarihdate { get; set; }
        public string tarih { get; set; }
        public double adet { get; set; }
        public double ciro { get; set; }
        public int cesit { get; set; }
        public int bayi { get; set; }
        public int firma { get; set; }
        public double penetrasyon { get; set; }
    }
}
