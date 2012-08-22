using System.Data.Entity;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Database
{
    public class NullDatabaseStrategyInitialiser : IDatabaseStrategyInitialiser
    {
        internal class EFNullDatabaseInitializer<TContext> : IDatabaseInitializer<TContext>
        where TContext : DbContext
        {
            public void InitializeDatabase(TContext context)
            {
            }
        }

        public void Apply()
        {
            System.Data.Entity.Database.SetInitializer(new EFNullDatabaseInitializer<DataContext>());
        }
    }
}