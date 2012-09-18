using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Context
{
    /// <summary>
    /// http://stackoverflow.com/questions/5246466/entity-framework-4-mapping-non-public-properties-with-ctp5-code-first-in-a-p
    /// </summary>
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {

            /*HasMany(u => u.GetPropertyFor<ICollection<ApplicationRole>>())
                .WithMany(r => r.GetPropertyFor<ICollection<User>>());*/
            //this.ObjectAccessor<WishList>.CreateExpression<ICollection<Wish>>("Wishes");
            //HasMany(u => u._businessUnits).WithMany(r => r._users);




        }
    }
}