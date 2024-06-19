using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PastaneMenuVeSiparis.VarlikKatmani.Enums
{
    public enum Durumlar
    {
        [Display(Name = "Sepette")]
        SEPETTE,
        [Display(Name = "Teslim Edildi")]
        TESLIMEDILDI
    }
}