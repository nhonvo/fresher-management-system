using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace Infrastructures.Persistence
{
    public class ApplicationDbContextInitializer
    {
        private readonly ApplicationDbContext _context;
        public ApplicationDbContextInitializer(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString(), "Migration error");
                Log.Error(ex, "Migration error");
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Migration error");
                System.Console.WriteLine(ex.ToString(), "Seeding error");
            }
        }

        public async Task TrySeedAsync()
        {
            // user or "||" operator for another table
            if (!_context.Users.Any())
            {
                string json = File.ReadAllText(@"../../Json/User.json");
                List<User> users = JsonSerializer.Deserialize<List<User>>(json)!;
                await _context.AddRangeAsync(users);
                await _context.SaveChangesAsync();
            };

            if (!_context.Syllabuses.Any())
            {
                string json = File.ReadAllText(@"../../Json/Syllabus.json");
                List<Syllabus> sylabuses = JsonSerializer.Deserialize<List<Syllabus>>(json)!;
                await _context.AddRangeAsync(sylabuses);
                await _context.SaveChangesAsync();

            };

            if (!_context.TrainingPrograms.Any())
            {
                string json = File.ReadAllText(@"../../Json/TrainingProgram.json");
                List<TrainingProgram> trainingProgram = JsonSerializer.Deserialize<List<TrainingProgram>>(json)!;
                await _context.AddRangeAsync(trainingProgram);
                await _context.SaveChangesAsync();

            };

            // if (!_context.TestAssessments.Any())
            // {
            //     string json = File.ReadAllText(@"../../Json/TestAssessment.json");
            //     List<TestAssessment> testAssessments = JsonSerializer.Deserialize<List<TestAssessment>>(json)!;
            //     await _context.AddRangeAsync(testAssessments);
            //     await _context.SaveChangesAsync();
            // };
            if (!_context.FSUs.Any())
            {
                string json = File.ReadAllText(@"../../Json/FSU.json");
                List<FSU> testAssessments = JsonSerializer.Deserialize<List<FSU>>(json)!;
                await _context.AddRangeAsync(testAssessments);
                await _context.SaveChangesAsync();
            };
            //if (!_context.TrainingClasses.Any())
            //{
            //    string json = File.ReadAllText(@"../../Json/TrainingClass.json");
            //    List<TrainingClass> testAssessments = JsonSerializer.Deserialize<List<TrainingClass>>(json)!;
            //    await _context.AddRangeAsync(testAssessments);
            //    await _context.SaveChangesAsync();
            //};

        }
    }
}
// TODO: ADD Logging service SeriLog - done
//update program file 2 part: pileline and app services- done
// , redis, elasticsearch, 
// TODO: add data seed  - customer seed data extention. 
