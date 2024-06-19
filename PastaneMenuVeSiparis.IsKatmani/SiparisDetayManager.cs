using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaneMenuVeSiparis.IsKatmani
{
    public class SiparisDetayManager : IDisposable
    {
        private readonly UnitOfWork unitOfWork;
        public SiparisDetayManager()
        {
            unitOfWork = new UnitOfWork();
        }
        public List<SiparisDetay> SiparisDetaylar(int siparisId)
        {
            return unitOfWork.SiparisDetayRepo.GetAllUrunWithSiparisId(siparisId).ToList();
        }
        public SiparisDetay Ekle(SiparisDetay item)
        {
            var siparisDetay = unitOfWork.SiparisDetayRepo.Add(item);
            unitOfWork.Save();
            return siparisDetay;
        }
        public SiparisDetay Guncelle(SiparisDetay item)
        {
            var siparisDetay = unitOfWork.SiparisDetayRepo.Update(item);
            unitOfWork.Save();
            return siparisDetay;
        }
        public void Sil(int id)
        {
            unitOfWork.SiparisDetayRepo.Remove(id);
            unitOfWork.Save();
        }
        public void Dispose()
        {
            unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
