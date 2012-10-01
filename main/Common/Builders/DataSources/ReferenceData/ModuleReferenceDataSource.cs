using System.Collections.Generic;
using System.Linq;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.Builders.DataSources.ReferenceData
{
    public class ModuleReferenceDataSource : ReferenceDataSource<Module>
    {
        protected override List<Module> InitialiseList()
        {

            return new List<Module>
            {
                new Module()
                    .SetValue(x => x.Description, "Asset Module Lookup - Create/ Edit"),

                new Module()
                    .SetValue(x => x.Description, "Asset - View"),

                new Module()
                    .SetValue(x => x.Description, "Asset - Create"),

            };
        }
    }
}