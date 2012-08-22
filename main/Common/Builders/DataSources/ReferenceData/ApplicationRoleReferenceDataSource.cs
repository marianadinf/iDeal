using System.Collections.Generic;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.Builders.DataSources.ReferenceData
{
    public class ApplicationRoleReferenceDataSource : ReferenceDataSource<ApplicationRole>
    {
        protected override List<ApplicationRole> InitialiseList()
        {
            return new List<ApplicationRole>
            {
                new ApplicationRole()
                    .SetValue(x => x.Code, "ASSMAN")
                    .SetValue(x => x.Description, "Asset Manager"),

                new ApplicationRole()
                    .SetValue(x => x.Code, "ASSAN")
                    .SetValue(x => x.Description, "Asset Analyst"),

                new ApplicationRole()
                    .SetValue(x => x.Code, "INVAN")
                    .SetValue(x => x.Description, "Investment Analyst"),

                new ApplicationRole()
                    .SetValue(x => x.Code, "INVASS")
                    .SetValue(x => x.Description, "Investment Associate"),

                new ApplicationRole()
                    .SetValue(x => x.Code, "DEALLDR")
                    .SetValue(x => x.Description, "Deal Leader"),

                new ApplicationRole()
                    .SetValue(x => x.Code, "ADMIN")
                    .SetValue(x => x.Description, "Administrator"),

                new ApplicationRole()
                    .SetValue(x => x.Code, "ASSPWRUSR")
                    .SetValue(x => x.Description, "Asset Power User"),

                new ApplicationRole()
                    .SetValue(x => x.Code, "INVPWRUSR")
                    .SetValue(x => x.Description, "Investment Power User"),


            };
        }
    }
}