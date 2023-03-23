namespace SklepElektroniczny1501
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("zamowienie")]
    public partial class zamowienie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public zamowienie()
        {
            zamowienie_produkt = new HashSet<zamowienie_produkt>();
        }

        public Guid id { get; set; }

        [StringLength(50)]
        public string numer_zamowienia { get; set; }

        [Column(TypeName = "date")]
        public DateTime? data_zamowienie { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<zamowienie_produkt> zamowienie_produkt { get; set; }
    }
}
