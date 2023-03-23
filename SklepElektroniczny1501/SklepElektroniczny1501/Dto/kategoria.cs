namespace SklepElektroniczny1501
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("kategoria")]
    public partial class kategoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public kategoria()
        {
            produkt_kategoria = new HashSet<produkt_kategoria>();
        }

        public Guid id { get; set; }

        [Column("kategoria")]

         public string kategoria1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<produkt_kategoria> produkt_kategoria { get; set; }
    }
}
