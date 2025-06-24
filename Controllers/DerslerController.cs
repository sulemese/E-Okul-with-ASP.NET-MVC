using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class DerslerController : Controller
    {


        DbMvcOkulEntities db = new DbMvcOkulEntities(); //veri tabanıma ulaşmak için kullanacağım nesnem
       

        
        public ActionResult Liste()
        {
            //aktif kulüpleri listeler
            var dersler = db.TBLDERSLER.Where(k => k.AKTİFLİK == true).ToList();
            return View(dersler);
        }


        

        [HttpGet] //boş formu döner
        public ActionResult DersEkle()
        {
            
            return View();
        }

        [HttpPost]  
        public ActionResult DersEkle(TBLDERSLER p)
        {
            db.TBLDERSLER.Add(p);
            db.SaveChanges();
            return View();
        }

        //dersi soft delete yöntemi ile siler. durumunu false yaparak pasif hale getirir.

        public ActionResult DersSil(int id)
        {
            var ders = db.TBLDERSLER.Find(id);
            if (ders == null)
            {
                return HttpNotFound();
            }
            else
            {
                ders.AKTİFLİK = false;
                db.SaveChanges();
                return RedirectToAction("Liste");
            }
        }

        public ActionResult DersGuncelle(int id)
        {
            var drs = db.TBLDERSLER.Find(id);
            return View(drs);
        }

        [HttpPost]
        public ActionResult DersGuncelle(TBLDERSLER model)
        {
            var drs = db.TBLDERSLER.Find(model.DERSID);
            if (drs == null)
                return HttpNotFound();
            drs.DERSAD = model.DERSAD;
            drs.HAFTALIK_SAAT = model.HAFTALIK_SAAT;
            db.SaveChanges();
            return RedirectToAction("Liste");
        }
    }
}