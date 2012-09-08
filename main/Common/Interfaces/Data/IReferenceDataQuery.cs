using System;
using System.Collections.Generic;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.Interfaces.Data
{
    public interface IReferenceDataQuery<TReferenceData> : IQuery<TReferenceData>
        where TReferenceData : ReferenceData
    {
        
        IEnumerable<TReferenceData> GetAllCachedForSelectedIds(IList<Guid> referenceDataIds);

        IEnumerable<TReferenceData> GetAllCached();


    }
}