using gncerp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Services
{
    public class dbServices
    {

        public static Appdefinition appdefinition()
        {
            return string.Format("Select * from Appdefinition").GetQuery<Appdefinition>()[0];
        }
    }
}
