

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using UIT.iDeal.Common.Errors;

namespace UIT.iDeal.Common.Extensions.Web
{
    public static class HtmlHelpers
    {
        
        

        public static MvcHtmlString HeaderColumnFor<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> propertySelector) 
            where TModel : class
        {
            var property = propertySelector.GetPropertyFromLambda();

            return new MvcHtmlString(string.Format("<th data-property-name=\"{0}\" {2}>{1}</th>",
                                                   property.Name,
                                                   property.GetDisplayedColumnName(),
                                                   model.GetColumnSpanFor(property)));
        }

        static bool IsScaffoldColumn(this PropertyInfo property)
        {
            return property.SatisfyForAttribute<ScaffoldColumnAttribute>(x => x.Scaffold);
        }

        static string GetDisplayedColumnName(this PropertyInfo property)
            
        {
            var displayedColumnName = string.Empty;

            if (!property.IsScaffoldColumn())
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

        static string GetColumnSpanFor(this Object model, PropertyInfo property)
        {

            var modelProperties = model.GetType().GetProperties().ToList();
            
            var currentPropertyIndexPosition = modelProperties.IndexOf(property);
            
            var isAllFollowingColumnsAreUsedForScaffolding =
                modelProperties
                    .Where(p => modelProperties.IndexOf(p) > currentPropertyIndexPosition)
                    .All(p => p.IsScaffoldColumn());

            var numberOfSpannedColumns = modelProperties.Count - currentPropertyIndexPosition;

            return isAllFollowingColumnsAreUsedForScaffolding
                ? string.Format("colspan=\"{0}\"", numberOfSpannedColumns)
                : string.Empty;

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

        /// <summary>
        /// Generates Application Messages
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="applicationMessage"> </param>
        /// <returns>HTML for Application Message</returns>
        public static string ApplicationMessage(this HtmlHelper htmlHelper, ExecutionResult applicationMessage, string classes = "")
        {
            var sb = new StringBuilder();
            if (applicationMessage != null)
            {
                var categories =
                    EnumExtensions
                        .GetEnumItems<MessageCategory>()
                        .Where(x => x != MessageCategory.FatalException ||
                                    x != MessageCategory.AuthorisationException);

                foreach (var category in categories)
                {
                    var group = (category == MessageCategory.BrokenBusinessRule) ?
                        applicationMessage.ErrorsMessageGroup : applicationMessage.GetMessageGroup(category);
                    var noItems = !group.Messages.Any();

                    var container = new TagBuilder("div");
                    var head = new TagBuilder("div");
                    var content = new TagBuilder("div");
                    var button = new TagBuilder("div");

                    container.AddCssClass("infobox");
                    if (noItems) container.AddCssClass("ajax-infobox");
                    if (classes != string.Empty)
                    {
                        container.AddCssClass(classes);
                    }
                    container.Attributes.Add("style", "display:none;");
                    container.Attributes.Add("id", category.ToDescription());
                    container.AddCssClass(string.Format("bghead_{0}", category.ToDescription()));
                    container.AddCssClass(string.Format("bgcnt_{0}", category.ToDescription()));
                    head.AddCssClass("head");
                    head.InnerHtml = group.PropertyName;
                    if (category == MessageCategory.Success)
                    {
                        var closebtn = new TagBuilder("div");
                        closebtn.AddCssClass("btn_close");
                        closebtn.MergeAttribute("title", "close");
                        head.InnerHtml += closebtn.ToString();
                    }
                    content.AddCssClass("content");
                    button.AddCssClass("button");
                    button.AddCssClass(string.Format("btn_{0}", category.ToDescription()));

                    var itemSB = new StringBuilder("<ul>");
                    foreach (var message in group.Messages)
                    {
                        itemSB.Append(string.Format("<li>{0}</li>", message));
                    }
                    itemSB.Append("</ul>");
                    content.InnerHtml = itemSB.ToString();
                    container.InnerHtml = string.Format("{0}\n{1}\n{2}\n", head.ToString(), content.ToString(), button.ToString());
                    sb.Append(container.ToString());
                }
                
            }
            return sb.ToString();
        }

    }
}