using Application;
using Application.Common.Exceptions;
using Application.Repositories;
using Infrastructures.Persistence;
using Infrastructures.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructures;

public class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private bool _disposed;
    //
    private readonly ApplicationDbContext _context;
    // repositories
    public IUserRepository UserRepository { get; }
    public ISyllabusRepository SyllabusRepository { get; }
    public IOutputStandardRepository OutputStandardRepository { get; }
    public IClassRepository ClassRepository { get; }
    //
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _context = dbContext;
        // repositories
        UserRepository = new UserRepository(_context);
        SyllabusRepository = new SyllabusRepository(_context);
        OutputStandardRepository = new OutputStandardRepository(_context);
        ClassRepository = new ClassRepository(_context);
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
            throw new TransactionException("No transaction to commit");
        try
        {
            _context.SaveChanges();
            _transaction.Commit();
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
            throw new TransactionException("No transaction to commit");
        try
        {
            await _context.SaveChangesAsync();
            _transaction.Commit();
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
            throw new TransactionException("No transaction to rollback");
        _transaction.Rollback();
        _transaction.Dispose();
        _transaction = null;
    }

    public async Task RollbackAsync()
    {
        if (_transaction == null)
            throw new TransactionException("No transaction to rollback");
        await _transaction.RollbackAsync();
        _transaction.Dispose();
        _transaction = null;
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
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw new TransactionException("Can't execute transaction");
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
