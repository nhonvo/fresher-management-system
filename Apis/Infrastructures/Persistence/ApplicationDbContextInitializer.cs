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
}
}