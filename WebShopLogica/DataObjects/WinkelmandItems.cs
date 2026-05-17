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
        public int Aantal { get; set; }
        public string Beginperiode { get; set; }
        public string Eindperiode { get; set; }
    }
}
