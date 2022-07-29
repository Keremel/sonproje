using gncerp.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Controllers
{
    public class GTSController : Controller
    {
        public IActionResult gtstemsilci()
        {
            return View();
        } 
        public IActionResult gtsiparis()
        {
            //ViewBag.id = id;

            return View();
        }


        [Route("{controller}/datatables")]
        [HttpPost]
        public IActionResult datatables([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            requestobj.dbtype = "SCSlogo";
        
            try
            {
                //var results = await _demoService.GetDataAsync(param);
                return new JsonResult(new DatatablesJS.DataTablesObjectResult().getresults(requestobj));
                //return new JsonResult(new { error = "aaa" });
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }

        }

    }
}
