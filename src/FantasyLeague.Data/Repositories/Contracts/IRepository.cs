using System;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyLeague.Data.Repositories.Contracts
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> All();

        Task<TEntity> GetByIdAsync(params object[] id);

        void Add(TEntity entity);

        void AddRange(params TEntity[] entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteRange(params TEntity[] entities);

        Task<int> SaveChangesAsync();
    }
}
