using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcStok.Models
{
    public class KaraListeDto
    {
        public int KARALISTEID { get; set; }
        [Required]
        public int URUNID { get; set; }
        public DateTime GIRISTARIHI { get; set; }
        public DateTime CIKISTARIHI { get; set; }
        [Required]
        public string ACIKLAMA1 { get; set; }
        public string ACIKLAMA2 { get; set; }

        public string URUNADİ { get; set; }
        public string KALDIRILDIMI { get; set; }
        public KaraListeDto()
        {
            CIKISTARIHI = DateTime.Now;
            GIRISTARIHI = DateTime.Now;
            ACIKLAMA2 = "Açıklama Tanımlanmammış";
            

        }
    }
}