using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories.Base
{
    public interface IRepository<T> : IDisposable
    {
        ICollection<T> GetAll();
        T GetItem(int id);
        T Add(T item);
        T Update(T item);
        void Remove(int id);
    }
}
