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

    public class HomeController : Controller
    {
        MvsDbstokEntities1 db = new MvsDbstokEntities1();
        public ActionResult Index()
        {    
            ViewBag.Sum = db.TBLKASA.Select(x => x.KASATOPLAM).Sum();
            ViewBag.Count1 = db.TBLSATIS.Count();
            ViewBag.Count2 = db.TBLURUNLER.Count();
            ViewBag.Count3 = db.TBLMUSTERILER.Count();
            ViewBag.Count4 = db.TBLKATEGORILER.Count();
            return View();
        }

        
    }
}