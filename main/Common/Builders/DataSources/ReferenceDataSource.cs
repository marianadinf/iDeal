using System;
using System.Collections.Generic;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Common.Builders.DataSources
{
    public abstract class ReferenceDataSource<TReferenceData> : DatasourceBase<TReferenceData>
        where TReferenceData : ReferenceData, new()
    {
        private Lazy<List<TReferenceData>> _lazyList;
      
        public override TReferenceData Next()
        {
            if (_lazyList == null)
            {
                _lazyList = new Lazy<List<TReferenceData>>(InitialiseList);
            }

            return _lazyList.Value[Generator.Generate()];
        }

        public abstract List<TReferenceData> InitialiseList();

    }
}