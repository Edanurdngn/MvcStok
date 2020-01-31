using MvcStok.Models;
using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcStok.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnurl)
        {
            using (MvsDbstokEntities1 db = new MvsDbstokEntities1())
            {


                if (ModelState.IsValid)
                {

                    TBLKULLANICI kullanici = db.TBLKULLANICI.Where
                        (x => x.Email.ToLower().Trim() == model.Email.ToLower().Trim() && x.Sifre.ToLower().Trim() == model.Sifre.ToLower().Trim()).FirstOrDefault();
                    if (kullanici != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        FormsAuthentication.RedirectFromLoginPage(model.Email, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View(model);

            }
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

    }

}