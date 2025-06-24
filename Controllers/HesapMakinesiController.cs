using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciNotMvc.Controllers
{
    public class HesapMakinesiController : Controller
    {
        // GET: HesapMakinesi

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(double sayi1=0,double sayi2=0)
        {
            double toplam = sayi1 + sayi2;
            double fark = sayi1 - sayi2;
            double carpim = sayi1 * sayi2;
            double bolum;
            if (sayi2 != 0)
            {
                 bolum = sayi1 / sayi2;
            }
            else
            {
                bolum = 0;
            }
            
            ViewBag.toplam = toplam; 
            ViewBag.fark = fark; 
            ViewBag.carpim = carpim; 
            ViewBag.bolum = bolum; 
            return View();
        }
    }
}