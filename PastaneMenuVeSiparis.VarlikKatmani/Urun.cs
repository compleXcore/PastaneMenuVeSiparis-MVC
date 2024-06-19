using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PastaneMenuVeSiparis.VarlikKatmani
{
    [Table("tblUrun")]
    public class Urun
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Urun Adi Bos Gecilemez"), MinLength(3, ErrorMessage = "Urun adi en az 3 karakterli olmalidir")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "Urun fiyati bos gecilmez")]
        public decimal Fiyat { get; set; }
        public int KategoriId { get; set; }
        [ForeignKey("KategoriId")]
        public Kategori Kategori { get; set; }
        [Required(ErrorMessage = "Açıklama bos gecilmez")]
        public string Aciklama { get; set; }

        public string image { get; set; } = "~/image/Tuzlu/lokmalik_tuzlu.png";
    }
}