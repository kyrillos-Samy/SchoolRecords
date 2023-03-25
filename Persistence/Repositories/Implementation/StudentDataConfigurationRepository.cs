using Common.Logger.Contract;
using Entities;
using Persistence.Repositories.Contract;

namespace Persistence.Repositories.Implementation
{
    public class StudentDataConfigurationRepository : BaseRepository<StudentDataConfiguration>, IStudentDataConfigurationRepository
    {
        public StudentDataConfigurationRepository(SchoolRecordsContext context, ILoggerBase logger) : base(context, logger)
        {

        }
    }
}
