using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace UIT.iDeal.TestLibrary.Browsers.PageObjects.Framework
{
    public abstract class Page<TViewModel> : Page
        where TViewModel: class
    {
        protected FormComponent<TViewModel> Form { get { return ForForm<TViewModel>(); } }
        public List<IWebElement> FormErrors
        {
            get { return Browser.FindElements(By.ClassName("field-validation-error")).ToList(); }
        }

        protected FormComponent<TForm> ForForm<TForm>()
        {
            return GetCachedUiComponent<FormComponent<TForm>>();
        }

        protected GridComponent<TModel> ForGrid<TModel>(string gridId) where TModel : class , new()
        {
            return GetCachedUiComponent(() => new GridComponent<TModel>(gridId));
        }

    }
}