using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VarlikKatmani.Enums;
using System.Collections.Generic;

namespace PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories.Base
{
    public interface ISiparisRepository : IRepository<Siparis>
    {
        ICollection<Siparis> GetAllWithKullanici(int id, Durumlar durum);
        ICollection<Siparis> GetAllWithKullanici();
        int GetAllSiparisCount();
        Siparis GetSiparisInSepette(int id);
        Siparis GetSiparisWithKullanici(int id);
    }
}
