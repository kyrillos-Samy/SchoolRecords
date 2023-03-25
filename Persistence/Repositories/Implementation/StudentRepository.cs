using Common.Logger.Contract;
using Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Contract;
using System.Linq.Expressions;

namespace Persistence.Repositories.Implementation
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolRecordsContext context, ILoggerBase logger) : base(context, logger)
        {

        }
    }
}
