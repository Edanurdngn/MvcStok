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
    public class UrunlerController : Controller
    {
        MvsDbstokEntities1 db = new MvsDbstokEntities1();

        
        public ActionResult Urunler()
        {


            var degerler = db.TBLURUNLER.Where(x => x.KARALISTEDEMI == false).Select(x => new UrunlerDto()
            {
                FIYAT = x.FIYAT,
                KATEGORİADİ = x.TBLKATEGORILER.KATEGORIADI,
                MARKA = x.MARKA,
                STOK = x.STOK,
                URUNAD = x.URUNAD,
                URUNID = x.URUNID,
                MINSTOK = x.MINSTOK
            }


                ).OrderByDescending(x => x.URUNID).ToList();
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
            p2.KARALISTEDEMI = false;
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
                if (urun != null)
                {
                    urun.URUNID = p1.URUNID;
                    urun.URUNAD = p1.URUNAD;
                    urun.MARKA = p1.MARKA;
                    urun.STOK = p1.STOK;
                    urun.FIYAT = p1.FIYAT;
                    urun.URUNKATEGORI = p1.URUNKATEGORI;
                    db.SaveChanges();
                    return RedirectToAction("Urunler", "Urunler");
                }
            }
            return View("Error");

        }
        [HttpPost]
        public JsonResult KaraListe(KaraListeDto karaListeDto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { result = "Girilen alanları kontrol ediniz", success = false });
                }
                TBLKARALISTE dahaOnceEklendiMi = db.TBLKARALISTE.FirstOrDefault(x => x.URUNID == karaListeDto.URUNID && x.KALDIRILDIMI == false);
                if (dahaOnceEklendiMi != null)
                {
                    return Json(new { result = "Ürün daha önce kara listeye eklenmiş", success = false });
                }
                TBLURUNLER urun = db.TBLURUNLER.FirstOrDefault(x => x.URUNID == karaListeDto.URUNID);
                if (urun == null)
                {
                    return Json(new { result = "Ürün bulunamadı", success = false });
                }

                var addModel = new TBLKARALISTE
                {
                    URUNID = karaListeDto.URUNID,
                    ACIKLAMA1 = karaListeDto.ACIKLAMA1,
                    GIRISTARIHI = karaListeDto.GIRISTARIHI,
                    CIKISTARIHI = karaListeDto.CIKISTARIHI,
                    ACIKLAMA2 = karaListeDto.ACIKLAMA2,
                    KALDIRILDIMI = karaListeDto.KALDIRILDIMI == "False"
                };
                db.TBLKARALISTE.Add(addModel);
                db.SaveChanges();
                urun.KARALISTEDEMI = true;
                db.SaveChanges();
                return Json(new { result = "İşlem başarılı", success = true });
            }
            catch (Exception exception)
            {
                return Json(new { result = exception.Message, success = false });
            }
        }

        public JsonResult GetJsonResult()
        {
            var result = db.TBLKARALISTE.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UrunGetir(int id)
        {

            using (var context = new MvsDbstokEntities1())
            {

                var model = (from urun in context.TBLURUNLER
                             join
                            kategori in context.TBLKATEGORILER on
                            urun.URUNKATEGORI equals kategori.KATEGORIID
                             where urun.URUNID == id
                             select new UrunDto
                             {
                                 Fiyat = urun.FIYAT,
                                 KategoriAd = kategori.KATEGORIADI,
                                 Marka = urun.MARKA,
                                 MinStok = urun.MINSTOK,
                                 Stok = urun.STOK,
                                 UrunAd = urun.URUNAD,
                                 UrunId = urun.URUNID
                             }).ToList();

                return Json(new { result = model, success = true });
            }
        }

    }
}