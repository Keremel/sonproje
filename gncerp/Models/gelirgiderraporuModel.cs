using gncerp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Models
{
    public class GelirgiderraporuModel
    {
        public Accountcodemapping accountcodemapping { get; set; }


       public List<TGelir> tgider { get; set; }

        public List<GelirgiderraporuModel> personel_giderler { get; set; }

        public List<Budget> budget { get; set; }
    }
}
