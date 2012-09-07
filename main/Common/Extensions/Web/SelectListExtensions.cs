using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Seterlund.CodeGuard;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.Extensions.Web
{
    public static class SelectListExtensions
    {

        public static SelectList ToSelectList(this IEnumerable<KeyValuePair<int, string>> items, Object withSelectedValue = null)
        {
            return items.ToSelectList(x => x.Key, x => x.Value, selectedValue: withSelectedValue);
        }

        public static SelectList ToSelectList<TReferenceData>(this IEnumerable<TReferenceData> referenceDatas )
            where TReferenceData : ReferenceData
        {
            return referenceDatas.ToSelectList(x => x.Id, x => x.Description, topAdditionalItemText: null);
        }


        public static SelectList ToSelectList<T>(this IEnumerable<T> items,
                                                 Func<T, Object> valuePropertySelector,
                                                 Func<T, Object> textPropertySelector = null,
                                                 Object topAdditionalItemValue = null,
                                                 String topAdditionalItemText = "Select",
                                                 Object selectedValue = null)
        {
            if (items == null)
            {
                return new SelectList(new T[] { });
            }

            Guard.That(valuePropertySelector).IsNotNull();

            if (textPropertySelector == null)
            {
                textPropertySelector = valuePropertySelector;
            }

            var selectItems =
                items
                    .Select(i => new SelectListItem
                    {
                        Value = valuePropertySelector(i).ToString(),
                        Text = textPropertySelector(i).ToString()
                    })
                    .ToList();

            if (!String.IsNullOrEmpty(topAdditionalItemText))
            {
                selectItems.Insert(0, new SelectListItem
                {
                    Text = topAdditionalItemText,
                    Value = (topAdditionalItemValue ?? String.Empty).ToString()
                });
            }

            return new SelectList(selectItems, "Value", "Text", selectedValue);
        }




    }
}