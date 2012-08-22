using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FizzWare.NBuilder.Dates;
using Machine.Specifications;
using UIT.iDeal.Infrastructure.Web.ModelMetaData;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.ModelMetaData
{
    public abstract class datetime_display_format_specs
    {
        protected static DateTimeFormatFilter SUT;
        protected static IEnumerable<Attribute> attributes;
        protected static ModelMetadata modelMetadata;
        protected static DateTime dateTimeToFormat;

        Establish context = () =>
        {
            SUT = new DateTimeFormatFilter();
            dateTimeToFormat = The.Year(2011).On.December.The20th;
            modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof (DateTime));
        };

        protected static string FormatedDateTime(string displayFormatString)
        {
            return string.Format(displayFormatString, dateTimeToFormat);
        }
    }
    
    public class when_displaying_a_datetime_without_display_format_attribute : datetime_display_format_specs
    {
        Establish context = () =>
            attributes = new List<Attribute> {new HiddenInputAttribute()};
        
        Because of = () => SUT.TransformMetadata(modelMetadata, attributes);

        It should_use_the_correct_default_display_format = () =>
            FormatedDateTime(modelMetadata.DisplayFormatString)
                .ShouldEqual("Tue 20 Dec 2011");

    }

    public class when_displaying_a_datetime_with_display_format_attribute : datetime_display_format_specs
    {
        Establish context = () =>
            attributes = new List<Attribute>
            {
                new HiddenInputAttribute(),
                new DisplayFormatAttribute {DataFormatString = "{0:dd/MM/yy}",}
            };

        Because of = () => SUT.TransformMetadata(modelMetadata, attributes);

        It should_not_apply_a_default_display_format_ = () =>
            modelMetadata.DisplayFormatString.ShouldBeNull();
    }
}