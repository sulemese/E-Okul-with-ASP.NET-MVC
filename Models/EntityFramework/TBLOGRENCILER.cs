//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OgrenciNotMvc.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLOGRENCILER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBLOGRENCILER()
        {
            this.AKTİFLİK = true;
            this.TBLNOTLAR = new HashSet<TBLNOTLAR>();
        }
    
        public int OGRID { get; set; }
        public string OGRAD { get; set; }
        public string OGRSOYAD { get; set; }
        public string OGRRESIM { get; set; }
        public string OGRCINSIYET { get; set; }
        public Nullable<int> OGRKULUP { get; set; }
        public Nullable<bool> AKTİFLİK { get; set; }
    
        public virtual TBLKULUPLER TBLKULUPLER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBLNOTLAR> TBLNOTLAR { get; set; }
    }
}
