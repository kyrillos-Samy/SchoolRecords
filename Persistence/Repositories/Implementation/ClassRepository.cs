using Common.Logger.Contract;
using Entities;
using Persistence.Repositories.Contract;

namespace Persistence.Repositories.Implementation
{
    public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        public ClassRepository(SchoolRecordsContext context, ILoggerBase logger) : base(context, logger)
        {

        }
    }
}
