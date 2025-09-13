using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PojisteniWebApp.Data;
using PojisteniWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PojisteniWebApp.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>(); 
        }

        public async Task<TEntity?> FindById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<bool> ExistsWithId(int id)
        {
            return await FindById(id) is not null;
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            EntityEntry<TEntity> entry = dbSet.Add(entity);
            await dbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                EntityEntry<TEntity> entry = dbSet.Update(entity);
                await dbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (DbUpdateConcurrencyException)
            {               
                throw new InvalidOperationException("Došlo ke konfliktu při souběžné úpravě záznamu.");
            }
        }

        public async Task Delete(TEntity entity)
        {
            try
            {
                dbSet.Remove(entity);
                await dbContext.SaveChangesAsync();
            }
            catch
            {
                dbContext.Entry(entity).State = EntityState.Unchanged;
                throw;
            }
        }
    }
}