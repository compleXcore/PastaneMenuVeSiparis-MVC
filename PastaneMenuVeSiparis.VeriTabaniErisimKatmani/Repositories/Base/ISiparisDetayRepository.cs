using PastaneMenuVeSiparis.VarlikKatmani;
using System.Collections.Generic;

namespace PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories.Base
{
    public interface ISiparisDetayRepository : IRepository<SiparisDetay>
    {
        ICollection<SiparisDetay> GetAllUrunWithSiparisId(int siparisId);
    }
}
