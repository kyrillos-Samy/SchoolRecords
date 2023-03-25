using Common.Logger.Contract;
using Entities;
using Persistence.Repositories.Contract;

namespace Persistence.Repositories.Implementation
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(SchoolRecordsContext context, ILoggerBase logger) : base(context, logger)
        {

        }
    }
}
