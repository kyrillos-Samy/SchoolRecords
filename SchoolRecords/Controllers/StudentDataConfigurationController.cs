using Business.Services.Contract;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace SchoolRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDataConfigurationController : BaseController<StudentDataConfigurationDTO>
    {
        protected new readonly IBaseService<StudentDataConfigurationDTO> _service;
        public StudentDataConfigurationController(IBaseService<StudentDataConfigurationDTO> service) : base(service)
        {
            _service = service;
        }
    }
}
