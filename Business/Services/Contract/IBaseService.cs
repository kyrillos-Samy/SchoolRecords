using DTO;

namespace Business.Services.Contract
{
    public interface IBaseService<TDTO> where TDTO : BaseDTO
    {
        Task<Response<TDTO>> Add(TDTO dto);
        Task<Response<TDTO>> Update(TDTO dto);
        Task<Response<TDTO>> Delete(int id);
        Task<Response<TDTO>> Get(int id);
        Task<Response<IEnumerable<TDTO>>> GetAll();
    }
}
