using Domain.Entities;

namespace Application.Repositories
{
    public interface IUnitRepository : IGenericRepository<Unit>
    {
    }
    public interface ITrainingProgramRepository : IGenericRepository<TrainingProgram>
    {
    }
    public interface IProgramSyllabusRepository : IGenericRepository<ProgramSyllabus>
    {
    }
}
