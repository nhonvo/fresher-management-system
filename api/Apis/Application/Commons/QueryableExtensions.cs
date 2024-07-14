
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Commons;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> IncludeMultiple<TEntity>(
        this IQueryable<TEntity> query,
        params Expression<Func<TEntity, object>>[] includes) where TEntity : BaseEntity
    {
        if (includes != null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        return query;
    }

    public static IIncludableQueryable<TEntity, TProperty> MyInclude<TEntity, TProperty>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class
    {
        return EntityFrameworkQueryableExtensions.Include(source, navigationPropertyPath);
    }

    public static IIncludableQueryable<TEntity, TProperty> MyThenInclude<TEntity, TPreviousProperty, TProperty>(
        this IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>> source,
        Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class
    {
        return EntityFrameworkQueryableExtensions.ThenInclude(source, navigationPropertyPath);
    }

    public static IQueryable<TEntity> Filter<TEntity>(
        this IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int pageIndex = 0,
        int pageSize = 10,
        bool pageIndexStartsFromZero = true,
        bool tracked = false) where TEntity : BaseEntity
    {
        // Apply AsNoTracking if it is required
        if (!tracked)
            query = query.AsNoTracking();

        // Apply filter if it is provided
        if (filter != null)
            query = query.Where(filter);

        // Apply include if it is provided
        if (include != null)
            query = include(query);

        // Apply orderBy if it is provided
        if (orderBy != null)
            query = orderBy(query);

        // Apply skip and take
        query = pageIndex == 1 ? query.Take(pageSize) :
            query.Skip((pageIndexStartsFromZero ? pageIndex : (pageIndex - 1)) * pageSize).Take(pageSize);

        return query;
    }
}
