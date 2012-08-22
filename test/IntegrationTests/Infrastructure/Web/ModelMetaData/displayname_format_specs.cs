using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Machine.Specifications;
using UIT.iDeal.Infrastructure.Web.ModelMetaData;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.ModelMetaData
{
    internal class DummyClassForHoldingPropertyDisplayNameAttribute
    {
        public string DummyProperty { get; set; }
    }

    public abstract class displayname_format_specs
    {
        protected static PascalCaseToDisplayNameFilter SUT;
        protected static IEnumerable<Attribute> attributes;
        protected static ModelMetadata modelMetadata;
        protected static string expectedDisplayName;

        Establish context = () =>
        {
            SUT = new PascalCaseToDisplayNameFilter();
            modelMetadata = ModelMetadataProviders.Current.GetMetadataForProperty(null,
                typeof (DummyClassForHoldingPropertyDisplayNameAttribute), "DummyProperty");
        };
    }

    public class when_displaying_a_name_without_display_name_attribute : displayname_format_specs
    {
        Establish context = () =>
        {
            expectedDisplayName = "Dummy Property";
            attributes = new List<Attribute> {new HiddenInputAttribute()};
        };
        
        Because of = () => SUT.TransformMetadata(modelMetadata, attributes);

        It should_use_the_correct_default_display_format = () =>
            modelMetadata.DisplayName.ShouldEqual(expectedDisplayName);

    }

    public class when_displaying_a_name_with_display_name_attribute : displayname_format_specs
    {
        Establish context = () =>
        {
            attributes = new List<Attribute>
            {
                new HiddenInputAttribute(),
                new DisplayNameAttribute("A Display Name")
            };
        };

        Because of = () => SUT.TransformMetadata(modelMetadata, attributes);

        It should_not_apply_a_default_display_format_ = () =>
            modelMetadata.DisplayName.ShouldBeNull();
    }
}