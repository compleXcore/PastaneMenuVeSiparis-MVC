using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.DataBaseContext;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories
{
    public class KullaniciRepository : Repository<Kullanici>, IKullaniciRepository
    {
        public KullaniciRepository(AppDbContext context) : base(context)
        {
        }

        public int GetAllKullaniciCount()
        {
            return context.Kullanicilar.Where(x => x.Yetki == Yetkiler.KULLANICI).Count();
        }

        public ICollection<Kullanici> GetAllYetkiliAndYonetici()
        {
            return context.Kullanicilar.Where(x => x.Yetki == Yetkiler.YETKILI || x.Yetki == Yetkiler.YONETICI).ToList();
        }

        public bool GetKullaniciAdiControl(string kullaniciAdi)
        {
            if (context.Kullanicilar.FirstOrDefault(x =>
            x.KullaniciAdi.ToLower().Equals(kullaniciAdi.ToLower())) != null)
                return false;
            else
                return true;
        }

        public Kullanici GetLoginKullanici(string kullaniciAdi, string parola)
        {
            return context.Kullanicilar.FirstOrDefault(x =>
            x.KullaniciAdi.ToLower().Equals(kullaniciAdi.ToLower()) &&
            x.KullaniciSifre.Equals(parola));
        }
        public bool Login(Kullanici kullanici)
        {
            if (context.Kullanicilar.FirstOrDefault(x =>
            x.KullaniciAdi.ToLower().Equals(kullanici.KullaniciAdi.ToLower()) &&
            x.KullaniciSifre.Equals(kullanici.KullaniciSifre)) != null)
                return true;
            else
                return false;
        }
    }
}
