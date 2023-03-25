using AutoMapper;
using DTO;
using Entities;

namespace Business.Helper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Class, ClassDTO>().ReverseMap();
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<StudentClass, StudentClassDTO>().ReverseMap();
            CreateMap<StudentDataConfiguration, StudentDataConfigurationDTO>().ReverseMap();
            CreateMap<StudentData, StudentDataDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
        }
    }
}
