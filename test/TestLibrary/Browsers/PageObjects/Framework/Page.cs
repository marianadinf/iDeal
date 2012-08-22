using System;
using System.Collections.Generic;

namespace UIT.iDeal.TestLibrary.Browsers.PageObjects.Framework
{
    public abstract class Page : UiComponent
    {
        readonly Dictionary<Type, UiComponent> _uIcomponents = new Dictionary<Type, UiComponent>();

        public abstract string PageId { get; }
        public abstract string PageTitle { get; }

        protected TComponent GetCachedUiComponent<TComponent>()
            where TComponent : UiComponent, new()
        {
            return GetCachedUiComponent(() => new TComponent {Browser = Browser});
        }

        protected TComponent GetCachedUiComponent<TComponent>(Func<TComponent> uiComponentCreator)
            where TComponent : UiComponent
        {

            var componentType = typeof (TComponent);


            if (!_uIcomponents.ContainsKey(componentType))
            {
                var uiComponent = uiComponentCreator();
                if (uiComponent.Browser == null)
                {
                    uiComponent.Browser = Browser;
                }
                _uIcomponents[componentType] = uiComponent;
            }
            return (TComponent) _uIcomponents[componentType];

        }
    }
}