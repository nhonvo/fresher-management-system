using Application;
using Application.Common.Exceptions;
using Application.Repositories;
using Infrastructures.Persistence;
using Infrastructures.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;

namespace Infrastructures;

public class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private bool _disposed;
    //
    private readonly ApplicationDbContext _context;

    // repositories
    public IAttendanceRepository AttendanceRepository { get; }
    public IApproveRequestRepository ApproveRequestRepository { get; }
    public IClassRepository ClassRepository { get; }
    public IClassStudentRepository ClassStudentRepository { get; }
    public IOutputStandardRepository OutputStandardRepository { get; }
    public ISyllabusRepository SyllabusRepository { get; }
    public ITestAssessmentRepository TestAssessmentRepository { get; }
    public IUserRepository UserRepository { get; }
    public IUnitRepository UnitRepository { get; }
    public ILessonRepository LessonRepository { get; }
    public IClassTrainerRepository ClassTrainerRepository { get; }
    public ITrainingProgramRepository TrainingProgramRepository { get; }
    public IFeedBackRepository FeedBackRepository { get; }
    public ICalenderRepository CalenderRepository { get; }
    public IProgramSyllabusRepository ProgramSyllabusRepository { get; }
    public IClassAdminRepository ClassAdminRepository { get; }
    public ITrainingMaterialRepository TrainingMaterialRepository { get; }
    //
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _context = dbContext;
        // repositories
        ClassRepository = new ClassRepository(_context);
        ClassStudentRepository = new ClassStudentRepository(_context);
        UserRepository = new UserRepository(_context);
        SyllabusRepository = new SyllabusRepository(_context);
        OutputStandardRepository = new OutputStandardRepository(_context);
        TestAssessmentRepository = new TestAssessmentRepository(_context);
        AttendanceRepository = new AttendanceRepository(_context);
        ApproveRequestRepository = new ApproveRequestRepository(_context);
        ClassTrainerRepository = new ClassTrainerRepository(_context);
        TrainingProgramRepository = new TrainingProgramRepository(_context);
        LessonRepository = new LessonRepository(_context);
        UnitRepository = new UnitRepository(_context);
        FeedBackRepository = new FeedBackRepository(_context);
        CalenderRepository = new CalenderRepository(_context);
        ProgramSyllabusRepository = new ProgramSyllabusRepository(_context);
        ClassAdminRepository = new ClassAdminRepository(_context);
        TrainingMaterialRepository = new TrainingMaterialRepository(_context);
    }

    // save changes
    public int SaveChanges() => _context.SaveChanges();

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    // transaction
    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    // commit
    public void Commit()
    {
        if (_transaction == null)
        {
            Log.Warning("No transaction to rollback");
            throw new TransactionException("No transaction to commit");
        }
        try
        {
            _context.SaveChanges();
            _transaction.Commit();
            Log.Information("Transaction to rollback");
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public async Task CommitAsync()
    {
        if (_transaction == null)
        {
            Log.Warning("No transaction to rollback");
            throw new TransactionException("No transaction to commit");
        }
        try
        {
            await _context.SaveChangesAsync();
            _transaction.Commit();
            Log.Information("Transaction to rollback");
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null;
        }
    }

    // rollback
    public void Rollback()
    {
        if (_transaction == null)
        {
            Log.Warning("No transaction to rollback");
            throw new TransactionException("No transaction to rollback");
        }
        _transaction.Rollback();
        _transaction.Dispose();
        _transaction = null;
        Log.Information("Transaction to rollback");
    }

    public async Task RollbackAsync()
    {
        if (_transaction == null)
        {
            Log.Warning("No transaction to rollback");
            throw new TransactionException("No transaction to rollback");
        }
        await _transaction.RollbackAsync();
        _transaction.Dispose();
        _transaction = null;
        Log.Information("Transaction to rollback");
    }

    // dispose
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // execute transaction
    public async Task ExecuteTransactionAsync(Action action)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            action();
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            Log.Information("Transaction committed");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Log.Error("Something went wrong can not execute transaction. It roll back");
            throw new TransactionException("Can't execute transaction: " + ex);
        }
    }

    public async Task ExecuteTransactionAsync(Func<Task> action)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await action();
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            Log.Information("Transaction committed");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Log.Error("Something went wrong can not execute transaction. It roll back");
            throw new TransactionException("Can't execute transaction: " + ex);
        }
    }

    public List<object> GetTrackedEntities()
    {
        return _context.ChangeTracker
            .Entries()
            .Where(e => e.State != EntityState.Detached && e.Entity != null)
            .Select(e => e.Entity)
            .ToList();
    }
}
