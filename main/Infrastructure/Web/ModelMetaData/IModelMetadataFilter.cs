using System;
using System.Collections.Generic;

namespace UIT.iDeal.Infrastructure.Web.ModelMetaData
{
    public interface IModelMetadataFilter
    {
        void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes);
    }
}