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
    [Table("tblSiparisDetaylar")]
    public class SiparisDetay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UrunId { get; set; }
        [ForeignKey("UrunId")]
        public Urun Urun { get; set; }
        public int SiparisId { get; set; }
        [ForeignKey("SiparisId")]
        public Siparis Siparis { get; set; }
        public int Adet { get; set; } = 1;
        public decimal Tutar { get; set; }
    }
}
