using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopLogica.DataObjects
{
    public class WinkelmandItems
    {
        public string Merk { get; set; }
        public string Materiaal { get; set; }
        public int Maat { get; set; }
        public int Aantal { get; set; }
        public DateTime Beginperiode { get; set; }
        public DateTime Eindperiode { get; set; }

        public int MateriaalId { get; set; }
        public int MaatId { get; set; }
        
    }
}
