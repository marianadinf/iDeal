using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Data.EntityFrameworkProvider.Database;
using UIT.iDeal.Data.EntityFrameworkProvider.Database.Initialisation;
using UIT.iDeal.Data.EntityFrameworkProvider.Repositories;
using UIT.iDeal.Infrastructure.Bootstrapper.StartupTasks;
using UIT.iDeal.Infrastructure.Configuration;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Bootstrapper.StartupTasks
{
    public class when_always_dropping_and_recreating_the_database : build_database_with<AlwaysDropAndRecreateDatabaseStrategyInitialiser>
    {
        It should_recreate_the_database_regardless_of_whether_the_domain_model_have_changed_or_not = () =>
            Repository.GetAll().ShouldBeEmpty();
    }

    public class when_initialising_database_with_no_model_changes : build_database_with<DropAndRecreateWhenModelChangesDatabaseStrategyInitialiser>
    {
        It should_not_recreate_the_database = () =>
            Repository.GetAll().ShouldNotBeEmpty();
    }

    #region Database initialisation specification

    [Subject(typeof(DatabaseInitialiserStrategyResolver))]
    public abstract class build_database_with<TDatabaseInitialiser> : WithFakes
        where TDatabaseInitialiser : class,IDatabaseStrategyInitialiser
    {
        private static IWindsorContainer _container;
        private static IDataContextReferenceDataInitialiser _dataContextReferenceDataInitialiser;
        protected static Database Database;
        protected static TasksRepository Repository;
        
        Establish context = () =>
        {
            _dataContextReferenceDataInitialiser = An<IDataContextReferenceDataInitialiser>();

            _container = new WindsorContainer()
                .Register(
                    Component
                        .For<IDataContextReferenceDataInitialiser>()
                        .Instance(_dataContextReferenceDataInitialiser),
                    Component.For<IDatabaseStrategyInitialiser>()
                        .ImplementedBy(typeof (TDatabaseInitialiser)));

                new DatabaseInitialiserStrategyResolver(_container).Execute();

                var connectionString =
                    ConfigurationFactory
                        .DevelopmentEnvironment()
                        .ConnectionString(ProjectFlavour.IntegrationTests);



                var context = new DataContextFactory(connectionString).Create();

                Repository = new TasksRepository(context);

                Repository.Save(new TaskBuilder());
                
                context.SaveChanges();

                Database = context.Database;
            };

        Because of = () =>
            Database.Initialize(true);
                


    }

    #endregion
}