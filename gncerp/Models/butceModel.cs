using gncerp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Models
{
    public class butceModel
    {
        public Accountcodemapping accountcodemapping { get; set; }

        public List<Budget> budget { get; set; }
    }
}
