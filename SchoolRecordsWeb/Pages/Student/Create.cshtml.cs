using Common.ServiceConnector.Contract;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolRecordsWeb.Pages.Student
{
    public class CreateModel : PageModel
    {
        private readonly IServiceConnector _serviceConnector;
        private readonly ApplicationSettings _applicationSettings;
        public CreateModel(IServiceConnector serviceConnector, ApplicationSettings applicationSettings)
        {
            _serviceConnector = serviceConnector;
            _applicationSettings = applicationSettings;
        }
        public SelectList CoursesSL { get; set; }
        public async Task OnGetAsync()
        {
            await _serviceConnector.TryGet(_applicationSettings.APIURL, $"StudentDataConfiguration/GetAll", out Response<IList<StudentDataConfigurationDTO>> configurationResponse, out string errorMessage);
            if (configurationResponse != null && configurationResponse.Code == ResponseStatusEnum.Success && configurationResponse.Data.Any())
            {
                foreach (var item in configurationResponse.Data)
                {
                    Student.StudentData.Add(new StudentDataDTO() { StudentDataConfigurationId = item.Id, StudentDataConfiguration = item });
                }
            }
            await _serviceConnector.TryGet(_applicationSettings.APIURL, $"Course/GetALl", out Response<List<CourseDTO>> coursesResponse, out errorMessage);
            if (coursesResponse != null && coursesResponse.Code == ResponseStatusEnum.Success)
            {
                CoursesSL = new SelectList(coursesResponse.Data,
                nameof(CourseDTO.Id),
                nameof(CourseDTO.Name));
            }
        }
        [BindProperty]
        public StudentDTO Student { get; set; } = new StudentDTO() { StudentData = new List<StudentDataDTO>(), StudentClasses = new List<StudentClassDTO>() { new StudentClassDTO() {Class = new ClassDTO() { Course = new CourseDTO()} } } };

        public async Task<IActionResult> OnPostAsync()
        {
            var clases = Student.StudentClasses;
            Student.StudentClasses = new List<StudentClassDTO>();
            var newStudent = await _serviceConnector.TryPost(_applicationSettings.APIURL, $"Student/Add", Student, out string errorMessage);
            Student.Id = newStudent.Id;
            Student.StudentClasses = clases;
            await _serviceConnector.TryPost(_applicationSettings.APIURL, $"Student/Enroll", Student, out errorMessage);
            return RedirectToPage("/Students");
        }
    }
}
