using gncerp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Models
{
    public class alislarModel
    {
        public List<Alis> alislar { get; set; }
        public IEnumerable<string> markalar { get; set; }
        public int gunselect { get; set; }


    }
}
