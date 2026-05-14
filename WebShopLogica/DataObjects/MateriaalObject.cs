using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopLogica.DataObjects
{
    public class MateriaalObject
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int TypeMateriaalId { get; set; }
        public int MerkId { get; set; }
        public string Foto { get; set; }

    }
}
