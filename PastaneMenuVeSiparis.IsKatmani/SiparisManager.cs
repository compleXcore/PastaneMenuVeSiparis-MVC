using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PastaneMenuVeSiparis.IsKatmani
{
    public class SiparisManager : IDisposable
    {
        private readonly UnitOfWork unitOfWork;
        public SiparisManager()
        {
            unitOfWork = new UnitOfWork();
        }

        public List<Siparis> SiparislerWithKullanici()
        {
            return unitOfWork.SiparisRepo.GetAllWithKullanici().OrderBy(x => x.Tarih).ToList();
        }
        public List<Siparis> SiparislerKullaniciVeDurum(int id, Durumlar durum)
        {
            return unitOfWork.SiparisRepo.GetAllWithKullanici(id, durum).OrderBy(x => x.Tarih).ToList();
        }
        public Siparis GetSepetteSiparis(int id)
        {
            return unitOfWork.SiparisRepo.GetSiparisInSepette(id);
        }
        public Siparis GetSiparisWithKullanici(int id)
        {
            return unitOfWork.SiparisRepo.GetSiparisWithKullanici(id);
        }
        public Siparis Ekle(Siparis item)
        {
            var siparis = unitOfWork.SiparisRepo.Add(item);
            unitOfWork.Save();
            return siparis;
        }
        public Siparis Guncelle(Siparis item)
        {
            var siparis = unitOfWork.SiparisRepo.Update(item);
            unitOfWork.Save();
            return siparis;
        }
        public void Sil(int id)
        {
            unitOfWork.SiparisRepo.Remove(id);
            unitOfWork.Save();
        }
        public int TeslimEdilenSiparisSayisi()
        {
            return unitOfWork.SiparisRepo.GetAllSiparisCount();
        }
        public void GuncelleSepet(int id)
        {
            var sepet = SiparislerKullaniciVeDurum(id, Durumlar.SEPETTE);
            if(sepet != null)
            {
                foreach(var item in sepet)
                {
                    item.Durum = Durumlar.TESLIMEDILDI;
                    item.Tarih = DateTime.Now;
                    var siparis = Guncelle(item);
                }
            }
        }
        public void Dispose()
        {
            unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}