using UIT.iDeal.Common.Interfaces.Data.Repositories.Write;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Repositories.Write
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        { }
    }
}