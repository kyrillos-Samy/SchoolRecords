using AutoMapper;
using Business.Helper.Extensions;
using Business.Services.Contract;
using Common.Logger.Contract;
using DTO;
using Entities;
using Persistence.UnitOfWork.Contract;
using System.Linq.Expressions;

namespace Business.Services.Implementation
{
    public class CourseService : BaseService<Course, CourseDTO>, ICourseService
    {
        public CourseService(IUnitOfWork<Course> unitOfWork, IMapper mapper, ILoggerBase loggerBase) : base(unitOfWork, mapper, loggerBase)
        {
        }
        public async override Task<Response<IEnumerable<CourseDTO>>> GetAll()
        {
            var response = new Response<IEnumerable<CourseDTO>>();
            var includes = new List<Expression<Func<Course, object>>>() { x => x.Classes };
            var entities = await _unitOfWork.BaseRepository.GetAll(includes: includes);
            if (entities.Any())
            {
                response.Code = ResponseStatusEnum.Success;
                response.Message = "Loaded Successfully!";
                response.Data = entities.ToDTOList<CourseDTO>();
                return response;
            }
            response.Code = ResponseStatusEnum.Success;
            response.Message = "Not Data Found!";
            return response;
        }
    }
}
