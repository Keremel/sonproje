using gncerp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Models
{
    public class aylikkarneModel
    {
        public string ay { get; set; }
        public int yil { get; set; }

        public List<Aylikkarne> aylikkarnes { get; set; }


    }
}
