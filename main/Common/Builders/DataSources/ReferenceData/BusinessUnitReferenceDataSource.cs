using System.Collections.Generic;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.Builders.DataSources.ReferenceData
{
    public class BusinessUnitReferenceDataSource : ReferenceDataSource<BusinessUnit>
    {
        protected override List<BusinessUnit> InitialiseList()
        {
            return new List<BusinessUnit>
            {
                BusinessUnit.Create("Business Unit Not Allocated"),
                BusinessUnit.Create("Business Unit 1"),
                BusinessUnit.Create("Business Unit 2"),
            };
        }
    }
}