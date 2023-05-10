using Application.Commons;
using Application.Students.DTO;
using Domain.Entities;

namespace Application.Repositories;

public interface IClassStudentRepository : IGenericRepository<ClassStudent>
{
    Task<Pagination<StudentProgressDTO>> GetPagedStudentProgressesById(
        int id,
        int pageNumber,
        int pageSize);
}
