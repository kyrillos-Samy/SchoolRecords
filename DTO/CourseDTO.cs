namespace DTO
{
    public class CourseDTO : BaseDTO
    {
        public string Name { get; set; }
        public List<ClassDTO> Classes { get; set; }
    }
}
