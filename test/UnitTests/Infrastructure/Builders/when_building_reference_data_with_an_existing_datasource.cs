using System.Collections.Generic;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using Machine.Specifications;
using UIT.iDeal.Common.Builders.DataSources.ReferenceData;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Common.Data;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.UnitTests.Infrastructure.Builders
{
    public class when_building_reference_data_with_an_existing_datasource 
    {
        static List<BusinessUnit> _businessUnits;
        static readonly IEnumerable<BusinessUnit> _expectedBusinessUnits = new BusinessUnitReferenceDataSource();

        Establish context = () =>
        {
            BuilderSetup.SetDefaultPropertyNamer(new SequentialPropertyNamer(new ReflectionUtil()));
            _expectedBusinessUnits.Each(businessUnit => businessUnit.SetValue(x => x.Id, GuidComb.Generate()));
        };

        Because of = () =>
            _businessUnits = new ReferenceDataBuilderFor<BusinessUnit>();

        It should_only_contain_reference_data_specified_in_ReferenceDataSource = () =>
            _businessUnits.ShouldContainOnly(_expectedBusinessUnits);

    }

    public class AnotherReferenceData : ReferenceData { }
}
