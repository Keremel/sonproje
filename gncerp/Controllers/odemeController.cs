using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gncerp.Entities;
using Microsoft.AspNetCore.Mvc;
using gncerp.Models;
using System.Data.SqlClient;
using gncerp.Utility;
using gncerp.Models.OdemeModel;

namespace gncerp.Controllers
{
    public class odemeController : Controller
    {
        public IActionResult ziraatindex()
        {
            return View();
        }

        [HttpPost]
         public IActionResult ziraatindex(ZiraatModel Model)
        {
            return View();
        } 


        public IActionResult ziraatonay()
        {
            return View();
        }

        public ZiraatModel OdemeDetay(ZiraatModel Model)
        {

            Random rnd = new Random();
         
            Model.storetype = "3d";
            Model.clientId = "190261817";
            Model.storekey = "Ny2erd2020";
            Model.lang = "tr";
         
            Model.rnd = rnd.Next(1234, 100000).ToString();
            Model.okUrl = "http://localhost:58159/odeme/okUrl";
            Model.failUrl = "http://localhost:58159/odeme/failUrl";

            string hashstr = Model.clientId + Model.oid + Model.amount + Model.okUrl + Model.failUrl + Model.islemtipi + Model.taksit + Model.rnd + Model.storekey;

            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] hashbytes = System.Text.Encoding.GetEncoding("ISO-8859-9").GetBytes(hashstr);
            byte[] inputbytes = sha.ComputeHash(hashbytes);

            string hash = Convert.ToBase64String(inputbytes);

            Model.hash = hash;

            return Model;


        }

 

        [HttpPost]
        public IActionResult okUrl(dynamic model)
        {
            return View();
        }


        [HttpPost]
        public IActionResult failUrl(dynamic model)
        {
            return View();
        }
     


       
    }
}