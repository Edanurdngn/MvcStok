using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcStok.Models
{
    public class UrunDto
    {
        public int UrunId { get; set; }
        public string UrunAd { get; set; }
        public string Marka { get; set; }
        public string KategoriAd { get; set; }
        public decimal? Fiyat { get; set; }
        public int? Stok { get; set; }
        public int? MinStok { get; set; }
        public string Aciklama1 { get; set; }

        public bool  Kaldirildimi { get; set; }

    }
}