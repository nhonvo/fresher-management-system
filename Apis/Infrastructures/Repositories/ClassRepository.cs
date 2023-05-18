using Application.Common.Exceptions;
using Application.Repositories;
using Application.ViewModels;
using Domain.Entities;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories;

public class ClassRepository : GenericRepository<TrainingClass>, IClassRepository
{
    private readonly ApplicationDbContext _context;

    public ClassRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ClassDuration> GetClassDurationAsync(int classId)
    {
        var isExistClass = await _context.TrainingClasses.AnyAsync(x => x.Id == classId);
        if (!isExistClass)
            throw new NotFoundException(nameof(TrainingClass), $"Id = {classId} not found");
        var isExistTrainingProgram = await _context.TrainingClasses
            .Where(x => x.Id == classId)
            .Select(x => x.TrainingProgram)
            .AnyAsync();
        if (!isExistTrainingProgram)
            return new ClassDuration()
            {
                Days = 0,
                Hours = 0
            };
        var hours = await _context.TrainingClasses
            .Where(x => x.Id == classId)
            .Select(x => x.TrainingProgram)
            .SelectMany(x => x!.ProgramSyllabus)
            .Select(x => x.Syllabus)
            .SelectMany(x => x.Units)
            .SelectMany(x => x.UnitLessons)
            .Select(x => x.Duration)
            .SumAsync(); ;
        var result = new ClassDuration()
        {
            Days = hours / 8,
            Hours = hours
        };
        return result;
    }
}
