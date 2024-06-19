using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.DataBaseContext;
using PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PastaneMenuVeSiparis.VeriTabaniErisimKatmani.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        public T Add(T item)
        {
            return context.Set<T>().Add(item);
        }

        public ICollection<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetItem(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Remove(int id)
        {
            context.Set<T>().Remove(GetItem(id));
        }

        public T Update(T item)
        {
            if (context.Entry<T>(item).State == EntityState.Detached)
            {
                context.Set<T>().Attach(item);
                context.Entry<T>(item).State = EntityState.Modified;
            }
            return item;
        }

        public void Dispose()
        {
            context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
