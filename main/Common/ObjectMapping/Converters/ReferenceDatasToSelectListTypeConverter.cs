using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using UIT.iDeal.Common.Extensions.Web;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.ObjectMapping.Converters
{
    public class ReferenceDatasToSelectListTypeConverter<TReferenceData> : TypeConverter<IEnumerable<TReferenceData>, SelectList>
        where TReferenceData : ReferenceData
    {
        protected override SelectList ConvertCore(IEnumerable<TReferenceData> source)
        {
            if (source == null) return new SelectList(Enumerable.Empty<SelectListItem>());

            return source.ToSelectList();
        }
    }
}