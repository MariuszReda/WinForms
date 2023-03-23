using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepElektroniczny1501.Domen
{
    public class Produkt
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }
        public string Model { get; set; }
        public Kategoria Kategoria { get; set; }
        public string Opis { get; set; }
        public int Ilosc { get; set; }
        public decimal Cena { get; set; }
        public Akcja Akcja { get; set; }
         
    }
    public enum Akcja
    { 
        Nowy,
        Edycja
    }
}
