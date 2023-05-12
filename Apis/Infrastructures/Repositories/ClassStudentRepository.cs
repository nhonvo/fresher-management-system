using Application.Commons;
using Application.Interfaces;
using Application.Repositories;
using Application.Students.DTO;
using Domain.Entities;
using Domain.Enums;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories;

public class ClassStudentRepository : GenericRepository<ClassStudent>, IClassStudentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ClassStudentRepository(ApplicationDbContext context, ICacheService cache) : base(context, cache)
    {
        _dbContext = context;
    }
}
