using System.Data.Entity.ModelConfiguration;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Configuration
{
    public class ModuleConfiguration : EntityTypeConfiguration<Module>
    {
        public ModuleConfiguration()
        {
            Ignore(x => x.ApplicationRoles);

            HasMany(x => x.internalApplicationRoles)
                .WithMany(x => x.internalModules)
                .Map(x =>
                {
                    x.ToTable("ApplicationPermissions");
                    x.MapLeftKey("ModuleId");
                    x.MapRightKey("ApplicationRoleId");
                });
        }
    }
}