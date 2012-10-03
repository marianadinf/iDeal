
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UIT.iDeal.Domain.Model.ReferenceData;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Common.Interfaces.Data.Repositories.Read;


namespace UIT.iDeal.Data.EntityFrameworkProvider.Repositories.Read
{
    public class ModuleQuery : Query<Module>, IModuleQuery
    {
        public ModuleQuery(DataContext context) : base(context)
        {
            
        }

        public override IQueryable<Module> GetAll(Expression<Func<Module, bool>> predicate = null)
        {
            return QueryAllWith(predicate, x => x.internalApplicationRoles);
        }
    }
}