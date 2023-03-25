using Common.Logger.Contract;
using Entities;
using Persistence.Repositories.Contract;
using Persistence.Repositories.Implementation;
using Persistence.UnitOfWork.Contract;

namespace Persistence.UnitOfWork.Implementation
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : BaseEntity, IDisposable
    {
        private readonly ILoggerBase _logger;
        private readonly SchoolRecordsContext context;

        public UnitOfWork(SchoolRecordsContext schoolRecordsContext, ILoggerBase logger)
        {
            context = schoolRecordsContext;
            _logger = logger;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                return (await context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                return false;
            }

        }

        #region Repositories
        public IBaseRepository<TEntity> BaseRepository
        {
            get
            {
                return new BaseRepository<TEntity>(context, _logger);
            }
            set { }
        }
        public IClassRepository ClassRepository
        {
            get
            {
                return new ClassRepository(context, _logger);
            }
            set { }
        }
        public ICourseRepository CourseRepository
        {
            get
            {
                return new CourseRepository(context, _logger);
            }
            set { }
        }
        public IStudentClassRepository StudentClassRepository
        {
            get
            {
                return new StudentClassRepository(context, _logger);
            }
            set { }
        }
        public IStudentDataConfigurationRepository StudentDataConfigurationRepository
        {
            get
            {
                return new StudentDataConfigurationRepository(context, _logger);
            }
            set { }
        }
        public IStudentDataRepository StudentDataRepository
        {
            get
            {
                return new StudentDataRepository(context, _logger);
            }
            set { }
        }
        public IStudentRepository StudentRepository
        {
            get
            {
                return new StudentRepository(context, _logger);
            }
            set { }
        }
        #endregion Repositories
    }
}
