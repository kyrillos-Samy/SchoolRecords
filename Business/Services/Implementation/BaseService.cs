using AutoMapper;
using Business.Helper;
using Business.Helper.Extensions;
using Business.Services.Contract;
using Common.Logger.Contract;
using DTO;
using Entities;
using Persistence.UnitOfWork.Contract;

namespace Business.Services.Implementation
{
    public class BaseService<TEntity, TDTO> : IBaseService<TDTO> where TEntity : BaseEntity where TDTO : BaseDTO
    {
        protected IUnitOfWork<TEntity> _unitOfWork;
        protected ILoggerBase _loggerBase;

        public BaseService(IUnitOfWork<TEntity> unitOfWork, IMapper mapper, ILoggerBase loggerBase)
        {
            AutoMapperConfiguration.Mapper = mapper;
            _unitOfWork = unitOfWork;
            _loggerBase = loggerBase;
        }
        public virtual async Task<Response<TDTO>> Add(TDTO dto)
        {
            var response = new Response<TDTO>();
            var entity = dto.ToEntity<TEntity>();
            var addedEnity = await _unitOfWork.BaseRepository.Add(entity);
            if (await _unitOfWork.SaveAsync())
            {
                response.Code = ResponseStatusEnum.Success;
                response.Message = "Saved Successfully!";
                response.Data = addedEnity.ToDTO<TDTO>();
                return response;
            }
            response.Code = ResponseStatusEnum.Error;
            response.Message = "Not Saved!";
            response.Data = entity.ToDTO<TDTO>();
            return response;
        }

        public virtual async Task<Response<TDTO>> Update(TDTO dto)
        {
            var response = new Response<TDTO>();
            var entity = dto.ToEntity<TEntity>();
            var updatedEnity = _unitOfWork.BaseRepository.Update(entity);
            if (await _unitOfWork.SaveAsync())
            {
                response.Code = ResponseStatusEnum.Success;
                response.Message = "Updated Successfully!";
                response.Data = updatedEnity.ToDTO<TDTO>();
                return response;
            }
            response.Code = ResponseStatusEnum.Error;
            response.Message = "Not Updated!";
            response.Data = entity.ToDTO<TDTO>();
            return response;
        }


        public virtual async Task<Response<TDTO>> Delete(int id)
        {
            var response = new Response<TDTO>();
            _unitOfWork.BaseRepository.Delete(id);
            if (await _unitOfWork.SaveAsync())
            {
                response.Code = ResponseStatusEnum.Success;
                response.Message = "Deleted Successfully!";
                return response;
            }
            response.Code = ResponseStatusEnum.Error;
            response.Message = "Not Deleted!";
            return response;
        }

        public virtual async Task<Response<TDTO>> Get(int id)
        {
            var response = new Response<TDTO>();
            var entity = await _unitOfWork.BaseRepository.GetById(id);
            if (entity != null)
            {
                response.Code = ResponseStatusEnum.Success;
                response.Message = "Loaded Successfully!";
                response.Data = entity.ToDTO<TDTO>();
                return response;
            }
            response.Code = ResponseStatusEnum.Success;
            response.Message = "Not Data Found!";
            return response;
        }
        public virtual async Task<Response<IEnumerable<TDTO>>> GetAll()
        {
            var response = new Response<IEnumerable<TDTO>>();
            var entities = await _unitOfWork.BaseRepository.GetAll();
            if (entities.Any())
            {
                response.Code = ResponseStatusEnum.Success;
                response.Message = "Loaded Successfully!";
                response.Data = entities.ToDTOList<TDTO>();
                return response;
            }
            response.Code = ResponseStatusEnum.Success;
            response.Message = "Not Data Found!";
            return response;
        }

    }
}
