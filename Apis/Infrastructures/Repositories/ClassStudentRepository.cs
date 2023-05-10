using Application.Commons;
using Application.Interfaces;
using Application.Repositories;
using Application.Students.DTO;
using Domain.Entities;
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

      public async Task<Pagination<StudentProgressDTO>> GetPagedStudentProgressesById(
        int id,
        int pageNumber = 1,
        int pageSize = 10)
    {
        // var query = _dbSet.AsQueryable().AsNoTracking();

        // // filter
        // query = query.Where(x => x.Id == id);

        // // include
        // query = query
        //     .Include(x => x.ClassStudents)
        //         .ThenInclude(x => x.TrainingClass)
        //         .ThenInclude(x => x.TestAssessments)
        //         .ThenInclude(x => x.Syllabus);

        // for (int i = 0; i < pageSize; i++)
        // {
        //     // 0, 1
        //     // 2, 3
        //     // 4
        //     var date = start.AddDays(i);
        //     reportUsersByDates.Add(new ReportUsersByDate
        //     {
        //         Date = date,
        //         UserCount = await query.Where(x => x.CreatedAt.Date == date.Date).CountAsync(),
        //     });
        // }
        // var result = new ItemResult<ReportUsersByStartEnd>(
        //     item: new ReportUsersByStartEnd()
        //     {
        //         Start = start,
        //         End = end,
        //         UserCount = await query.CountAsync(),
        //         ReportUsersByDates = reportUsersByDates
        //     },
        //     count: (end - start).Days + 1,
        //     pageIndex: pageIndex,
        //     pageSize: pageSize
        // );

        var result = new Pagination<StudentProgressDTO>()
        {
            PageIndex = pageNumber,
            PageSize = pageSize,
            Items = new List<StudentProgressDTO>(){
                new StudentProgressDTO(){
                    ClassId = 1,
                    StudentGPA = 8.5f,
                    ClassGPA = 6.9f
                },
            }
        };

        return result;
    }
}
