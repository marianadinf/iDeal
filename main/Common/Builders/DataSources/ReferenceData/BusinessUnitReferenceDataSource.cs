using System.Collections.Generic;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.Builders.DataSources.ReferenceData
{
    public class BusinessUnitReferenceDataSource : ReferenceDataSource<BusinessUnit>
    {
        protected override List<BusinessUnit> InitialiseList()
        {
            return new List<BusinessUnit>
            {
                new BusinessUnit().SetValue(x => x.Description,"Business Unit Not Allocated"),
                new BusinessUnit().SetValue(x => x.Description,"Business Unit 1"),
                new BusinessUnit().SetValue(x => x.Description,"Business Unit 2"),
            };
        }
    }
}