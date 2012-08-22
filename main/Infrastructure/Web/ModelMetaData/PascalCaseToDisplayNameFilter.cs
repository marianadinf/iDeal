using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using UIT.iDeal.Common.Extensions;

namespace UIT.iDeal.Infrastructure.Web.ModelMetaData
{
    public class PascalCaseToDisplayNameFilter : IModelMetadataFilter
    {
        public void TransformMetadata(ModelMetadata metadata, IEnumerable<Attribute> attributes)
        {
            if (!string.IsNullOrEmpty(metadata.PropertyName) && !attributes.OfType<DisplayNameAttribute>().Any())
            {
                metadata.DisplayName = metadata.PropertyName.ToProperCaseWords();
            }
        }
    }
}