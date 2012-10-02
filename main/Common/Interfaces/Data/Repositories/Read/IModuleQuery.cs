using System;
using System.Linq;
using System.Linq.Expressions;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.Interfaces.Data.Repositories.Read
{
    public interface IModuleQuery : IQuery<Module>
    {
        IQueryable<Module> GetAll(Expression<Func<Module, bool>> predicate = null);
    }
}