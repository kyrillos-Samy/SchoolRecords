using Entities;
using System.Linq.Expressions;

namespace Persistence.Repositories.Contract
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           List<Expression<Func<TEntity, object>>> includes = null);

        Task<List<TEntity>> GetWhere(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null);

        Task<TEntity> GetById(object id);


        Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
            List<Expression<Func<TEntity, object>>> includes = null);


        Task<TEntity> Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entity);
        TEntity Update(TEntity entity);
        void UpdateRange(IList<TEntity> entities);
        void Delete(int id);
        public void Delete(Expression<Func<TEntity, bool>> filter = null,
           List<Expression<Func<TEntity, object>>> includes = null);
        void DeleteRange(Expression<Func<TEntity, bool>> filter = null);
    }
}
