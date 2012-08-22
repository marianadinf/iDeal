using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FizzWare.NBuilder.Dates;
using Machine.Specifications;
using UIT.iDeal.Infrastructure.Web.ModelMetaData;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.ModelMetaData
{
    public abstract class timespan_display_format_specs
    {
        protected static TimeSpanFormatFilter SUT;
        protected static IEnumerable<Attribute> attributes;
        protected static ModelMetadata modelMetadata;
        protected static TimeSpan timeSpanToFormat;

        Establish context = () =>
        {
            SUT = new TimeSpanFormatFilter();
            timeSpanToFormat = The.Time(12, 30);
            modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof (TimeSpan));
        };

        protected static string FormatedTimeSpan(string displayFormatString)
        {
            return string.Format(displayFormatString, timeSpanToFormat);
        }
    }

    public class when_displaying_a_timespan_without_display_format_attribute : timespan_display_format_specs
    {
        Establish context = () =>
            attributes = new List<Attribute> {new HiddenInputAttribute()};
        
        Because of = () => SUT.TransformMetadata(modelMetadata, attributes);

        It should_use_the_correct_default_display_format = () =>
            FormatedTimeSpan(modelMetadata.DisplayFormatString).ShouldEqual("12:30");

    }

    public class when_displaying_a_timepsan_with_display_format_attribute : timespan_display_format_specs
    {
        Establish context = () =>
            attributes = new List<Attribute>
            {
                new HiddenInputAttribute(),
                new DisplayFormatAttribute {DataFormatString = "{0:hh}",}
            };

        Because of = () => SUT.TransformMetadata(modelMetadata, attributes);

        It should_not_apply_a_default_display_format_ = () =>
            modelMetadata.DisplayFormatString.ShouldBeNull();
    }
}