using System;
using System.Linq;
using System.Collections.Generic;
using Seterlund.CodeGuard;
using UIT.iDeal.Common.Data;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Domain;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Repositories
{
    public class Repository<T> : RepositoryWithTypedId<T, Guid>, IRepository<T> 
        where T : AggregateRoot
    {
        public Repository(DataContext context) : base(context) { }

        public override Guid GenerateId()
        {
            return GuidComb.Generate();
        }

        public override void ResetIdSequence()
        {
            GuidComb.Reset();
        }

        public override T Get(Guid id)
        {
            return GetOne(entity => entity.Id == id);
        }
        
    }

    public abstract class RepositoryWithTypedId<T, TId> : QueryWithTypedId<T,TId>, IRepositoryWithTypedId<T, TId>
        where T : EntityWithTypedId<TId>
    {
   
        protected RepositoryWithTypedId(DataContext context) : base(context)
        { }

        public abstract TId GenerateId();

        public abstract void ResetIdSequence();

        public virtual T Save(T entity)
        {
            Guard.That(entity).IsNotNull();

            AddToContextIfTransient(entity);

            Context.SaveChanges();
            return entity;

        }
        
        public IEnumerable<T> SaveList(List<T> entities, bool resetIdSequence = false)
        {
            Guard.That(entities).IsNotNull();

            if (resetIdSequence)
            {
                ResetIdSequence();
            }

            var result = entities.Each(AddToContextIfTransient);

            Context.SaveChanges();

            return result;
        }
            
        public virtual void Delete(T entity) {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }


        private void AddToContextIfTransient(T entity)
        {
            if (entity.IsTransient())
            {
                entity.SetValue(x => x.Id, GenerateId());
                Context.Set<T>().Add(entity);    
            }
        }

    }
}
