namespace SklepElektroniczny1501
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class produkt_kategoria
    {
        public Guid id { get; set; }

        public Guid? id_produkt { get; set; }

        public Guid? id_kategoria { get; set; }

        public virtual kategoria kategoria { get; set; }

        public virtual produkt produkt { get; set; }
    }
}
