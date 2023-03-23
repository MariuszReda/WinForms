namespace SklepElektroniczny1501
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class zamowienie_produkt
    {
        public Guid id { get; set; }

        public Guid? id_produkt { get; set; }

        public Guid? id_zamowienie { get; set; }

        public int? ilosc { get; set; }

        public decimal? cena { get; set; }

        public virtual produkt produkt { get; set; }

        public virtual zamowienie zamowienie { get; set; }
    }
}
