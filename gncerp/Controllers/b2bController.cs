using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using gncerp.Models;
using gncerp.Services;
using gncerp.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gncerp.Controllers
{
    //[Authorize(Roles = "cari,user")]
    public class b2bController : Controller
    {
        yurticikargoServices _yurticikargoServices = new yurticikargoServices();
        public IActionResult Index()
        {
            //dynamic model = _yurticikargoServices.getdetay("DEP2020000008");



            return View();
        }
        



        public IActionResult carihesapekstresi()
        {
            return View();
        }  
        public IActionResult babsformum()
        {
            return View();
        }    
        public IActionResult satisrf()
        {
            return View();
        }    
        
        public IActionResult adresdilgilerim()
        {
            dynamic model = string.Format(@"Select  cari.[Kişi / Kurum Adı (*)] adi,cari.[Adresi (*)] adresi,cari.İl il,cari.İlçe ilce,cari.[Telefon Cep] telcep,cari.[Telefon Ev/İş ] televis,cari.[E-Mail] email
 from ARY_017_CARI_LISTESI_TG cari WHERE cari.[Cari Kodu]='{0}'", CurrentSession.Caricodu).GetDynamicQuery("SCSlogo");


            return View(model);
        } 

        public IActionResult satisytemsilcim()
        {
            dynamic model = string.Format(@"select f.[Satış Temsilcisi] st from ARY_017_CARI_MUSTER_TEMSILCI f WHERE f.[Cari Kodu]='{0}'", CurrentSession.Caricodu).GetDynamicQuery("SCSlogo");

            return View(model);
        }


        [HttpPost]
        [HttpGet]
        public IActionResult Logout()
        {

            HttpContext.SignOutAsync();

            return View("Login"); 
        }


        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public Result<string> Login([FromBody]B2BLoginModel model)
        {


            Result<string> result = new Result<string>();

            List<B2BLoginModel> cari = string.Format(@"SELECT cari.[Cari Kodu] carikodu , cari.[Kişi / Kurum Adı (*)] isim,cari.[E-Mail] email,cari.[Vergi No] vergino FROM ARY_017_CARI_LISTESI_TG cari WHERE cari.[E-Mail]='{0}' AND cari.[Vergi No]='{1}'",model.email,model.vergino).GetQuery<B2BLoginModel>("SCSlogo");
            //dynamic system_setting = string.Format("SELECT * FROM system_settings WHERE siteurl='{0}'", ConfigurationManager.AppSettings["siteurl"].ToString()).GetDynamicQuery();

            if (cari.Count > 0)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                //current user bilgileri
                identity.AddClaim(new Claim(ClaimTypes.Name, cari[0].isim));
                identity.AddClaim(new Claim(ClaimTypes.Email, cari[0].email));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, cari[0].carikodu));


                IEnumerable<string> roles = new List<string>();
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim(ClaimTypes.Role, "cari"));


                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                result.status = true;
                result.message = "İşlem Başarılı" +HttpContext.User.Identity.Name;


                return result;
            }
            else
            {
                result.status = false;
                result.message = "Kullanıcı Bulunamdı";

                return result;
            }
                

        }


        [Route("{controller}/datatables")]
        [HttpPost]
        public IActionResult datatables([FromBody]DatatablesJS.DatatablesObject requestobj)
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