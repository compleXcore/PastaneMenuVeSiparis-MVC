using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PastaneMenuVeSiparis.VarlikKatmani
{
    [Table("tblSiparis")]
    public class Siparis
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        [ForeignKey("KullaniciId")]
        public Kullanici Kullanici { get; set; }
        public Durumlar Durum { get; set; } = Durumlar.SEPETTE;
        public decimal ToplamFiyat { get; set; }
        public DateTime Tarih { get; set; }
    }
}
