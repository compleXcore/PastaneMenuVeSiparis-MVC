using PastaneMenuVeSiparis.VarlikKatmani;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.DataBaseContext;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories
{
    public class SiparisDetayRepository : Repository<SiparisDetay>, ISiparisDetayRepository
    {
        public SiparisDetayRepository(AppDbContext context) : base(context)
        {
        }

        public ICollection<SiparisDetay> GetAllUrunWithSiparisId(int siparisId)
        {
            return context.SiparisDetaylar.Include(x => x.Urun).Include(x => x.Urun.Kategori).Where(x => x.SiparisId == siparisId).ToList();
        }
    }
}
