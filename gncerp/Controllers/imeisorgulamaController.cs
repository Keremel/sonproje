using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gncerp.Models;
using gncerp.Utility;
using Microsoft.AspNetCore.Mvc;

namespace gncerp.Controllers
{
    public class imeisorgulamaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    

 
        [HttpPost]
        public Result<string> sorgula([FromBody]dynamic request)
        {
            Result<string> result = new Result<string>();

            try
            { string IMEI = request.id;
                dynamic model = string.Format(@"SELECT * FROM ARY_XXX_IMEI_BAZLI_SATIS  WHERE IMEI = '{0}'",IMEI).GetDynamicQuery("SCSlogo");

                if (model.Count == 0)
                {
                    result.message = "0";
                }
                else
                {
                    result.message = "1";
                }
             
                   result.status = true;
                


            }
            catch (Exception Ex)
            {
                result.message = Ex.Message;
                result.status = false;
            }

            return result;

        }
    }
}