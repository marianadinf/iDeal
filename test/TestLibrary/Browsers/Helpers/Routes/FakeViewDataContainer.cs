using System.Web.Mvc;

namespace UIT.iDeal.TestLibrary.Browsers.Helpers.Routes
{
    internal class FakeViewDataContainer : IViewDataContainer
    {
        private ViewDataDictionary _viewData = new ViewDataDictionary();
        public ViewDataDictionary ViewData { get { return _viewData; } set { _viewData = value; } }
    }
}
