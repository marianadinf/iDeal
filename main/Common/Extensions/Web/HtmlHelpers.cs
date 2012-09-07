using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace UIT.iDeal.Common.Extensions.Web
{
    public static class HtmlHelpers
    {
         public static MvcHtmlString MultiSelectWithCheckboxes<TModel,TProperty>(this HtmlHelper<TModel> htmlHelper, 
                                                                       Expression<Func<TModel,SelectList>> readListExpressionSelector,
                                                                       Expression<Func<TModel, IEnumerable<TProperty>>> postListExpressionSelector)
         {


             var readList = readListExpressionSelector.Compile().Invoke(htmlHelper.ViewData.Model);
             var postListPropertyName = ExpressionHelper.GetExpressionText(postListExpressionSelector);
             var isFirstLabel = true;

             var postList = postListExpressionSelector.Compile().Invoke(htmlHelper.ViewData.Model);
             var innerHtml = string.Empty;

             foreach (var item in readList)
             {
                 var isSelected = postList != null && postList.Any(x => x.ToString() == item.Value);
                 innerHtml +=
                     string.Format("<label{0}><input type=\"checkbox\" {4} name=\"{1}[]\" value=\"{2}\"/>{3}</label>",
                                   isFirstLabel ? " class=\"first\"" : string.Empty,
                                   postListPropertyName,
                                   item.Value,
                                   item.Text,
                                   isSelected ? "checked" : string.Empty);
                 
                 isFirstLabel = false;
             }

             return
                 new MvcHtmlString(string.Format("<div class=\"{0}\" id=\"{1}\">{2}</div>",
                                                 "multiselect",
                                                 ExpressionHelper.GetExpressionText(readListExpressionSelector),
                                                 innerHtml));
         }

    }
}