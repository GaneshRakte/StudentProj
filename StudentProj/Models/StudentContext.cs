using Microsoft.EntityFrameworkCore;

namespace StudentProj.Models
{
    public class StudentContext:DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> context) : base(context) 
        { 

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subject { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>()
            .HasOne(_ => _.Student)
            .WithMany(_ => _.Subject)
            .HasForeignKey(_ => _.StudentId);
        }



    }
}
