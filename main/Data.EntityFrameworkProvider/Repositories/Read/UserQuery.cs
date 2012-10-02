using UIT.iDeal.Common.Interfaces.Data.Repositories.Read;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Repositories.Read
{
    public class UserQuery : Query<User>, IUserQuery
    {
        public UserQuery(DataContext context) :base(context)
        { }
    }
}