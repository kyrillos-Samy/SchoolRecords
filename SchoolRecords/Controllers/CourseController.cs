using Business.Services.Contract;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace SchoolRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController<CourseDTO>
    {
        protected new readonly ICourseService _service;
        public CourseController(ICourseService service) : base(service)
        {
            _service = service;
        }
    }
}
