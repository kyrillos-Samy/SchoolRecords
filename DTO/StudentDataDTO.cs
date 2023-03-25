namespace DTO
{
    public class StudentDataDTO : BaseDTO
    {
        public int StudentId { get; set; }
        public int StudentDataConfigurationId { get; set; }
        public StudentDataConfigurationDTO StudentDataConfiguration { get; set; }
        public string Value { get; set; }
    }
}
