using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Entities
{
    public class Deneme
    {
        [Key]
       public int id { get; set; }
       public string test { get; set; }
    }
}
