namespace DTO
{
    public class ClassDTO : BaseDTO
    {
        public int CourseId { get; set; }
        public CourseDTO Course { get; set; }
        public string Name { get; set; }
        public List<StudentDTO> Students { get; set; }
    }
}
