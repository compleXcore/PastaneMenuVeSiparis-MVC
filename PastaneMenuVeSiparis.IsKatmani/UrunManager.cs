using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaneMenuVeSiparis.IsKatmani
{
    public class UrunManager : IDisposable
    {
        private readonly UnitOfWork unitOfWork;
        public UrunManager()
        {
            unitOfWork = new UnitOfWork();
        }
        public List<Urun> Arama(string text)
        {
            return unitOfWork.UrunRepo.Search(text).ToList();
        }
        public List<Urun> ListeleKategori()
        {
            return unitOfWork.UrunRepo.GetAllWithKategori().ToList();
        }
        public Urun GetItem(int id)
        {
            return unitOfWork.UrunRepo.GetItem(id);
        }
        public Urun Ekle(Urun item)
        {
            var urun = unitOfWork.UrunRepo.Add(item);
            unitOfWork.Save();
            return urun;
        }
        public Urun Guncelle(Urun item)
        {
            var urun = unitOfWork.UrunRepo.Update(item);
            unitOfWork.Save();
            return urun;
        }
        public void Sil(int id)
        {
            unitOfWork.UrunRepo.Remove(id);
            unitOfWork.Save();
        }
        public void Dispose()
        {
            unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
