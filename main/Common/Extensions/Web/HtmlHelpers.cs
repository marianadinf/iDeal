using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace UIT.iDeal.Common.Extensions.Web
{
    public static class HtmlHelpers
    {

        public static TAttribute GetAttributeFor<TAttribute>(this PropertyInfo property, bool inherit = false)
        {
            return
                property
                    .GetCustomAttributes(typeof (TAttribute), inherit)
                    .OfType<TAttribute>()
                    .FirstOrDefault();
        }

        public static MvcHtmlString HeaderColumnFor<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> propertySelector) 
            where TModel : class
        {
            var property = propertySelector.GetPropertyFromLambda();

            return new MvcHtmlString(string.Format("<th data-property-name=\"{0}\" {2}>{1}</th>",
                                                   property.Name,
                                                   property.GetDisplayedColumnName(),
                                                   model.GetColumnSpanFor(property)));
        }

        static string GetDisplayedColumnName(this PropertyInfo property)
            
        {
            var displayedColumnName = string.Empty;
            var scaffoldColumnAttribute = property.GetAttributeFor<ScaffoldColumnAttribute>();

            if (scaffoldColumnAttribute == null || !scaffoldColumnAttribute.Scaffold)
            {
                displayedColumnName = property.Name.ToWords();

                var displayNameAttribute = property.GetAttributeFor<DisplayNameAttribute>();

                if (displayNameAttribute != null)
                {
                    displayedColumnName = displayNameAttribute.DisplayName;
                }
            }
            return displayedColumnName;
        }

        static string GetColumnSpanFor<TModel>(this TModel model, PropertyInfo property)
        {
            
            var modelProperties = typeof(TModel).GetProperties().ToList();
            var isLastColumIsUsedForScaffolding = false;

            if (modelProperties.IndexOf(property) == modelProperties.Count - 2)
            {
                var attribute = modelProperties.Last().GetAttributeFor<ScaffoldColumnAttribute>();
                isLastColumIsUsedForScaffolding = attribute != null && attribute.Scaffold;
                
            }

            return isLastColumIsUsedForScaffolding ? "colspan=\"2\"" : string.Empty;

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
                 new MvcHtmlString( htmlHelper.LabelFor(readListExpressionSelector) +
                                    string.Format("<div data-hidden-input-name=\"{0}\" class=\"{1}\" id=\"{2}\">{3}</div>",
                                                 postListPropertyName,
                                                 classes,
                                                 ExpressionHelper.GetExpressionText(readListExpressionSelector),
                                                 innerHtml) );
         }

    }
}