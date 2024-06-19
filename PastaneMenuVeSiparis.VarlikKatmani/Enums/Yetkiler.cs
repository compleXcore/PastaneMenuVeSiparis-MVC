using System.ComponentModel.DataAnnotations;

namespace PastaneMenuVeSiparis.VarlikKatmani.Enums
{
    public enum Yetkiler
    {
        [Display(Name = "Kullanıcı")]
        KULLANICI,
        [Display(Name = "Yetkili")]
        YETKILI,
        [Display(Name = "Yönetici")]
        YONETICI
    }
}
