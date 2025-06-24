using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class OgrencilerController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();

        public ActionResult Liste()
        {
            var ogrenciler = db.TBLOGRENCILER.Where(k => k.AKTİFLİK == true).ToList();
            return View(ogrenciler);
        }

        [HttpGet]
        public ActionResult OgrenciEkle()
        {
            //linq yazdık ve verileri çektik. 
            ViewBag.Kulupler = db.TBLKULUPLER.Where(k=>k.AKTİFLİK==true).Select(k => new SelectListItem
            {
                Text = k.KULUPAD,
                Value = k.KULUPID.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult OgrenciEkle(TBLOGRENCILER p)
        {
            if(!ModelState.IsValid)
            {
                
                return View(p);
            }
            var kulup = db.TBLKULUPLER.Where(m => m.KULUPID == p.OGRKULUP).Where(m=>m.AKTİFLİK==true).FirstOrDefault();
            p.TBLKULUPLER = kulup;

            var dersListesi = db.TBLDERSLER.Where(k => k.AKTİFLİK == true).ToList();

            foreach( var ders in dersListesi)
            {
                var yeniNot = new TBLNOTLAR
                {
                    OGRID = p.OGRID,
                    DERSID = ders.DERSID,
                    SINAV1 = 0,
                    SINAV2 = 0,
                    SINAV3 = 0,
                    PROJE = 0,
                    ORTALAMA = 0
                };
                db.TBLNOTLAR.Add(yeniNot);
               

            }
            
            db.TBLOGRENCILER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Liste");
        }

        public ActionResult OgrenciSil(int id)
        {
            var ogr = db.TBLOGRENCILER.Find(id);
            ogr.AKTİFLİK = false;

            var notlar = db.TBLNOTLAR.Where(k => k.OGRID == id);
            foreach(var not in notlar)
            {
                not.AKTİFLİK = false;
            }
            db.SaveChanges();
            return RedirectToAction("Liste");
        }

        public ActionResult OgrenciGuncelle(int id)
        {
            var ogr = db.TBLOGRENCILER.Find(id);
            //linq yazdık ve verileri çektik. 
            ViewBag.Kulupler = db.TBLKULUPLER.Where(k=>k.AKTİFLİK==true).Select(k => new SelectListItem
            {
                Text = k.KULUPAD,
                Value = k.KULUPID.ToString(),
                Selected=(k.KULUPID==ogr.OGRKULUP)
                
            }).ToList();
            return View(ogr);
        }


        [HttpPost]
        public ActionResult OgrenciGuncelle(TBLOGRENCILER model)
        {
            var ogrenci = db.TBLOGRENCILER.Find(model.OGRID);
            if (ogrenci == null)
                return HttpNotFound();

            ogrenci.OGRAD = model.OGRAD;
            ogrenci.OGRSOYAD = model.OGRSOYAD;
            ogrenci.OGRRESIM = model.OGRRESIM;
            ogrenci.OGRKULUP = model.OGRKULUP;

            db.SaveChanges();

            return RedirectToAction("Liste");
        }
    
    }
}