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
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvsDbstokEntities1 db = new MvsDbstokEntities1();
        public ActionResult Index(int sayfa = 1)
        {
            //  var degerler = db.TBLKATEGORILER.ToList();
            var degerler = db.TBLKATEGORILER.OrderByDescending(x=> x.KATEGORIID).ToList().ToPagedList(sayfa, 6);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View(p1);
            }
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index", "Kategori");

        }

        public ActionResult SIL(int id)
        {
            var kategori = db.TBLKATEGORILER.Find(id);
            if (kategori == null)
            {
                return View("Error");
            }
            db.TBLKATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORILER.Find(id);
            return View(ktgr);
        }

        [HttpPost]
        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var ktg = db.TBLKATEGORILER.Find(p1.KATEGORIID);
            if (ktg==null)
            {
                return View("Error");
            }
            ktg.KATEGORIID = p1.KATEGORIID;
            ktg.KATEGORIADI = p1.KATEGORIADI;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}