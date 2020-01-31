using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MvcStok.Models
{

    public class SatisDto

    { 
        public int SATISID { get; set; }
        public string URUN { get; set; }
        public string MUSTERI { get; set; }
        public Nullable<byte> ADET { get; set; }
        public Nullable<decimal> FIYAT { get; set; }
        public Nullable<int> TUTAR { get; set; }
        public int TOPLAMTUTAR{ get; set; }
    }
}
