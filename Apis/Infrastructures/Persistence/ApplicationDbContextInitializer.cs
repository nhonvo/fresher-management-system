// using Domain.Entities;
// using Microsoft.EntityFrameworkCore;
// using Serilog;
// using System.Text.Json;

// namespace Infrastructures.Persistence
// {
//     public class ApplicationDbContextInitializer
//     {
//         private readonly ApplicationDbContext _context;
//         public ApplicationDbContextInitializer(ApplicationDbContext context)
//         {
//             _context = context;
//         }
//         public async Task InitialiseAsync()
//         {
//             try
//             {
//                 await _context.Database.MigrateAsync();

//             }
//             catch (Exception ex)
//             {
//                 // System.Console.WriteLine(ex.ToString(), "Migration error");
//                 Log.Error(ex, "Migration error");
//             }
//         }

       
//     }
// }