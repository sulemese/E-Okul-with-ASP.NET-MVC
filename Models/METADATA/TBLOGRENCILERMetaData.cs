using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace OgrenciNotMvc.Models
{
    [MetadataType(typeof(TBLOGRENCILERMetaData))]
    public partial class TBLOGRENCILER
    {
        // Boş, sadece metadata ile eşleşmek için
    }
    public class TBLOGRENCILERMetaData
    {
        [Required(ErrorMessage = "Ad boş bırakılamaz")]
        [StringLength(30)]
        public string OGRAD { get; set; }

        [Required(ErrorMessage = "Soyad boş bırakılamaz")]
        [StringLength(30)]
        public string OGRSOYAD { get; set; }

        [Required(ErrorMessage = "Fotoğraf URL zorunlu")]
        public string OGRRESIM { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Kulüp seçimi zorunlu")]
        public int OGRKULUP { get; set; }
    }
}