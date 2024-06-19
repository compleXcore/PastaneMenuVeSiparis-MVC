using PastaneMenuVeSiparis.VarlikKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories.Base
{
    public interface IUrunRepository : IRepository<Urun>
    {
        ICollection<Urun> Search(string text);

        ICollection<Urun> GetAllWithKategori();

        Urun GetItemWithKategori(int id);
    }
}
