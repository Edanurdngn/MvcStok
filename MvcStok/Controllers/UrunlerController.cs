using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    public class UrunlerController : Controller
    {

        MvsDbstokEntities1 db = new MvsDbstokEntities1();
        public ActionResult Urunler()
        {
            var degerler = db.TBLURUNLER.Select(x => new UrunlerDto()
            {
                FIYAT = x.FIYAT,
                KATEGORİADİ = x.TBLKATEGORILER.KATEGORIADI,
                MARKA = x.MARKA,
                STOK = x.STOK,
                URUNAD = x.URUNAD,
                URUNID = x.URUNID
            }
            ).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIADI,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniUrun(TBLURUNLER p2)
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIADI,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            if (!ModelState.IsValid)
            {
                return View(p2);
            }
            db.TBLURUNLER.Add(p2);
            db.SaveChanges();
           
            return RedirectToAction("Urunler", "Urunler");

        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Urunler");
        }
        [HttpGet]
        public ActionResult UrunGuncelle(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIADI,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View(urun);
        }

        [HttpPost]
        public ActionResult UrunGuncelle(TBLURUNLER p1)
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIADI,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
          
            if (ModelState.IsValid)
            {
                var urun = db.TBLURUNLER.Find(p1.URUNID);
                if (urun!=null)
                {
                    urun.URUNID = p1.URUNID;
                    urun.URUNAD = p1.URUNAD;
                    urun.MARKA = p1.MARKA;
                    urun.STOK = p1.STOK;
                    urun.FIYAT = p1.FIYAT;
                    urun.URUNKATEGORI = p1.URUNKATEGORI;
                    db.SaveChanges();
                    return RedirectToAction("Urunler","Urunler");
                }
            }
            return View("Error");
          
        }
    }
}