using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using UIT.iDeal.Common.Builders.Base;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Builders.DataSources.Base;
using UIT.iDeal.Common.Builders.DataSources.ReferenceData;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.Builders.Entities
{

    public class ReferenceDataBuilderFor<TReferenceData> : EntityBuilder<TReferenceData>
        where TReferenceData : ReferenceData, new()
    {
        public ReferenceDataBuilderFor()
            : this(_numberOfReferenceData)
        { }


        public ReferenceDataBuilderFor(int listSize)
            : base(listSize)
        { }

        protected override List<TReferenceData> BuildList()
        {
            VerifyThatReferenceDataSourceHasBeenSet();

            return _referenceDataSource.Take(_numberOfReferenceData).ToList();
        }

        private ReferenceDataSource<TReferenceData> _referenceDataSource = GetInstanceOfReferenceDataSource();
        public ReferenceDataBuilderFor<TReferenceData> WithReferenceDataSource(ReferenceDataSource<TReferenceData> referenceDataSource)
        {
            _referenceDataSource = referenceDataSource;
            return this;
        }

        

        private void VerifyThatReferenceDataSourceHasBeenSet()
        {
            if (_referenceDataSource == null)
            {
                throw new ArgumentNullException(String.Format("You must provide a Reference data source of {0}",
                                                              typeof (TReferenceData).Name), null as Exception);
            }
        }

        private static int _numberOfReferenceData = 3;
        public static ReferenceDataSource<TReferenceData> GetInstanceOfReferenceDataSource()
        {
            var referenceDataSourceBaseType = typeof(ReferenceDataSource<>).MakeGenericType(typeof(TReferenceData));

            var referenceDataSourceType =
                AssemblyScanner
                    .GetMatchingTypesFromAssemblyContaining<SequentialListGenerator>(t => t.IsConcreteTypeOf(referenceDataSourceBaseType))
                    .FirstOrDefault();

            ReferenceDataSource<TReferenceData> result = null;

            if (referenceDataSourceType != null)
            {
                result = (ReferenceDataSource<TReferenceData>)Activator.CreateInstance(referenceDataSourceType);

                _numberOfReferenceData = result.Count();
            }

            return result;
        }
    }
}