using Domain.Entities;
using Domain.Entities.Syllabuses;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructures.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassUsers> ClassUsers { get; set; }
        public DbSet<FeedbackForm> FeedbackForms { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<OutputStandard> OutputStandards { get; set; }
        public DbSet<TestAssessment> TestAssessments { get; set; }
        public DbSet<TMS> TimeMngSystems { get; set; }
        public DbSet<TrainingMaterial> TrainingMaterials { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Syllabus> Syllabus { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ClassUnitDetail> ClassUnitDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
