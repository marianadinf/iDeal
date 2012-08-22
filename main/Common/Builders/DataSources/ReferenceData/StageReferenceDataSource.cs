using System.Collections.Generic;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.Builders.DataSources.ReferenceData
{
    public class StageReferenceDataSource : ReferenceDataSource<Stage>
    {
        protected override List<Stage> InitialiseList()
        {
            return new List<Stage>
            {
                new Stage()
                    .SetValue(x => x.Name, "Stage 1")
                    .SetValue(x => x.Code, "PREALLOC")
                    .SetValue(x => x.Description, "Stage Prospect - Pre Allocation")
            };
        }
    }
}