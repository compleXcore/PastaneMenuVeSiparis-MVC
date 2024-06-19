using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PastaneMenuVeSiparis.VarlikKatmani
{
    [Table("tblKategori")]
    public class Kategori
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Kategori Adi Bos Gecilemez"), MinLength(3, ErrorMessage = "Kategori adi en az 3 karakterli olmalidir")]
        public string Ad { get; set; }
    }
}
