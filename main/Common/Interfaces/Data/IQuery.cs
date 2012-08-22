using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UIT.iDeal.Common.Data;
using UIT.iDeal.Domain;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Common.Interfaces.Data
{
    
    public interface IQuery<T> : IQueryWithTypedId<T,Guid> where T : Entity
    {
        
    }

    public interface IPaginatedQuery<T> : IPaginatedQueryWithTypedId<T,Guid> where T : Entity
    {
        
    }

    public interface IPaginatedQueryWithTypedId<T,TId> : IQueryWithTypedId<T,TId>
        where T : EntityWithTypedId<TId>
    {
        PagedResult<T> PagedList(IQueryable<T> query, int pageNumber, int pageSize, string sort);
        PagedResult<T> PagedList(int pageNumber, int pageSize, string sort);
        PagedResult<T> PagedList(IQueryable<T> query, IPagedQueryObject pagedQueryObject);
        PagedResult<T> PagedList(IPagedQueryObject pagedQueryObject);
    }

    public interface IQueryWithTypedId<T,TId> where T : EntityWithTypedId<TId>
    {

        /// <summary>
        /// Returns null if a row is not found matching the provided Id.
        /// </summary>
        T Get(TId id);

        /// <summary>
        /// Get all the entities
        /// </summary>
        /// <param name="predicate">when provided will filter entity statisfying the predicate</param>
        /// <param name="includes">when provided will force to load specified related properties with entities</param>
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null,
                             params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Get one entity matching a condition
        /// </summary>
        /// <param name="predicate">will ensure only one or none statisfy the predicate</param>
        /// <param name="includes">when provided will force to load specified related properties with the entity</param>
        T GetOne(Expression<Func<T, bool>> predicate,
                             params Expression<Func<T, object>>[] includes);
        
    }
}