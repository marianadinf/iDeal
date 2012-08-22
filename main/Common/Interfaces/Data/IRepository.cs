using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UIT.iDeal.Domain;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Common.Interfaces.Data
{
    /// <summary>
    ///     Provides a standard interface for Repositories which are data-access mechanism agnostic.
    /// 
    ///     Since nearly all of the domain objects you create will have a type of int Id, this 
    ///     base IRepository assumes that.  If you want an entity with a type 
    ///     other than int, such as string, then use <see cref = "IRepositoryWithTypedId{T, IdT}" />.
    /// </summary>
    public interface IRepository<T> : IRepositoryWithTypedId<T, Guid> where T : AggregateRoot { }

    public interface IRepositoryWithTypedId<T, TId> : IQueryWithTypedId<T,TId> 
        where T : EntityWithTypedId<TId>
    {
        
        /// <summary>
        /// For entities with automatically generated Ids, such as identity or Hi/Lo, Save may 
        /// be called when saving or updating an entity.  If you require separate Save and Update
        /// methods, you'll need to extend the base repository interface when using NHibernate.
        /// 
        /// Updating also allows you to commit changes to a detached object.  More info may be found at:
        /// </summary>
        T Save(T entity);
            
        IEnumerable<T> SaveList(List<T> entities, bool resetIdSequence = false);

        /// <summary>
        /// I'll let you guess what this does.
        /// </summary>  
        void Delete(T entity);
    }
}