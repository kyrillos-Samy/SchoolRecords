using Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SchoolRecordsContext : DbContext
    {
        public SchoolRecordsContext(DbContextOptions<SchoolRecordsContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Seed(modelBuilder);
        }

        #region Entities
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentClass> StudentClass { get; set; }
        public virtual DbSet<StudentData> StudentData { get; set; }
        public virtual DbSet<StudentDataConfiguration> StudentDataConfiguration { get; set; }
        #endregion Entites
        #region Seed
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentDataConfiguration>().HasData(
             new StudentDataConfiguration { Id = 1, FieldName = "First Name", IsVisable = true },
             new StudentDataConfiguration { Id = 2, FieldName = "Last Name", IsVisable = true },
             new StudentDataConfiguration { Id = 3, FieldName = "Email", IsVisable = true }
             );

            modelBuilder.Entity<Student>().HasData(
             new Student { Id = 1 }
             );

            modelBuilder.Entity<StudentData>().HasData(
             new StudentData { Id = 1, StudentId = 1, StudentDataConfigurationId = 1, Value = "john" },
             new StudentData { Id = 2, StudentId = 1, StudentDataConfigurationId = 2, Value = "Doe" },
             new StudentData { Id = 3, StudentId = 1, StudentDataConfigurationId = 3, Value = "John.Doe@gmail.com" }
             );

            modelBuilder.Entity<Course>().HasData(
             new Course { Id = 1, Name = "Math 1" }
             );

            modelBuilder.Entity<Class>().HasData(
             new Class { Id = 1, CourseId = 1, Name = "Class A" }
             );

            modelBuilder.Entity<StudentClass>().HasData(
             new StudentClass { Id = 1, StudentId = 1, ClassId = 1 }
             );
        }
        #endregion Seed
    }
}
