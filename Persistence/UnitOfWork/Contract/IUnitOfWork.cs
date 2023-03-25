using Entities;
using Persistence.Repositories.Contract;

namespace Persistence.UnitOfWork.Contract
{
    public interface IUnitOfWork<TEntity> where TEntity : BaseEntity
    {
        Task<bool> SaveAsync();
        #region Repositories
        IBaseRepository<TEntity> BaseRepository { get; }
        IClassRepository ClassRepository { get; }
        ICourseRepository CourseRepository { get; }
        IStudentClassRepository StudentClassRepository { get; }
        IStudentDataConfigurationRepository StudentDataConfigurationRepository { get; }
        IStudentDataRepository StudentDataRepository { get; }
        IStudentRepository StudentRepository { get; }
        #endregion Repositories
    }
}
