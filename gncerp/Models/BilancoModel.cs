using gncerp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Models
{
    public class BilancoModel
    {
        public List<Bilanco> Bilanco_List_Aktif { get; set; }  
        public List<Bilanco> Bilanco_List_Pasif { get; set; }
        public List<Bilanco> Digerler { get; set; }

    }
}
