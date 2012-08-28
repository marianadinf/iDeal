using System.Collections.Generic;
using UIT.iDeal.Common.Builders.DataSources.Base;

namespace UIT.iDeal.Common.Builders.DataSources.ReferenceData
{
    public abstract class ReferenceDataSource<TReferenceData> : DatasourceBase<TReferenceData>
        where TReferenceData : iDeal.Domain.Model.ReferenceData.ReferenceData, new()
    {
       
    }
}