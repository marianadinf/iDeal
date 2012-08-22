using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

using Seterlund.CodeGuard;

using UIT.iDeal.Domain;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Repositories
{
    public class Query<T> : QueryWithTypedId<T,Guid>, IQuery<T>
        where T : Entity
    {
        public Query(DataContext context)
            : base(context)
        { }

        public override T Get(Guid id)
        {
            return GetOne(entity => entity.Id == id);
        }

        
    }
    
    public abstract class QueryWithTypedId<T,TId> : IQueryWithTypedId<T,TId> 
        where T : EntityWithTypedId<TId> 
    {
        public IDbSet<T> EntityCollection { get; set; }

        protected QueryWithTypedId(DataContext context)
        {
            Guard.That(context).IsNotNull();
            Context = context;
        }

        public virtual DataContext Context { get; private set; }

        public abstract T Get(TId id);

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null,
                                            params Expression<Func<T, object>>[] includes)
        {
            var set = Context.CreateIncludedSet(includes);

            return (predicate == null) ? set : set.Where(predicate);
        }

        public virtual T GetOne(Expression<Func<T, bool>> predicate,
                                params Expression<Func<T, object>>[] includes)
        {
            var set = Context.CreateIncludedSet(includes);

            return set.SingleOrDefault(predicate);
        }

        
    }
}