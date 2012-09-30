using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace UIT.iDeal.Common.Extensions.Web
{
    public static class HtmlHelpers
    {



        public static MvcHtmlString HeaderColumnFor<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> propertySelector,
                                                            bool populateColumn = true) 
            where TModel : class
        {
            var property = propertySelector.GetPropertyFromLambda();
            return new MvcHtmlString(string.Format("<th data-property-name=\"{0}\" data-property-type=\"{1}\">{2}</th>",
                                    property.Name,
                                    property.PropertyType.Name,
                                    populateColumn ? property.Name.ToWords() : string.Empty));
        }
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
                     string.Format("<label{0}><input type=\"checkbox\" {1} value=\"{2}\"/>{3}</label>",
                                   isFirstLabel ? " class=\"first\"" : string.Empty,
                                   isSelected ? "checked" : string.Empty,
                                   item.Value,
                                   item.Text);
                 
                 isFirstLabel = false;
             }
             var classes = "multiselect";

             if (htmlHelper.ViewData.ModelState.ContainsKey(postListPropertyName))
             {
                 classes += " " + HtmlHelper.ValidationInputCssClassName;
             }

             return
                 new MvcHtmlString(string.Format("<div data-hidden-input-name=\"{0}\" class=\"{1}\" id=\"{2}\">{3}</div>",
                                                 postListPropertyName,
                                                 classes,
                                                 ExpressionHelper.GetExpressionText(readListExpressionSelector),
                                                 innerHtml) );
         }

    }
}