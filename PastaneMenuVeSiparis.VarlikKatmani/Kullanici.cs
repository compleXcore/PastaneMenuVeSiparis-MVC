using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaneMenuVeSiparis.VarlikKatmani
{
    [Table("tblKullanici")]
    public class Kullanici
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Kullanici adi bos gecilemez"), MinLength(5, ErrorMessage = "Kullanici adi en az 5 karakterli olmalidir"), Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }
        [Required(ErrorMessage = "Kullanici sifre bos gecilemez"), MinLength(5, ErrorMessage = "Kullanici sifre en az 5 karakterli olmalidir"), Display(Name = "Kullanıcı Şifre")]
        public string KullaniciSifre { get; set; }
        [Required(ErrorMessage = "Adres bos gecilemez")]
        public string Adres { get; set; }
        [Required(ErrorMessage = "Telno Bos Gecilemez"), Display(Name = "Telefon Numarası")]
        public string Telno { get; set; }
        public Yetkiler Yetki { get; set; } = Yetkiler.KULLANICI;
    }
}
