using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepElektroniczny1501.Interface
{
    public interface IMapper
    {
        Domen.Produkt ProduktMapper(produkt_kategoria produkt);
    }
}
