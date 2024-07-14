﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructures.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ClassAdmin> ClassAdmins { get; set; }
        public DbSet<OutputStandard> OutputStandards { get; set; }
        public DbSet<ProgramSyllabus> ProgramSyllabuses { get; set; }
        public DbSet<Syllabus> Syllabuses { get; set; }
        public DbSet<TestAssessment> TestAssessments { get; set; }
        public DbSet<TrainingClass> TrainingClasses { get; set; }
        public DbSet<ClassStudent> ClassStudents { get; set; }
        public DbSet<TrainingMaterial> TrainingMaterials { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Lesson> UnitLessons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ApproveRequest> ApproveRequests { get; set; }

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
