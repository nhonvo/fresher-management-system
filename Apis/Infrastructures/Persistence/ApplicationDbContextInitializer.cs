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
            // string json = File.ReadAllText("customers.json");
            // List<User> customers = JsonSerializer.Deserialize<List<User>>(json);

            var users = new User[]
            {
                new User
                {
                    FullName = "Võ Thương Trường Nhơn",
                    Email ="vothuongtruongnhon2002@gmail.com",
                    Password ="$2a$11$JpdSbKC0DPnarMb4CfQ8tOoa2GcuPCweteqA2kk8tn7RUiPOqIMGi",
                    Phone ="0905726748",
                    DateOfBirth =new DateTime(2002,10,30),
                    Role = Domain.Enums.UserEnums.UserRole.Trainee,
                    IsMale = true,
                    AvatarURL = "Null"
                },
                new User
                {
                    FullName = "Võ Thành Đô",
                    Email ="vothanhdo@gmail.com",
                    Password ="$2a$11$JpdSbKC0DPnarMb4CfQ8tOoa2GcuPCweteqA2kk8tn7RUiPOqIMGi",
                    Phone ="0905726748",
                    DateOfBirth =new DateTime(2000,10,30),
                    Role = Domain.Enums.UserEnums.UserRole.Trainee,
                    IsMale = true,
                    AvatarURL = "Null"
                },
                new User
                {
                    FullName = "Bình Đẹp Trai",
                    Email ="binhdeptrai@gmail.com",
                    Password ="$2a$11$JpdSbKC0DPnarMb4CfQ8tOoa2GcuPCweteqA2kk8tn7RUiPOqIMGi",
                    Phone ="0905726748",
                    DateOfBirth =new DateTime(2002,10,30),
                    Role = Domain.Enums.UserEnums.UserRole.Trainee,
                    IsMale = true,
                    AvatarURL = "Null"
                },
                new User
                {
                    FullName = "Đạt Lớp Trưởng",
                    Email ="datloptruong@gmail.com",
                    Password ="$2a$11$JpdSbKC0DPnarMb4CfQ8tOoa2GcuPCweteqA2kk8tn7RUiPOqIMGi",
                    Phone ="0905726748",
                    DateOfBirth =new DateTime(2000,10,30),
                    Role = Domain.Enums.UserEnums.UserRole.Trainee,
                    IsMale = true,
                    AvatarURL = "Null"
                },
                new User
                {
                    FullName = "Nam Lớn",
                    Email ="namlon@gmail.com",
                    Password ="$2a$11$JpdSbKC0DPnarMb4CfQ8tOoa2GcuPCweteqA2kk8tn7RUiPOqIMGi",
                    Phone ="0905726748",
                    DateOfBirth =new DateTime(2000,10,30),
                    Role = Domain.Enums.UserEnums.UserRole.Trainee,
                    IsMale = true,
                    AvatarURL = "Null"
                }
            };

            await _context.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }
    }
}
// TODO: ADD Logging service SeriLog - done
//update program file 2 part: pileline and app services- done
// , redis, elasticsearch, 
// TODO: add data seed  - customer seed data extention. 
