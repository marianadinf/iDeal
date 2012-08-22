using System;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.TestLibrary.Browsers.Locators;
using By = UIT.iDeal.TestLibrary.Browsers.Locators.By;

namespace UIT.iDeal.TestLibrary.Browsers.PageObjects.Framework
{
    public class PanelComponent<TViewModel> : UiComponent
        where TViewModel:class ,new ()
    {
        readonly string _panelId;
        TViewModel _panel;

        public PanelComponent(string panelId)
        {
            _panelId = panelId;
        }

        public TViewModel Panel
        {
            get
            {
                if (_panel != null)
                {
                    return _panel;
                }

                _panel = new TViewModel();

                var readOnlyElements = Browser.FindElements(By.jQuery(String.Format("#{0} span", _panelId)));

                var modelType = typeof (TViewModel);

                foreach (var element in readOnlyElements)
                {
                    var propertyName = element.GetAttribute("id");
                    var property = modelType.GetProperty(propertyName);
                    var text = element.Text;

                    if (CanWriteProperty(text, property))
                    {
                        var value = text.ChangeType(property.PropertyType);

                        property.SetValue(_panel, value, null);
                    }
                }

                return _panel;
            }
        }
        
       
       

        
     
    }

}