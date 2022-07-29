using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using gncerp.Entities;
using gncerp.Models;
using gncerp.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gncerp.Controllers
{
    public class accountController : Controller
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public Result<string> Login([FromBody]loginModel model)
        {
            Result<string> result = new Result<string>();
            if (ModelState.IsValid)
            {

                List<Appuser> _user = string.Format("SELECT * FROM appusers WHERE email='{0}' AND password='{1}'  AND ispsv=0 AND isdel=0", model.email, model.password).GetQuery<Appuser>();

                if (_user.Count == 0)
                {
                    result.message = "Kullanıcı bulunamadı ya da şifre yanlış!";
                    result.status = false;
                    return result;
                }

             

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, _user[0].id.ToString()));
                identity.AddClaim(new Claim("name", _user[0].name));
                identity.AddClaim(new Claim("surname", _user[0].surname));
                identity.AddClaim(new Claim("username", _user[0].username));
                identity.AddClaim(new Claim(ClaimTypes.Email, _user[0].email));
                identity.AddClaim(new Claim("tel", _user[0].tel));
                identity.AddClaim(new Claim(ClaimTypes.Role, _user[0].role));

                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                props.IsPersistent = model.rememberMe;

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

                result.status = true;
                return result;

            }

            result.message = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
            result.status = false;
            return result;
        }



        [HttpPost]
        [HttpGet]
        public IActionResult Logout()
        {
            ResultV result = new ResultV();
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("currentuser");

            //ResultV alertVM = new ResultV();
            //alertVM.redirectingUrl = "/account/Login";
            //alertVM.title = "İşlem Başarılı";
            //alertVM.redirectingTimeout = 2000;
            return View("Login");
        }


    }
}