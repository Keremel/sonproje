using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace gncerp.Controllers
{
    public class banksController : Controller
    {
        public IActionResult list(int id=1)
        {

            dynamic modellist = string.Format("  SELECT * from Accountnos a JOIN Banktype b ON a.bankid=b.id WHERE a.id={0}", id).GetDynamicQuery();
            return View(modellist);
        }
    }
}
