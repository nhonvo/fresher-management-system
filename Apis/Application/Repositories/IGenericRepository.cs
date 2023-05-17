using Application.Commons;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Adds the specified entity to the repository asynchronously. This method is called when the user clicks the Add button in the form.
        /// </summary>
        /// <param name="entity">The entity to add to the repository as an asynchronous operation</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Adds a range of entities to the storage asynchronously. This is done by calling the Add method of the TEntity storage class
        /// </summary>
        /// <param name="entity">The entities to add</param>
        Task AddRangeAsync(IEnumerable<TEntity> entity);

        /// <summary>
        /// Determines whether any entity matches the filter. This is a shortcut for LINQ'any '
        /// </summary>
        /// <param name="filter">The filter to match</param>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> AnyAsync();

        /// <summary>
        /// Gets the count asynchronous. This is the equivalent of the LINQ COUNT ( filter )
        /// </summary>
        /// <param name="filter">The filter to use</param>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Gets the number of items in the queue. This is an asynchronous operation and returns a
        /// </summary>
        Task<int> CountAsync();
        /// <summary>
        /// Gets the entity with the specified id. If no entity is found with the specified id null is returned
        /// </summary>
        /// <param name="id">The id of the</param>
        Task<TEntity> GetByIdAsync(object id);
        /// <summary>
        /// Gets a paged list of entities. You can use this method to paginate through a list of entities that match a filter and / or include the entity by id or name
        /// </summary>
        /// <param name="filter">Expression that determines which entities to return</param>
        /// <param name="include">Include this entity in the result ( optional )</param>
        /// <param name="pageIndex">Index of the page ( optional ).</param>
        /// <param name="pageSize">Size of the page ( optional ). Default is 10</param>
        Task<Pagination<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
            int pageIndex = 0,
            int pageSize = 10);

        /// <summary>
        /// Get entities by filter. This is a async operation. You can use it to get a subset of entities that match the filter
        /// </summary>
        /// <param name="filter">Expression that determines which entities to get</param>
        /// <param name="pageIndex">Index of page to start retrieving from.</param>
        /// <param name="pageSize">Size of page to retrieve from the API ( default 10</param>
        Task<Pagination<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            int pageIndex = 0,
            int pageSize = 10);
        Task<Pagination<TEntity>> GetAsync<TKey>(
                   Expression<Func<TEntity, bool>> filter = null,
                   Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null,
                   Expression<Func<TEntity, TKey>> keySelectorForSort = null,
                   SortType sortType = SortType.Ascending,
                   int pageIndex = 0,
                   int pageSize = 10);
        Task<Pagination<TEntity>> ToPagination(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int pageIndex = 0,
            int pageSize = 10,
            bool pageIndexStartsFromZero = true,
            bool tracked = false);

        /// <summary>
        /// Converts the data to a pagination. Used to get the list of entities that have been added to the data store
        /// </summary>
        /// <param name="pageNumber">The page number to start from.</param>
        /// <param name="pageSize">The page size to start from ( default 10</param>
        Task<Pagination<TEntity>> ToPagination(int pageNumber = 0, int pageSize = 10);
        /// <summary>
        /// Updates the entity in the database. This is called when an entity is updated and should be used to make changes to the data source.
        /// </summary>
        /// <param name="entity">The entity to update in the database. This can be a new entity or a part of an existing</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the range of entities. This is called when entities are added or removed from the list
        /// </summary>
        /// <param name="entities">The entities to update</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the specified entity. This is an asynchronous operation. To wait for the operation to complete call GetOperationStatus
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes a range of entities from the repository. This is useful for deleting entities that are no longer needed and can be reinserted in the database
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes the object with the specified identifier. This is an asynchronous operation. A task will return before the object has been deleted.
        /// </summary>
        /// <param name="id">The identifier of the object to delete. This can be a for the current thread or a for a sub - thread</param>
        Task Delete(object id);
        /// <summary>
        /// Removes the specified entity from the repository. This is useful for entities that are no longer part of the repository.
        /// </summary>
        /// <param name="entity">The entity to remove from the repository. This can be a null reference ( Nothing in Visual Basic ) if the entity is not part of the repository</param>
        public void SoftRemove(TEntity entity);
        /// <summary>
        /// Removes a range of entities from the repository. This is useful for undoing changes that are made to the repository while it is being used to perform a soft remove.
        /// </summary>
        /// <param name="entities">The entities to remove from the repository. This can be null</param>
        public void SoftRemoveRange(IEnumerable<TEntity> entities);
        /// <summary>
        /// Gets the first entity that matches the filter or null if no entities match.
        /// </summary>
        /// <param name="filter">The filter to match entities by. Cannot be null</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> FirstOrdDefaultAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null);
        Task<TEntity> GetByIdAsyncAsNoTracking(object id);
    }
}
