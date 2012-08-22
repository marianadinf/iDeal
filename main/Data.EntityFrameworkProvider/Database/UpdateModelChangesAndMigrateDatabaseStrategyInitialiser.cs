using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIT.iDeal.Common.Interfaces.Data;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Database
{
    /// <summary>
    /// when needed
    /// http://blogs.msdn.com/b/adonet/archive/2012/02/09/ef-4-3-code-based-migrations-walkthrough.aspx
    /// </summary>
    public class UpdateModelChangesAndMigrateDatabaseStrategyInitialiser : IDatabaseStrategyInitialiser
    {
        public void Apply()
        {
            throw new NotImplementedException();
            //System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, DbMigrationsConfiguration<DataContext>>());
            
        }
    }
}
