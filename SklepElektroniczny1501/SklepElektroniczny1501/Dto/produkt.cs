namespace SklepElektroniczny1501
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("produkt")]
    public partial class produkt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public produkt()
        {
            produkt_kategoria = new HashSet<produkt_kategoria>();
            zamowienie_produkt = new HashSet<zamowienie_produkt>();
        }

        public Guid id { get; set; }

        [StringLength(50)]
        public string nazwa { get; set; }

        [StringLength(50)]
        public string model { get; set; }

        [StringLength(255)]
        public string opis { get; set; }

        public int? ilosc_dostepna { get; set; }

        public decimal? cena { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<produkt_kategoria> produkt_kategoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<zamowienie_produkt> zamowienie_produkt { get; set; }
    }
}
