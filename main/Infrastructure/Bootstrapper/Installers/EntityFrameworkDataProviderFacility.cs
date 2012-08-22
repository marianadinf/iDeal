using System;
using Castle.Core;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Data.EntityFrameworkProvider.Database;
using UIT.iDeal.Data.EntityFrameworkProvider.Database.Initialisation;
using UIT.iDeal.Data.EntityFrameworkProvider.Repositories;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Installers
{
    /// <summary>
    /// This is the one place where the Ioc Registers for Entity framework's implementation
    /// as a data provider
    /// This facility could be swaped with another Facility for an ORM like Nhibernate
    /// </summary>
    public class EntityFrameworkDataProviderFacility : AbstractFacility
    {
        #region Members
        
        private static Type _databaseInitialiserStrategyType = typeof(DropAndRecreateWhenModelChangesDatabaseStrategyInitialiser);
        private static LifestyleType _contextLifeStyle = LifestyleType.PerWebRequest;
        
        #endregion

        #region properties
        
        public static Func<string> ConnectionStringProvider = () =>
        {
            throw new ArgumentException("A connection string must be set.");
        };

        #endregion

        protected override void Init()
        {
            //Data Context, its factory and Unit of work
            //For Application Enviroment the Data Context and Unit Of Work life cycle are per web request and not managed externaly by its factory
            Kernel.Register(Component.For<IDataContextFactory>()
                                .Instance(new DataContextFactory(ConnectionStringProvider()))
                                .LifestyleSingleton(),
                            Component.For<DataContext>()
                                .UsingFactoryMethod(k => k.Resolve<IDataContextFactory>().Create())
                                .LifeStyle.Is(_contextLifeStyle),
                            Component.For<IUnitOfWork>()
                                .ImplementedBy<UnitOfWork>()
                                .LifeStyle.Is(_contextLifeStyle));

            //Query (Read only) Repository
            Kernel.Register(Types.FromAssemblyContaining<TasksQuery>()
                                   .BasedOn(typeof(IQuery<>))
                                   .WithService.DefaultInterfaces()
                                   .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)));

            //Write Repository
            Kernel.Register(Types.FromAssemblyContaining<TasksRepository>()
                                 .BasedOn(typeof(IRepository<>))
                                 .WithService.DefaultInterfaces()
                                 .Configure(c => c.LifeStyle.Is(LifestyleType.Transient)));

            //Database strategy initialiser
            Kernel.Register(Component.For<IDatabaseStrategyInitialiser>().ImplementedBy(_databaseInitialiserStrategyType));

            //Reference data / lookup initialiser
            Kernel.Register(Component.For<IDataContextReferenceDataInitialiser>()
                                .ImplementedBy<DataContextReferenceDataInitialiser>());
        }

        public static void WithDatabaseInitialiser<TDatabaseInitialiser>()
            where TDatabaseInitialiser:IDatabaseStrategyInitialiser
        {
            WithDatabaseInitialiser(typeof(TDatabaseInitialiser));
        }

        public static void WithDatabaseInitialiser(Type databaseInitialiserStrategyType)
        {
            _databaseInitialiserStrategyType = databaseInitialiserStrategyType;
        }

        public static void WithContextLifeStyle(LifestyleType contextLifestyle)
        {
            _contextLifeStyle = contextLifestyle;
        }
    }

}
