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
    public class KaraListeController : Controller
    {
        // GET: KaraListe
        MvsDbstokEntities1 db = new MvsDbstokEntities1();
        public ActionResult KaraListe()
        {
       
            var model = (from liste in db.TBLKARALISTE
                         join
                        urun in db.TBLURUNLER on
                        liste.URUNID equals urun.URUNID
                        where liste.KALDIRILDIMI==false
                         select new KaraListeDto
                         {
                             KARALISTEID=liste.KARALISTEID,
                             URUNADİ = urun.URUNAD,
                             GIRISTARIHI = (DateTime)liste.GIRISTARIHI,
                             CIKISTARIHI = (DateTime)liste.CIKISTARIHI,
                             ACIKLAMA1 = liste.ACIKLAMA1,
                             ACIKLAMA2 = liste.ACIKLAMA2,
                         }).ToList();
            return View(model);
        }
        public JsonResult KaraListedenKaldir(KaraListeKaldirDto karaListeKaldirDto )
        {
            var liste = db.TBLKARALISTE.FirstOrDefault(x=> x.URUNID == karaListeKaldirDto.URUNID && x.KALDIRILDIMI==false);
            if (liste !=null)
            {
                liste.KARALISTEID = liste.KARALISTEID;
                liste.KALDIRILDIMI = true;
                liste.ACIKLAMA2 = karaListeKaldirDto.ACİKLAMA2;
                liste.CIKISTARIHI = DateTime.Now;
                db.SaveChanges();

                var urun = db.TBLURUNLER.FirstOrDefault(x => x.URUNID == liste.URUNID);
                if (urun == null)
                {
                    return Json(new { result = "Ürün bulunamadı", success = false });
                }
                else
                {
                    urun.KARALISTEDEMI = false;
                    db.SaveChanges();
                }
                return Json(new { result = "İşlem Başarılı", success = true });
            }
            else
            {
                return Json(new { result = "Beklenmedik bir hata oluştu", success = false });
            }
           
        }
        public JsonResult UrunGetir(int id)
        {
            using (var context = new MvsDbstokEntities1())
            {
                var karalisteId = context.TBLKARALISTE.FirstOrDefault(x => x.KARALISTEID == id);
                if (karalisteId !=null)
                {
                    var model = (from urun in context.TBLURUNLER
                                 join
                                kategori in context.TBLKATEGORILER on
                                urun.URUNKATEGORI equals kategori.KATEGORIID join  liste in  context.TBLKARALISTE on  
                                 urun.URUNID equals liste.URUNID
                                 where urun.URUNID == karalisteId.URUNID
                                 select new UrunDto
                                 {
                                     Fiyat = urun.FIYAT,
                                     KategoriAd = kategori.KATEGORIADI,
                                     Marka = urun.MARKA,
                                     MinStok = urun.MINSTOK,
                                     Stok = urun.STOK,
                                     UrunAd = urun.URUNAD,
                                     UrunId = urun.URUNID,
                                     Aciklama1=liste.ACIKLAMA1,
                                     Kaldirildimi=(bool)liste.KALDIRILDIMI
                                     
                                 }).ToList();
                    var entity = model.Where(x => x.Kaldirildimi == false).FirstOrDefault();
                    return Json(new { result = entity, success = true });
                }
                else
                {
                    return Json(new { result = "Ürün bulunamadı", success = false });
                }
                
            }
        }
    }

   public class KaraListeKaldirDto
    {
        public int URUNID { get; set; }
        public string ACİKLAMA2 { get; set; }
    }

}