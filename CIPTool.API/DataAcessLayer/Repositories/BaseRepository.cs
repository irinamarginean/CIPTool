using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAcessLayer.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly CIPToolContext dataContext;
        private readonly DbSet<T> dbSet;

        public BaseRepository(CIPToolContext dataContext)
        {
            this.dataContext = dataContext;
            dbSet = dataContext.Set<T>();
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await Task.FromResult(dbSet.ToList());
        }

        public DbSet<T> GetDbSet()
        {
            return dataContext.Set<T>();
        }

        public async Task Insert(T entity)
        {
            await dbSet.AddAsync(entity);

            await dataContext.SaveChangesAsync();
        }

        public virtual async Task Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            dataContext.Entry(entityToUpdate).State = EntityState.Modified;

            await dataContext.SaveChangesAsync();
        }

        public async Task Delete(T entityToDelete)
        {
            if (dataContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);

            await dataContext.SaveChangesAsync();
        }
    }
}
