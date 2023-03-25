namespace DTO
{
    public class StudentClassDTO : BaseDTO
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public ClassDTO Class { get; set; }
    }
}
