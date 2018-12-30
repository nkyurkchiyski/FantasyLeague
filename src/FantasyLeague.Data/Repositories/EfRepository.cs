using FantasyLeague.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyLeague.Data.Repositories
{
    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet;
        private readonly FantasyLeagueDbContext context;

        public EfRepository(FantasyLeagueDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dbSet = this.context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> All() => this.dbSet;

        public virtual void Add(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        public virtual void AddRange(params TEntity[] entities)
        {
            this.dbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public virtual void DeleteRange(params TEntity[] entities)
        {
            this.dbSet.RemoveRange(entities);
        }

        public Task<int> SaveChangesAsync() => this.context.SaveChangesAsync();

        public void Dispose() => this.context.Dispose();

        public Task<TEntity> GetByIdAsync(params object[] id) => this.dbSet.FindAsync(id);
    }

}
