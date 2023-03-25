namespace Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}
