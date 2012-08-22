using System.Data.Entity;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Data.EntityFrameworkProvider.Database;
using UIT.iDeal.Data.EntityFrameworkProvider.Database.Initialisation;
using UIT.iDeal.Domain.Model.ReferenceData;
using UIT.iDeal.Infrastructure.Configuration;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.IntegrationTests.Data.EntityFrameworkProvider.Database
{
    public class when_always_dropping_and_initialising_database : reference_data_with<AlwaysDropAndRecreateDatabaseStrategyInitialiser>
    {
        It should_call_Populate_on_data_context_reference_data_initialiser = () =>
           The<IDataContextReferenceDataInitialiser>().WasToldTo(x => x.Populate(_dataContext));
    }

    public class when_dropping_if_model_changes_and_initialising_database : reference_data_with<DropAndRecreateWhenModelChangesDatabaseStrategyInitialiser>
    {
        Establish context = () =>
            _dataContext.Database.Delete();

        It should_call_Populate_on_data_context_reference_data_initialiser = () =>
           The<IDataContextReferenceDataInitialiser>().WasToldTo(x => x.Populate(_dataContext));
    }

    public abstract class reference_data_with<TDatabaseInitialiser> : WithSubject<TDatabaseInitialiser>
        where TDatabaseInitialiser : class, IDatabaseInitializer<DataContext>
    {
        protected static DataContext _dataContext;

        Establish context = () =>
        {
            System.Data.Entity.Database.SetInitializer(Subject);
            
            var environment = ConfigurationFactory.DevelopmentEnvironment();
            var contextFactory = new DataContextFactory(environment.ConnectionString(ProjectFlavour.IntegrationTests));
            _dataContext = contextFactory.Create();
        };

        Because of = () =>
            Subject.InitializeDatabase(_dataContext);



    }
}
