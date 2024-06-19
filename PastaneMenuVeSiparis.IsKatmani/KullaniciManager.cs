using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaneMenuVeSiparis.IsKatmani
{
    public class KullaniciManager : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;
        public KullaniciManager()
        {
            _unitOfWork = new UnitOfWork();
        }
        public List<Kullanici> Listele()
        {
            return _unitOfWork.KullaniciRepo.GetAll().ToList();
        }
        public List<Kullanici> ListeleYetkililer()
        {
            return _unitOfWork.KullaniciRepo.GetAllYetkiliAndYonetici().OrderByDescending(x => x.Yetki).ToList();
        }
        public bool Giris(Kullanici kullanici)
        {
            return _unitOfWork.KullaniciRepo.Login(kullanici);
        }
        public bool KullaniciAdiKontrol(string kullaniciAdi)
        {
            return _unitOfWork.KullaniciRepo.GetKullaniciAdiControl(kullaniciAdi);
        }
        public Kullanici GetGirisKullanici(string kullaniciAdi, string kullaniciSifre)
        {
            return _unitOfWork.KullaniciRepo.GetLoginKullanici(kullaniciAdi, kullaniciSifre);
        }
        public Kullanici GetKullanici(int id)
        {
            return _unitOfWork.KullaniciRepo.GetItem(id);
        }
        public Kullanici Ekle(Kullanici item)
        {
            var kullanici = _unitOfWork.KullaniciRepo.Add(item);
            _unitOfWork.Save();
            return kullanici;
        }
        public Kullanici Guncelle(Kullanici item)
        {
            var kullanici = _unitOfWork.KullaniciRepo.Update(item);
            _unitOfWork.Save();
            return kullanici;
        }
        public int ToplamKullaniciSayisi()
        {
            return _unitOfWork.KullaniciRepo.GetAllKullaniciCount();
        }
        public void Sil(int id)
        {
            _unitOfWork.KullaniciRepo.Remove(id);
            _unitOfWork.Save();
        }
        public void Dispose()
        {
            _unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
