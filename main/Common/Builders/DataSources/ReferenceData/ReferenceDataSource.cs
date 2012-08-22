using System;
using System.Collections;
using System.Collections.Generic;
using UIT.iDeal.Common.Builders.DataSources.Base;

namespace UIT.iDeal.Common.Builders.DataSources.ReferenceData
{
    public abstract class ReferenceDataSource<TReferenceData> : DatasourceBase<TReferenceData>, IEnumerable<TReferenceData> 
        where TReferenceData : Domain.Model.ReferenceData.ReferenceData, new()
    {
        private readonly Lazy<List<TReferenceData>> _lazyList;

        protected ReferenceDataSource()
        {
            _lazyList = new Lazy<List<TReferenceData>>(InitialiseList);
        }
      
        public override TReferenceData Next()
        {
            return _lazyList.Value[Generator.Generate()];
        }

        protected abstract List<TReferenceData> InitialiseList();

        public IEnumerator<TReferenceData> GetEnumerator()
        {
            return _lazyList.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}