using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using Machine.Specifications;
using UIT.iDeal.Common.Builders.Entities.Framework;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Infrastructure.Configuration;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.IntegrationTests.Data.EntityFrameworkProvider
{
    public abstract class DatabaseSpec
    {
        protected static DataContextFactory ContextFactory;
        private static string _connectionString;
        protected static DataContext Context;
        protected static UnitOfWork UnitOfWork;
        protected static IObjectContextAdapter ObjectContextAdapter;
        protected static DbTransaction Transaction;

        Establish context = () =>
            {

                System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<DataContext>());
                BuilderSetup.SetDefaultPropertyNamer(new PersistableEntityPropertyNamer(new ReflectionUtil()));
                
                var environment = ConfigurationFactory.DevelopmentEnvironment();
                _connectionString = environment.ConnectionString(ProjectFlavour.IntegrationTests);
                ContextFactory = new DataContextFactory(_connectionString);
                Context = ContextFactory.Create();

                Context.Database.Initialize(true);

                UnitOfWork = new UnitOfWork(Context);

                ObjectContextAdapter = Context;


                if ((ObjectContextAdapter.ObjectContext.Connection.State &
                    ConnectionState.Open) != ConnectionState.Open)
                {
                    ObjectContextAdapter.ObjectContext.Connection.Open();
                }
            };

        Cleanup on_exit = () =>
            UnitOfWork.Dispose();

        
    }
}