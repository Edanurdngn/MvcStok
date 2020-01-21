using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcStok.Models
{
    public class UrunlerDto
    {
        public int URUNID { get; set; }
        
        public string URUNAD { get; set; }
  
        public string MARKA { get; set; }
        
        public string KATEGORİADİ { get; set; }
        public Nullable<decimal> FIYAT { get; set; }
        public Nullable<byte> STOK { get; set; }
    }
}