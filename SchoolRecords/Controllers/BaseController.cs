using Business.Services.Contract;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace SchoolRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TDTO> : ControllerBase where TDTO : BaseDTO
    {
        protected readonly IBaseService<TDTO> _service;

        public BaseController(IBaseService<TDTO> service)
        {
            _service = service;
        }

        #region Get
        [HttpGet("GetAll")]
        public virtual async Task<Response<IEnumerable<TDTO>>> GetAll()
        {
            try
            {
                return await _service.GetAll();
            }
            catch (Exception ex)
            {

                return new Response<IEnumerable<TDTO>>()
                {
                    Code = ResponseStatusEnum.Exception,
                    Message = ex.Message
                };
            }
        }

        [HttpGet("GetById/{id}")]
        public virtual async Task<Response<TDTO>> GetById(int id)
        {
            try
            {
                return await _service.Get(id);
            }
            catch (Exception ex)
            {

                return new Response<TDTO>()
                {
                    Code = ResponseStatusEnum.Exception,
                    Message = ex.Message
                };
            }
        }

        #endregion

        #region Create

        [HttpPost("Add")]
        public virtual async Task<Response<TDTO>> Add(TDTO dto)
        {
            try
            {
                return await _service.Add(dto);
            }
            catch (Exception ex)
            {

                return new Response<TDTO>()
                {
                    Code = ResponseStatusEnum.Exception,
                    Message = ex.Message
                };
            }
        }
        #endregion

        #region Edit

        [HttpPost("Update")]
        public virtual async Task<Response<TDTO>> Update(TDTO dto)
        {
            try
            {
                return await _service.Update(dto);
            }
            catch (Exception ex)
            {

                return new Response<TDTO>()
                {
                    Code = ResponseStatusEnum.Exception,
                    Message = ex.Message
                };
            }
        }
        #endregion

        #region Delete

        [HttpPost("Delete")]
        public virtual async Task<Response<TDTO>> Delete(int id)
        {
            try
            {
                return await _service.Delete(id);
            }
            catch (Exception ex)
            {

                return new Response<TDTO>()
                {
                    Code = ResponseStatusEnum.Exception,
                    Message = ex.Message
                };
            }
        }
        #endregion
    }
}
