using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcStok.Models
{
    public class KasaDto
    {
        public int KASAID { get; set; }
        public Nullable<byte> DEFO { get; set; }
        public Nullable<int> SATISID { get; set; }
        public Nullable<int> KASATOPLAM { get; set; }
        public Nullable<System.DateTime> ISLEMTARIHI { get; set; }

    }
}