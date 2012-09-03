using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Repositories
{
    public class UserQuery : Query<User>, IUserQuery
    {
        public UserQuery(DataContext context) :base(context)
        { }
    }

    public class ReferenceDataQuery<TReferenceData> : Query<TReferenceData>, IReferenceDataQuery<TReferenceData>
        where TReferenceData : ReferenceData
    {
        public ReferenceDataQuery(DataContext context) : base(context)
        {
        }
    }
}