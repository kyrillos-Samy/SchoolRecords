using Common.ServiceConnector.Contract;
using DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SchoolRecordsWeb.Pages
{
    public class StudentsModel : PageModel
    {
        private readonly IServiceConnector _serviceConnector;
        private readonly ApplicationSettings _applicationSettings;
        public IList<StudentDTO> Students { get; set; } = new List<StudentDTO>();
        public StudentsModel(IServiceConnector serviceConnector, ApplicationSettings applicationSettings)
        {
            _serviceConnector = serviceConnector;
            _applicationSettings = applicationSettings;
        }
        public async Task OnGetAsync()
        {
            await _serviceConnector.TryGet(_applicationSettings.APIURL, $"Student/GetAll", out Response<IList<StudentDTO>> studentsResponse, out string errorMessage);
            Students = studentsResponse != null && studentsResponse.Code == ResponseStatusEnum.Success && studentsResponse.Data.Any() ? 
                studentsResponse.Data : new List<StudentDTO>() { new StudentDTO() { } };
        }
    }
}
