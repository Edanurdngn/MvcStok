using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcStok.Models
{
    public class LoginModel
    {
        public int KullaniciId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Tel { get; set; }
        public string TC { get; set; }
        [Required(ErrorMessage = "Lütfen mail adresinizi giriniz.")]
        [Display(Name = "E-Mail")]
        [EmailAddress(ErrorMessage ="Email format dışı")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        [MaxLength(16,ErrorMessage ="16 karakterden fazla giriş yapılamaz")]
        [MinLength(6, ErrorMessage = "6 karakterden az giriş yapılamaz")]
        public string Sifre { get; set; }
    }
}