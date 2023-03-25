using Business.Services.Contract;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace SchoolRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController<StudentDTO>
    {
        protected new readonly IStudentService _service;
        public StudentController(IStudentService service) : base(service)
        {
            _service = service;
        }

        [HttpPost("Enroll")]
        public virtual async Task<Response<StudentDTO>> Enroll(StudentDTO student)
        {
            try
            {
                return await _service.Enroll(student);
            }
            catch (Exception ex)
            {

                return new Response<StudentDTO>()
                {
                    Code = ResponseStatusEnum.Exception,
                    Message = ex.Message
                };
            }
        }
    }
}
