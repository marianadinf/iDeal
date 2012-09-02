using System.Collections.Generic;
using Machine.Specifications;
using UIT.iDeal.Common.Builders.DataSources.ReferenceData;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.UnitTests.Infrastructure.Builders
{
    public class when_building_reference_data_with_an_existing_datasource 
    {
        static List<BusinessUnit> _businessUnits;
        static readonly IEnumerable<BusinessUnit> _expectedBusinessUnits = new BusinessUnitReferenceDataSource();

        Because of = () =>
            _businessUnits = new ReferenceDataBuilderFor<BusinessUnit>();

        It should_only_contain_reference_data_specified_in_ReferenceDataSource = () =>
            _businessUnits.ShouldEqual(_businessUnits);

    }

    public class AnotherReferenceData : ReferenceData { }
}
