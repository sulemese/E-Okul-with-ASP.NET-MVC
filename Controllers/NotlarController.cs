using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;
using System.Data.Entity;

namespace OgrenciNotMvc.Controllers
{
    public class NotlarController : Controller
    {
        // GET: Notlar
        DbMvcOkulEntities db = new DbMvcOkulEntities();
        public ActionResult Liste(int? dersId)
        {
            //ders dropdownlist için dersleri liste halinde çektik. 
            ViewBag.Dersler = db.TBLDERSLER
                .Where(k => k.AKTİFLİK == true)
                .Select(d => new SelectListItem
                {
                    Text = d.DERSAD,
                    Value = d.DERSID.ToString()
                })
                .ToList();

            //ders seçimi yapılmamışsa boş view döndürür.
            if (dersId == null)
            {
                return View();
            }

            //eğer ders seçimi yapılmışsa o dersin notlarını (öğrenci ile birlikte) görüntüleyelim. 
            else
            {
                //şimdi notları(öğrenci bilgileri ile birlikte )view ekranına aktar.
                var notlar = db.TBLNOTLAR
                    .Where(k => k.DERSID == dersId)
                    .Include(o => o.TBLOGRENCILER)
                    .ToList();
                return View(notlar);
            }
        }

        [HttpGet]
        public ActionResult NotGir(int? dersId)
        {
            //ders dropdownlist için dersleri liste halinde çektik. 
            ViewBag.Dersler = db.TBLDERSLER
                .Where(k => k.AKTİFLİK == true)
                .Select(d => new SelectListItem
                {
                    Text = d.DERSAD,
                    Value = d.DERSID.ToString()
                })
                .ToList();

            //ders seçimi yapılmamışsa boş view döndürür.
            if (dersId == null)
            {
                return View();
            }

            //eğer ders seçimi yapılmışsa o dersin notlarını (öğrenci ile birlikte) görüntüleyelim. 
            else
            {
                //şimdi notları(öğrenci bilgileri ile birlikte )view ekranına aktar.
                var notlar = db.TBLNOTLAR
                     .Where(k => k.DERSID == dersId && k.AKTİFLİK==true )
                    .Include(o => o.TBLOGRENCILER)               
                    .ToList();
                return View(notlar);
            }
        }

        
        public ActionResult NotKaydet(List<TBLNOTLAR> notlar)
        {
            foreach(var item in notlar)
            {
                var not = db.TBLNOTLAR.Find(item.NOTID);
                not.SINAV1 = item.SINAV1;
                not.SINAV2 = item.SINAV2;
                not.SINAV3 = item.SINAV3;
                not.PROJE = item.PROJE;

                not.ORTALAMA = (item.SINAV1 + item.SINAV2 + item.SINAV3 + item.PROJE) / 4;
                if(not.ORTALAMA>=50)
                {
                    not.DURUM = true;
                }
                else
                {
                    not.DURUM = false;
                }
            }
           
            db.SaveChanges();
            return RedirectToAction("Liste");
        }

        
    }
}