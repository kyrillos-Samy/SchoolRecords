using Common.ServiceConnector.Contract;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolRecordsWeb.Pages.Student
{
    public class CoursesModel : PageModel
    {
        private readonly IServiceConnector _serviceConnector;
        private readonly ApplicationSettings _applicationSettings;
        public CoursesModel(IServiceConnector serviceConnector, ApplicationSettings applicationSettings)
        {
            _serviceConnector = serviceConnector;
            _applicationSettings = applicationSettings;
        }
        [BindProperty]
        public StudentDTO Student { get; set; } = new StudentDTO() { StudentData = new List<StudentDataDTO>(), StudentClasses = new List<StudentClassDTO>() };
        public SelectList CoursesSL { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return BadRequest();
            await _serviceConnector.TryGet(_applicationSettings.APIURL, $"Student/GetById/{id.Value}", out Response<StudentDTO> studentResponse, out string errorMessage);
            Student = studentResponse != null && studentResponse.Code == ResponseStatusEnum.Success ? studentResponse.Data : new StudentDTO() { StudentClasses = new List<StudentClassDTO>() { new StudentClassDTO() { Class  = new ClassDTO() { Course = new CourseDTO() } } } };

            Student.StudentClasses.Add(new StudentClassDTO() { Class = new ClassDTO { Course = new CourseDTO() { } } });
            await _serviceConnector.TryGet(_applicationSettings.APIURL, $"Course/GetALl", out Response<List<CourseDTO>> coursesResponse, out errorMessage);
            if (coursesResponse != null && coursesResponse.Code == ResponseStatusEnum.Success)
            {
                CoursesSL = new SelectList(coursesResponse.Data,
                nameof(CourseDTO.Id),
                nameof(CourseDTO.Name));
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _serviceConnector.TryPost(_applicationSettings.APIURL, $"Student/Enroll", Student, out string errorMessage);
            return RedirectToPage("/Students");
        }
    }
}
