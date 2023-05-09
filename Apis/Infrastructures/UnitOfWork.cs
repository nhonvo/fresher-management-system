using Application;
using Application.Repositories;
using Infrastructures.Persistence;
using Infrastructures.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ISyllabusRepository _syllabusRepository;
        private readonly IUserRepository _userRepository;
        private IDbContextTransaction _transaction;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
            _syllabusRepository = new SyllabusRepository(_context);
        }

        public IUserRepository UserRepository => _userRepository;
        public ISyllabusRepository SyllabusRepository => _syllabusRepository;

        public int SaveChanges() => _context.SaveChanges();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

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
                throw;
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
}
