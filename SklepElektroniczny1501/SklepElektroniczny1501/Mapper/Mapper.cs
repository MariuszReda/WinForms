using SklepElektroniczny1501.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepElektroniczny1501.Mapper
{
    public class Mapper : IMapper
    {
        public Domen.Produkt ProduktMapper(produkt_kategoria produkt)
        {
            return new Domen.Produkt
            {
                Id = produkt.produkt.id,
                Nazwa = produkt.produkt.nazwa,
                Cena =(decimal)produkt.produkt.cena,
                Model = produkt.produkt.model,
                Kategoria = new Domen.Kategoria { 
                    Id = (Guid)produkt.id_kategoria,
                    KategoriaNazwa = produkt.kategoria.kategoria1},
                Ilosc = (int)produkt.produkt.ilosc_dostepna,
                Opis = produkt.produkt.opis
            };
                
        }
    }
}
