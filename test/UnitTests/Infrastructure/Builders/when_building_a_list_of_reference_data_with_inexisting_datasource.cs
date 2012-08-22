using System;
using System.Collections.Generic;
using Machine.Specifications;
using UIT.iDeal.Common.Builders.Entities;

namespace UIT.iDeal.UnitTests.Infrastructure.Builders
{
    public class when_building_a_list_of_reference_data_with_inexisting_datasource 
    {
        static List<AnotherReferenceData> _referenceDataList;
        static ArgumentNullException _argumentNullException;


        Because of = () =>
            _argumentNullException =
                Catch.Exception(() => _referenceDataList = new ReferenceDataBuilderFor<AnotherReferenceData>()) as
                    ArgumentNullException;

        It should_throw_an_ArgumentNullException = () =>
            _argumentNullException.ShouldNotBeNull();

        It the_exception_error_message_is_You_must_provide_a_Reference_data_source_of_AnotherReferenceData = () =>
            _argumentNullException.Message.ShouldEqual(
                "You must provide a Reference data source of AnotherReferenceData");




    }
}