using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;

using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Builders.DataSources.Base;
using UIT.iDeal.Common.Builders.DataSources.ReferenceData;
using UIT.iDeal.Common.Builders.Entities.Framework;

using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.Builders.Entities
{
    
    public class ReferenceDataBuilderFor<TReferenceData> : EntityBuilder<TReferenceData> 
        where TReferenceData : ReferenceData, new()
    {
        public ReferenceDataBuilderFor(): base(_numberOfReferenceData)
        { }

        protected override List<TReferenceData> BuildList()
        {
            VerifyThatReferenceDataSourceHasBeenSet();

            var result =
                _listTemplate
                    .All()
                    .Do(referenceData => CopyValues(from: _referenceDataSource.Next(), to: referenceData))
                    .Build()
                    .ToList();

            return result;
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

        private void CopyValues(TReferenceData from, TReferenceData to)
        {

            var referenceDataType = typeof(TReferenceData);

            referenceDataType
                .GetProperties()
                .Where(p => p.Name != "Id")
                .Each(p => p.SetValue(to, referenceDataType.GetProperty(p.Name).GetValue(from, null), null));

        }

        private static int _numberOfReferenceData = 3;
        private static ReferenceDataSource<TReferenceData> GetInstanceOfReferenceDataSource()
        {
            var referenceDataSourceBaseType = typeof(ReferenceDataSource<>).MakeGenericType(typeof(TReferenceData));

            var referenceDataSourceType =
                AssemblyScanner
                    .GetMatchingTypesFromAssemblyContaining<SequentialListGenerator>(t => t.IsConcreteTypeOf(referenceDataSourceBaseType))
                    .FirstOrDefault();

            ReferenceDataSource<TReferenceData> result = null;

            if (referenceDataSourceType != null)
            {
                result =  (ReferenceDataSource<TReferenceData>)Activator.CreateInstance(referenceDataSourceType);
         
                _numberOfReferenceData = result.Count();
            }

            return result;
        }
    }
}