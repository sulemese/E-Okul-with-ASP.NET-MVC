using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
using OgrenciNotMvc.Models;
using System.Data.Entity;

namespace OgrenciNotMvc.Controllers
{
    public class KulüplerController : Controller
    {
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Liste()
        {
           
            //aktif kulüpleri listeler
            var kulüpler = db.TBLKULUPLER.Where(k => k.AKTİFLİK == true).ToList();
            return View(kulüpler);
        }


        //yeni kulüp ekler
        [HttpGet]
        public ActionResult KulüpEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KulüpEkle(TBLKULUPLER p)
        {
            db.TBLKULUPLER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Liste");
        }

        
        //kulübü soft delete yöntemi ile siler. durumunu false yaparak pasif hale getirir. 
        public ActionResult KulüpSil(int id)
        {
            var kulup = db.TBLKULUPLER.Find(id);
            if (kulup == null)
            {
                return HttpNotFound();
            }
            else 
            {
                kulup.AKTİFLİK = false;
                db.SaveChanges();
                return RedirectToAction("Liste");
            }
           
        }

      
        [HttpGet]
        public ActionResult ÖğrenciKulüpKayıt(int? kulüpId)
        {

            //kulüp dropdownlist için kulüpleri liste halinde çektik. 
            ViewBag.Kulüpler = db.TBLKULUPLER
                .Where(k => k.AKTİFLİK == true)
                .Select(d => new SelectListItem
                {
                    Text = d.KULUPAD,
                    Value = d.KULUPID.ToString()
                })
                .ToList();

            //kulüp seçimi yapılmamışsa boş view döndürür.
            if (kulüpId == null)
            {
                return View();
            }
            //eğer kulüp seçimi yapılmışsa aktif öğrencilerin listesin, görüntüleyelim. 
            else
            {
                ViewBag.kulubekayıtlıogrencıleridleri = db.TBLOGRENCILER
                    .Where(k => k.AKTİFLİK == true && k.OGRKULUP == kulüpId)
                    .Select(k => k.OGRID)
                    .ToList(); 
                   
                    
                    
                    
                //şimdi notları(öğrenci bilgileri ile birlikte )view ekranına aktar.
                var öğrenciler = db.TBLOGRENCILER
                    .Where(k=> k.AKTİFLİK==true)                   
                    .ToList();
                return View(öğrenciler);
            }

            /*
            var model = new KulupAtamaViewModel
            {
                Ogrenciler = db.TBLOGRENCILER.ToList(),
                Kulupler = db.TBLKULUPLER.Where(k => k.AKTİFLİK == true).ToList()
            };

            return View(model);
            */
        }

        [HttpPost]
        public ActionResult ÖğrenciKulüpKayıt(KulupAtamaViewModel model)
        {
            if (model.SecilenKulupId != null && model.SecilenOgrenciIdleri != null)
            {
                foreach (var id in model.SecilenOgrenciIdleri)
                {
                    var ogr = db.TBLOGRENCILER.Find(id);
                    if (ogr != null)
                    {
                        ogr.OGRKULUP = model.SecilenKulupId;
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("ÖğrenciKulüpKayıt");
        }

        public ActionResult KulüpGuncelle(int id)
        {
            var klp = db.TBLKULUPLER.Find(id);
            return View(klp);
        }


        [HttpPost]
        public ActionResult KulüpGuncelle(TBLKULUPLER model)
        {
            var klp = db.TBLKULUPLER.Find(model.KULUPID);
            if (klp == null)
                return HttpNotFound();
            klp.KULUPAD = model.KULUPAD;
            db.SaveChanges();
            return RedirectToAction("Liste");
        }

    }
}