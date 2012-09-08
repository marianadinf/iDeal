using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Repositories
{
    public class ReferenceDataQuery<TReferenceData> : Query<TReferenceData>, IReferenceDataQuery<TReferenceData>
        where TReferenceData : ReferenceData
    {
        static IEnumerable<TReferenceData> _allReferenceDatas;

        public ReferenceDataQuery(DataContext context) : base(context) { }
        
        public IEnumerable<TReferenceData> GetAllCachedForSelectedIds(IList<Guid> referenceDataIds)
        {
            if (referenceDataIds == null || !referenceDataIds.Any()) return Enumerable.Empty<TReferenceData>();

            return GetAllCached().Where(r => referenceDataIds.Contains(r.Id));
        }

        public IEnumerable<TReferenceData> GetAllCached()
        {
            return EnumerableExtensions.LazyInitialiseFor(ref _allReferenceDatas, () => GetAll().ToList());
        }
    }
}