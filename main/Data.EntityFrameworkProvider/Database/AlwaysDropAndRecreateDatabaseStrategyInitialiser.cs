using System.Data.Entity;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Data.EntityFrameworkProvider.Database.Initialisation;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Database
{
    public class AlwaysDropAndRecreateDatabaseStrategyInitialiser : DropCreateDatabaseAlways<DataContext>, IDatabaseStrategyInitialiser
    {
        readonly IDataContextReferenceDataInitialiser _referenceDataInitialiser;

        public AlwaysDropAndRecreateDatabaseStrategyInitialiser(IDataContextReferenceDataInitialiser referenceDataInitialiser)
        {
            _referenceDataInitialiser = referenceDataInitialiser;
        }
        
        public void Apply()
        {
            System.Data.Entity.Database.SetInitializer(this);
        }

        protected override void Seed(DataContext context)
        {
            _referenceDataInitialiser.Populate(context);
        }
    }
}