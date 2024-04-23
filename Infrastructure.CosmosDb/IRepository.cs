namespace Infrastructure.CosmosDb
{
    using Domain.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        IEnumerable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(string id);

        Task<TEntity> AddOrUpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(string id);
    }
}
