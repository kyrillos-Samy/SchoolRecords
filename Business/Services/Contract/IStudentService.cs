using DTO;

namespace Business.Services.Contract
{
    public interface IStudentService : IBaseService<StudentDTO>
    {
        Task<Response<StudentDTO>> Enroll(StudentDTO student);
    }
}
