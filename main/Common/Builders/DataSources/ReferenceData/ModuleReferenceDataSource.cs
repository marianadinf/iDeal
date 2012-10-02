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
                Module.Create("ADDASSETLOOKUP","Asset Module Lookup - Create/ Edit"),
                Module.Create("VIEWASSET","Asset - View"),
                Module.Create("ADDASSET","Asset - Create"),
            };
        }
    }
}