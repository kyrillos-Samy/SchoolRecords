using Business.Services.Contract;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace SchoolRecordsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : BaseController<ClassDTO>
    {
        protected new readonly IClassService _service;
        public ClassController(IClassService service) : base(service)
        {
            _service = service;
        }
    }
}
