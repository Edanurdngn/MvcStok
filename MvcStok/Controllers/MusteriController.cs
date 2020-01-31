using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Filters;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MvcStok.Controllers
{
    [AuthenticationFilter]
    public class MusteriController : Controller
    {
        MvsDbstokEntities1 db = new MvsDbstokEntities1();
        public ActionResult Musteri(int sayfa = 1)
        {
            //var degerler = db.TBLMUSTERILER.ToList();
            var degerler = db.TBLMUSTERILER.ToList().ToPagedList(sayfa, 6);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERILER p3)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TBLMUSTERILER.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Musteri", "Musteri");

        }

        public ActionResult SIL(int id)
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Musteri", "Musteri");
        }
        [HttpGet]
        public ActionResult MusteriGuncelle(int id)
        {
            var mst = db.TBLMUSTERILER.Find(id);
            return View( mst);
        }
        public ActionResult Guncelle(TBLMUSTERILER p1)
        {
            var must = db.TBLMUSTERILER.Find(p1.MUSTERIID);
            must.MUSTERIAD = p1.MUSTERIAD;
            must.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Musteri", "Musteri");
        }
    }
}