using Microsoft.EntityFrameworkCore;

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
                // Log.Error(ex, "Migration error");
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
                // Log.Error(ex, "Migration error");
                System.Console.WriteLine(ex.ToString(), "Seeding error");
            }
        }

        public async Task TrySeedAsync()
        {
        }
    }
}
// TODO: ADD Logging service
// TODO: add data seed  - customer seed data extention. update program file 2 part: pileline and app services
