using System.Text.Json;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
            if (_context.Users.Any()) return;
            string json = File.ReadAllText(@"../../Json/user.json");
            List<User> users = JsonSerializer.Deserialize<List<User>>(json);

            await _context.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }
    }
}
// TODO: ADD Logging service SeriLog - done
//update program file 2 part: pileline and app services- done
// , redis, elasticsearch, 
// TODO: add data seed  - customer seed data extention. 
