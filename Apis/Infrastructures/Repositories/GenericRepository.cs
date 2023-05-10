using Application.Commons;
using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructures.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbSet<TEntity> _dbSet;
        private readonly ICacheService _cacheService;
        private readonly string _cacheKey = $"{typeof(TEntity)}";

        public GenericRepository(ApplicationDbContext context, ICacheService cacheService)
        {
            _dbSet = context.Set<TEntity>();
            _cacheService = cacheService;
        }
        // create
        public async Task AddAsync(TEntity entity)
        {
            _cacheService.Remove(_cacheKey);
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            _cacheService.Remove(_cacheKey);
            await _dbSet.AddRangeAsync(entities);
        }

        #region  Read
        public async Task<Pagination<TEntity>> ToPagination(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int pageIndex = 0,
            int pageSize = 10,
            bool pageIndexStartsFromZero = true,
            bool tracked = false)
        {
            var query = _dbSet.AsQueryable();

            // filter
            query = query.Filter(
                filter: filter,
                include: include,
                orderBy: orderBy,
                pageIndex: pageIndex,
                pageSize: pageSize,
                pageIndexStartsFromZero: pageIndexStartsFromZero,
                tracked: tracked);

            var count = await query.CountAsync();
            var items = await query.ToListAsync();

            var result = new Pagination<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = count,
                Items = items,
            };

            return result;
        }

        public async Task<Pagination<TEntity>> ToPagination(int pageIndex = 0, int pageSize = 10)
        {
            if (_cacheService.TryGet(_cacheKey, out Pagination<TEntity> cachedResult))
                return cachedResult;
            var itemCount = await _dbSet.CountAsync();
            var items = await _dbSet.Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .AsNoTracking()
                                    .ToListAsync();

            var result = new Pagination<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };
            _cacheService.Set(_cacheKey, result);

            return result;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter) => await _dbSet.AnyAsync(filter);
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
                return await _dbSet.CountAsync();
            return await _dbSet.CountAsync(filter);
        }
        public async Task<int> CountAsync() => await _dbSet.CountAsync();
        public async Task<TEntity> GetByIdAsync(object id) => await _dbSet.FindAsync(id);
        public async Task<TEntity> GetByIdAsyncAsNoTracking(object id)
                => await _dbSet.AsNoTracking()
                               .FirstOrDefaultAsync(x => x.Id == int.Parse(id.ToString()));
        public async Task<Pagination<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
            int pageIndex = 0,
            int pageSize = 10)
        {
            var query = _dbSet.AsQueryable();

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var itemCount = await query.CountAsync();

            var items = await query.Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            var result = new Pagination<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }

        public async Task<Pagination<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            int pageIndex = 0,
            int pageSize = 10)
        {


            var itemCount = await _dbSet.CountAsync();
            var items = await _dbSet.Where(filter)
                                    .Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .AsNoTracking()
                                    .ToListAsync();

            var result = new Pagination<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
          => await _dbSet.IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(filter);

        public async Task<TEntity> FirstOrdDefaultAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();

            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync(filter);
        }
        #endregion
        #region Update & delete
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            // Update the cached list of entities
            _cacheService.Remove(_cacheKey);

        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            _cacheService.Remove(_cacheKey);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            _cacheService.Remove(_cacheKey);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            _cacheService.Remove(_cacheKey);
        }

        public async Task Delete(object id)
        {
            TEntity entity = await GetByIdAsync(id);
            Delete(entity);
        }

        public void SoftRemove(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void SoftRemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }
        #endregion
    }
}
