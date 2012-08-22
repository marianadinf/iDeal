using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using OpenQA.Selenium;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.TestLibrary.Browsers.Helpers.Extensions;

namespace UIT.iDeal.TestLibrary.Browsers.PageObjects.Framework
{
    
   public class FormComponent<TForm> : UiComponent
    {

        public bool GetCheckBoxValue<TField>(Expression<Func<TForm, TField>> field)
        {
            var name = ExpressionHelper.GetExpressionText(field);

            return Browser.FindElement(By.Name(name)).Selected;
        }

        public FormComponent<TForm> EnterTextBoxValue<TField>(Expression<Func<TForm, TField>> propertySelector, TField value)
        {
            var name = ExpressionHelper.GetExpressionText(propertySelector);

            Execute(By.Name(name), e => e.ClearAndSendKeys(value.ToString()));

            return this;
        }

        public Boolean IsFocused<TProperty>(Expression<Func<TForm, TProperty>> propertySelector)
        {
            var id = ExpressionHelper.GetExpressionText(propertySelector).TestableId();

            return IsFocused(id);
        }

        public TField GetTextBoxValue<TField>(Expression<Func<TForm, TField>> field)
        {
            var id = ExpressionHelper.GetExpressionText(field).TestableId();

            return Browser
                    .FindElement(By.Id(id))
                    .GetValueFromTextBox<TField>();
        }

        public FormComponent<TForm> SelectItemInDropDown<TField>(Expression<Func<TForm, TField>> field, String valueToBeEntered)
        {
            var name = ExpressionHelper.GetExpressionText(field);
            var id = name.RemoveSuffix().TestableId();

            var selector = "option";
            if (!String.IsNullOrWhiteSpace(valueToBeEntered))
            {
                selector +=  String.Format("[value='{0}']",valueToBeEntered);
            }

            Execute(By.Id(id), e => e.FindElement(By.CssSelector(selector)).Click());

            return this;
        }

        public bool HasDropDownFor(Expression<Func<TForm, SelectList>> dropDownSelector)
        {
            var name = ExpressionHelper.GetExpressionText(dropDownSelector);
            var id = name.RemoveSuffix().TestableId();

            return Browser.HasElement(By.Id(id));
        }

        public Boolean HasNoSelectedValueForDropDown(Expression<Func<TForm, SelectList>> dropDownSelector)
        {
            var name = ExpressionHelper.GetExpressionText(dropDownSelector);
            var selectId = name.RemoveSuffix().TestableId();

            var selectElement = Browser.FindElement(By.Id(selectId));
            
            
            var hasSelectedOptions =  selectElement.FindElements(By.CssSelector("option[selected]")).Any();

            return !hasSelectedOptions;

        }

        public Boolean FirstItemForDropDownIs(Expression<Func<TForm, SelectList>> dropDownSelector, string itemText)
        {
            var name = ExpressionHelper.GetExpressionText(dropDownSelector);
            var id = name.RemoveSuffix().TestableId();

            var select = Browser.FindElement(By.Id(id));

            return select.FindElement(By.CssSelector("option")).Text == itemText;
        }

        public Boolean HasDefaultItemSelectedForDropDown(Expression<Func<TForm, SelectList>> dropDownSelector, string defaultTextItem = "Select")
        {
            return
                HasNoSelectedValueForDropDown(dropDownSelector) &&
                FirstItemForDropDownIs(dropDownSelector, defaultTextItem);
        }

        public String SelectedItemForDropDown(Expression<Func<TForm, SelectList>> dropDownSelector)
        {
            var name = ExpressionHelper.GetExpressionText(dropDownSelector);
            var id = name.RemoveSuffix().TestableId();

            var select = Browser.FindElement(By.Id(id));

            return select.FindElement(By.CssSelector("option[selected]")).Text;
        }
    }
}