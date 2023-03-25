namespace DTO
{
    public class StudentDTO : BaseDTO
    {
        public List<StudentDataDTO> StudentData { get; set; }
        public List<StudentClassDTO> StudentClasses { get; set; }
        public List<StudentDataConfigurationDTO> StudentDataConfigurations { get; set; }
    }
}
