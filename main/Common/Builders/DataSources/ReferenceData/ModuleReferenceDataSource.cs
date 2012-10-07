using System.Collections.Generic;
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
                Module.Create("DELASSET","Asset - Delete"),
                Module.Create("ADDINVLOOKUP","Investment Modle Lookup"),
                Module.Create("VIEWDEAL","Deal - View"),
                Module.Create("ADDDEAL","Deal - Create"),
                Module.Create("DELDEAL","Deal - Delete"),
                Module.Create("SETDEALSTAGEPRO","Deal - Set Stage Prospect - Pre Allocation"),
                Module.Create("SETDEALSTAGEEVAL","Deal - Set Stage Evaluating - Post Allocation"),
                Module.Create("SETDEALSTAGEUC","Deal - Set Stage Under Control - Post LOI"),
                Module.Create("SETDEALPOSTPRE","Deal - Set Due Dilligence - Post Preliminary Post IC"),
                Module.Create("SETDEALCLOSINGPOST","Deal- Set Closing Post Final IC"),
                Module.Create("SETDEALCLOSED","Deal- Set Closed Deal"),
                Module.Create("SETDEALDEAD","Deal- Set Dead Deal"),
            };
        }
    }
}