namespace Entities
{
    public class BaseEntity : IDisposable
    {
        public int Id { get; set; }

        public void Dispose()
        {
    
        }
    }
}
