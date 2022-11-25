using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackster.Repository
{
    public interface IRepository<TEntity, TKey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        TEntity Get(TKey key);
        IEnumerable<TEntity> GetAll();
        void Delete(TEntity entity);

    }

    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly TracksterContext context;
        private readonly DbSet<TEntity> dbSet;

        public Repository(TracksterContext context)
        {
            //Ove dvije linije
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }
        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public TEntity Get(TKey key)
        {
            return dbSet.Find(key);
        }


        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            dbSet.Update(entity);
            context.SaveChanges();
        }
    }
}
