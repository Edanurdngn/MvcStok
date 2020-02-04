using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Filters;
using MvcStok.Models;
using MvcStok.Models.Entity;
namespace MvcStok.Controllers
{
    [AuthenticationFilter]
    public class SatisController : Controller
    {
        // GET: Satis
        MvsDbstokEntities1 db = new MvsDbstokEntities1();
        public ActionResult Satis()
        {
            var satislar = db.TBLSATIS.Select(x => new SatisDto()
            {
                SATISID = x.SATISID,
                URUN = x.TBLURUNLER.URUNAD,
                MUSTERI = x.TBLMUSTERILER.MUSTERIAD,
                ADET = x.ADET,
                FIYAT = x.FIYAT,
                TUTAR = x.TUTAR
            }).ToList();

            ViewBag.Sum = db.TBLKASA.Select(x => x.KASATOPLAM).Sum();

            return View(satislar);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {


            List<SelectListItem> urunsatis = (from i in db.TBLURUNLER.ToList()
                                              where i.KARALISTEDEMI == false
                                              select new SelectListItem
                                              {
                                                  Text = i.URUNAD,
                                                  Value = i.URUNID.ToString()
                                              }).ToList();
            ViewBag.dgr = urunsatis;

            List<SelectListItem> musteriid = (from mst in db.TBLMUSTERILER.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = mst.MUSTERIAD,
                                                  Value = mst.MUSTERIID.ToString()
                                              }).ToList();
            ViewBag.dgr2 = musteriid;


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YeniSatis(TBLSATIS p2)
        {
            List<SelectListItem> degerler = (from i in db.TBLURUNLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.URUNAD,
                                                 Value = i.URUNID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            List<SelectListItem> musteriid = (from mst in db.TBLMUSTERILER.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = mst.MUSTERIAD,
                                                  Value = mst.MUSTERIID.ToString()
                                              }).ToList();
            ViewBag.dgr2 = musteriid;


            if (!ModelState.IsValid)
            {
                return View(p2);
            }


            var stokazalt = db.TBLURUNLER.Find(p2.URUN);
            int fark = Convert.ToInt32(stokazalt.STOK) - Convert.ToInt32(p2.ADET);
            p2.TUTAR = Convert.ToInt32(p2.ADET * p2.FIYAT);
            if (stokazalt != null)
            {
                stokazalt.STOK = Convert.ToByte(fark);
            }
            int tutar = p2.TUTAR;
            db.TBLSATIS.Add(p2);

            var kasaekle = db.TBLKASA.Create();
            {
                //kasaekle.SATISID = ;
                kasaekle.DEFO = 0;
                kasaekle.KASATOPLAM = p2.TUTAR;
                kasaekle.ISLEMTARIHI = DateTime.Now;
            }
            db.TBLKASA.Add(kasaekle);

            db.SaveChanges();
            return RedirectToAction("Satis", "Satis");

        }

        public ActionResult SIL(int id)
        {
            var satis = db.TBLSATIS.Find(id);
            db.TBLSATIS.Remove(satis);
            TBLKASA kasasil = db.TBLKASA.FirstOrDefault(x => x.SATISID == satis.SATISID );
            { db.TBLKASA.Remove(kasasil);  }
            db.SaveChanges();
            return RedirectToAction("Satis");
        }
        [HttpGet]
        public ActionResult SatisGuncelle(int id)
        {
            var urun = db.TBLSATIS.Find(id);
            List<SelectListItem> urunıd = (from i in db.TBLURUNLER.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.URUNAD,
                                               Value = i.URUNID.ToString()
                                           }).ToList();
            ViewBag.dgr = urunıd;
            var musteri = db.TBLSATIS.Find(id);
            List<SelectListItem> musteriid = (from mst in db.TBLMUSTERILER.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = mst.MUSTERIAD,
                                                  Value = mst.MUSTERIID.ToString()
                                              }).ToList();
            ViewBag.dgr2 = musteriid;
            return View(musteri);
            return View(urun);

        }
        [HttpPost]
        public ActionResult SatisGuncelle(TBLSATIS satısgun)
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
                var satislar = db.TBLSATIS.Find(satısgun.SATISID);
                if (satislar != null)
                {
                    satislar.SATISID = satısgun.SATISID;
                    satislar.URUN = satısgun.URUN;
                    satislar.MUSTERI = satısgun.MUSTERI;
                    satislar.FIYAT = satısgun.FIYAT;
                    satislar.ADET = satısgun.ADET;


                    db.SaveChanges();
                    return RedirectToAction("Satis", "Satis");
                }
            }
            return View("Error");

        }
        public ActionResult Guncelle(TBLSATIS p1)
        {
            var sts = db.TBLSATIS.Find(p1.SATISID);

            sts.URUN = p1.URUN;
            sts.MUSTERI = p1.MUSTERI;
            sts.ADET = p1.ADET;
            sts.FIYAT = p1.FIYAT;

            db.SaveChanges();
            return RedirectToAction("Satis");
        }
    }
}

