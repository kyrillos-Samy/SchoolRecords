using Common.Logger.Contract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Contract;
using System.Linq.Expressions;

namespace Persistence.Repositories.Implementation
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected SchoolRecordsContext _context;
        protected readonly DbSet<TEntity> dbSet;
        public readonly ILoggerBase _logger;

        public BaseRepository(
            SchoolRecordsContext context,
            ILoggerBase logger)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.dbSet = _context.Set<TEntity>();
            _logger = logger;
        }


        public async virtual Task<List<TEntity>> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (includes != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);
            }
            if (orderBy != null)
                query = orderBy(query);
            return await query.ToListAsync();
        }

        public async virtual Task<List<TEntity>> GetWhere(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (includes != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);
            }
            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }


        public virtual async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
            List<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includes != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            return (await dbSet.AddAsync(entity)).Entity;
        }

        public virtual async Task AddRange(IEnumerable<TEntity> entity)
        {
            await dbSet.AddRangeAsync(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return dbSet.Update(entity).Entity;
        }

        public virtual void UpdateRange(IList<TEntity> entities)
        {
            dbSet.AttachRange(entities);
            //_context.Entry(entities).State = EntityState.Detached;
            //_context.Entry(entities).State = EntityState.Modified;
        }


        public virtual void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> filter = null,
            List<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includes != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);
            }

            TEntity entityToDelete = query.FirstOrDefault(filter);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void DeleteRange(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            IQueryable<TEntity> entitiesToDelete = query.Where(filter);
            foreach (var item in entitiesToDelete)
            {
                Delete(item);
            }
        }
    }
}
