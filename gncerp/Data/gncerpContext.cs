using gncerp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Data
{
    public class gncerpContext: DbContext
    {

        public gncerpContext(DbContextOptions<gncerpContext> options) : base(options) { }

        //public DbSet<Products> Products { get; set; }
        public DbSet<Appuser> appusers { get; set; }
    }

}
