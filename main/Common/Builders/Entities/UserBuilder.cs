using UIT.iDeal.Common.Builders.Entities.Framework;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.Common.Builders.Entities
{
    public class UserBuilder : EntityBuilder<User>
    {
        public UserBuilder(int listSize)
            : base(listSize)
        {

        }

        public UserBuilder()
        {

        }
    }
}