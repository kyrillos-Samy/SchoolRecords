namespace Entities
{
    public class StudentData : BaseEntity
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int StudentDataConfigurationId { get; set; }
        public virtual StudentDataConfiguration StudentDataConfiguration { get; set; }
        public string Value { get; set; }
    }
}
