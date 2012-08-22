using System;
using System.Web.Mvc;
using UIT.iDeal.Common.Extensions;

namespace UIT.iDeal.Infrastructure.Web.ActionResults
{
    
    public class AutoMappedViewResult : ViewResult, IAutoMapViewModel
    {
        public Type ViewModelType { get; private set; }

        public AutoMappedViewResult(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            ViewData.Model = BuildViewModel();

            base.ExecuteResult(context);
        }

        public object BuildViewModel()
        {
            return MapperExtensions.Map(ViewData.Model, ViewData.Model.GetType(), ViewModelType);
        }
    }
}
