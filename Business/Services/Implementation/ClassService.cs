using AutoMapper;
using Business.Services.Contract;
using Common.Logger.Contract;
using DTO;
using Entities;
using Persistence.UnitOfWork.Contract;

namespace Business.Services.Implementation
{
    public class ClassService : BaseService<Class, ClassDTO>, IClassService
    {
        public ClassService(IUnitOfWork<Class> unitOfWork, IMapper mapper, ILoggerBase loggerBase) : base(unitOfWork, mapper, loggerBase)
        {
        }
    }
}
