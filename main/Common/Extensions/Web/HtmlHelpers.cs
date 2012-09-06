using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace UIT.iDeal.Common.Extensions.Web
{
    public static class HtmlHelpers
    {
         public static MvcHtmlString MultiSelectWithCheckboxes<TModel>(this HtmlHelper<TModel> htmlHelper, 
                                                                       Expression<Func<TModel,SelectList>> readListExpressionSelector,
                                                                       Expression<Func<TModel, Object>> postListExpressionSelector)
         {

             
             var multiSelectDiv = new TagBuilder("div");
             multiSelectDiv.AddCssClass("multiselect");
             multiSelectDiv.MergeAttribute("id", ExpressionHelper.GetExpressionText(readListExpressionSelector));

             var list = readListExpressionSelector.Compile().Invoke(htmlHelper.ViewData.Model);
             var postListPropertyName = ExpressionHelper.GetExpressionText(postListExpressionSelector);
             var isFirstLabel = true;

             foreach (var item in list)
             {
                 multiSelectDiv.InnerHtml +=
                                   string.Format("<label{0}><input type=\"checkbox\" name=\"{1}[]\" value=\"{2}\"/>{3}</label>",
                                                 isFirstLabel? " class=\"first\"" : "",
                                                 postListPropertyName,
                                                 item.Value,
                                                 item.Text);

                 isFirstLabel = false;
             }
             
             return new MvcHtmlString(multiSelectDiv.ToString());
         }

    }
}