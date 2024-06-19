using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.DataBaseContext;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories
{
    public class SiparisRepository : Repository<Siparis>, ISiparisRepository
    {
        public SiparisRepository(AppDbContext context) : base(context)
        {
        }
        public ICollection<Siparis> GetAllWithKullanici()
        {
            return context.Siparisler.Include(x => x.Kullanici).Where(x => x.Durum == Durumlar.TESLIMEDILDI).ToList();
        }
        public ICollection<Siparis> GetAllWithKullanici(int id, Durumlar durum)
        {
            return context.Siparisler.Include(x => x.Kullanici).Where(x => x.KullaniciId == id && x.Durum == durum).ToList();
        }
        public Siparis GetSiparisInSepette(int id)
        {
            return context.Siparisler.Include(x => x.Kullanici).FirstOrDefault(x => x.KullaniciId == id && x.Durum == Durumlar.SEPETTE);
        }
        public int GetAllSiparisCount()
        {
            return context.Siparisler.Include(x => x.Kullanici).Where(x => x.Durum != Durumlar.SEPETTE).Count();
        }

        public Siparis GetSiparisWithKullanici(int id)
        {
            return context.Siparisler.Include(x => x.Kullanici).FirstOrDefault(x => x.Id == id);
        }
    }
}