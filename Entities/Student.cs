namespace Entities
{
    public class Student : BaseEntity
    {
        public virtual ICollection<StudentData> StudentData { get; set; }
    }
}
