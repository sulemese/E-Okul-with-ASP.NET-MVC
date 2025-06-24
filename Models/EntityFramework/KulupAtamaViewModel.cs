using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OgrenciNotMvc.Models
{
    public class KulupAtamaViewModel
    {
        public int? SecilenKulupId { get; set; } // Dropdown seçimi

        public List<int> SecilenOgrenciIdleri { get; set; } // Checkbox seçimi

        public List<OgrenciNotMvc.Models.EntityFramework.TBLOGRENCILER> Ogrenciler { get; set; } // Öğrencileri göstermek için

        public List<OgrenciNotMvc.Models.EntityFramework.TBLKULUPLER> Kulupler { get; set; } // Dropdown list için
    }
}