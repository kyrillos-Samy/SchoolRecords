using Common.Logger.Contract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Contract;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.Repositories.Implementation
{
    public class StudentClassRepository : BaseRepository<StudentClass>, IStudentClassRepository
    {
        public StudentClassRepository(SchoolRecordsContext context, ILoggerBase logger) : base(context, logger)
        {

        }
        public override async Task<List<StudentClass>> GetWhere(Expression<Func<StudentClass, bool>> filter = null,
           Func<IQueryable<StudentClass>, IOrderedQueryable<StudentClass>> orderBy = null,
           List<Expression<Func<StudentClass, object>>> includes = null)
        {
            IQueryable<StudentClass> query = dbSet;
            if (includes != null)
            {
                foreach (Expression<Func<StudentClass, object>> include in includes)
                    query = query.Include(include);
            }
            query = query.Include(x => x.Class).ThenInclude(x => x.Course);
            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }
    }
}
