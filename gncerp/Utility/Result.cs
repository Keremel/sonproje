using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Utility
{
    public class Result<T>
    {
       
        public List<T> items { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string jsonObj { get; set; }


    
        public Result()
        {
            items =new List<T>();
            status = true;       
        }

      
    }
   
}
