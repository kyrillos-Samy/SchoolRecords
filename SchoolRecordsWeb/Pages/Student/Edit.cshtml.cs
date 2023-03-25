using Common.ServiceConnector.Contract;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SchoolRecordsWeb.Pages.Student
{
    public class EditModel : PageModel
    {
        private readonly IServiceConnector _serviceConnector;
        private readonly ApplicationSettings _applicationSettings;
        public EditModel(IServiceConnector serviceConnector, ApplicationSettings applicationSettings)
        {
            _serviceConnector = serviceConnector;
            _applicationSettings = applicationSettings;
        }
        [BindProperty]
        public StudentDTO Student { get; set; } = new StudentDTO() { StudentData = new List<StudentDataDTO>() };
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return BadRequest();
            await _serviceConnector.TryGet(_applicationSettings.APIURL, $"Student/GetById/{id.Value}", out Response<StudentDTO> studentResponse, out string errorMessage);
            Student = studentResponse != null && studentResponse.Code == ResponseStatusEnum.Success ? studentResponse.Data : new StudentDTO() { StudentData = new List<StudentDataDTO>(), StudentDataConfigurations = new List<StudentDataConfigurationDTO>()};
            Student.StudentData.ForEach(data => { data.StudentDataConfiguration = Student.StudentDataConfigurations.FirstOrDefault(x => x.Id == data.StudentDataConfigurationId); });
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _serviceConnector.TryPost(_applicationSettings.APIURL, $"Student/Update", Student, out string errorMessage);
            return RedirectToPage("/Students");
        }
    }
}
