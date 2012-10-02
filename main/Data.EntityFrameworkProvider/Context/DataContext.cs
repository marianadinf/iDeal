using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Seterlund.CodeGuard;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;
//using UIT.iDeal.Data.EntityFrameworkProvider.Extensions;
using Module = UIT.iDeal.Domain.Model.ReferenceData.Module;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Context
{
    /// <summary>
    /// http://kazimanzurrashid.com/posts/entity-framework-code-first-bootstrapping
    /// Collect all the mappings and register it automatically
    /// </summary>
    public class DataContext : DbContext
    {
        #region Members
        
        private readonly IDictionary<MethodInfo, object> configurations;
        
        #endregion

        #region Entities Code First

        public IDbSet<Task> Tasks { get; set; }
        public IDbSet<BusinessUnit> BusinessUnits { get; set; }
        public IDbSet<ApplicationRole> ApplicationRoles { get; set; }
        public IDbSet<Stage> Stages { get; set; }
        public IDbSet<User> Users { get; set; } 
        public IDbSet<Module> Modules { get; set; } 

        #endregion

        #region Constructor

        public DataContext(
            string nameOrConnectionString,
            IDictionary<MethodInfo, object> configurations)
            : base(nameOrConnectionString)
        {
            Guard.That(configurations).IsNotNull();
            this.configurations = configurations;
        }

        #endregion

        #region Public methods

        public IQueryable<T> CreateQueryWith<T>(IEnumerable<Expression<Func<T, object>>> propertySelectors = null)
            where T : class
        {

            IQueryable<T> set = RetrieveSet<T>();

            if (propertySelectors != null && propertySelectors.Any())
            {
                set = propertySelectors.Aggregate(set, (current, propertySelector) => current.Include(propertySelector));
            }

            return set;
        }

        public IDbSet<T> RetrieveSet<T>() 
            where T : class
        {
            var entityType = typeof (T);
            var closedDbSetType = typeof (IDbSet<>).MakeGenericType(entityType);

            var property = this.FindPropertyMatching(p => p.PropertyType == closedDbSetType);

            if (property == null)
                throw new PropertyNotFoundException(string.Format("No IDbset of {0} property found in DataContext",
                                                                  entityType.Name));

            return (IDbSet<T>)property.GetValue(this, null);

        }


        public virtual void MarkAsModified<TEntity>(TEntity instance)
            where TEntity : class
        {
            Entry(instance).State = EntityState.Modified;
        }

        #endregion

        #region Auto fluent configuration

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Guard.That(modelBuilder).IsNotNull();

            configurations
                .Each(config => config
                                    .Key
                                    .Invoke(modelBuilder.Configurations, new[] {config.Value}));

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}