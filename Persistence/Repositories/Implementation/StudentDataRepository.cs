using Common.Logger.Contract;
using Entities;
using Persistence.Repositories.Contract;

namespace Persistence.Repositories.Implementation
{
    public class StudentDataRepository : BaseRepository<StudentData>, IStudentDataRepository
    {
        public StudentDataRepository(SchoolRecordsContext context, ILoggerBase logger) : base(context, logger)
        {

        }
    }
}
