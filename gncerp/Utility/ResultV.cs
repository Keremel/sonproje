using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace gncerp.Utility
{
    public class ResultV
    {
     
        public string title { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public bool IsRedirecting { get; set; }
        public string redirectingUrl { get; set; }
        public int redirectingTimeout { get; set; }
       
        public ResultV()
        {
            title = "İşlem Başarılı...";
            text = "Yönlendiriliyorsunuz";
            IsRedirecting = true;
            redirectingUrl = "/Home/Index";
            redirectingTimeout = 15000;
            icon = "success";
        }
    }
 
}
