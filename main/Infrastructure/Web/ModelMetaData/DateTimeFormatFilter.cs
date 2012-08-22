using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace UIT.iDeal.Infrastructure.Web.ModelMetaData
{
    public class DateTimeFormatFilter : IModelMetadataFilter
    {
        public void TransformMetadata(ModelMetadata metadata, IEnumerable<Attribute> attributes)
        {
            if (metadata.ModelType == typeof(DateTime) && !attributes.OfType<DisplayFormatAttribute>().Any())
            {
                metadata.DisplayFormatString = "{0:ddd dd MMM yyyy}";
            }
        }
    }
}