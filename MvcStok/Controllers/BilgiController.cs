using MvcStok.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{

    [AuthenticationFilter]
    public class BilgiController : Controller
    {
        MvsDbstokEntities1 db = new MvsDbstokEntities1();
        // GET: Bilgi
        public ActionResult Bilgi()
        {
            return View();
        }
        
    }
}