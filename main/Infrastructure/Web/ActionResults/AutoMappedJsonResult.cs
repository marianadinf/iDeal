using System;
using System.Web.Mvc;
using UIT.iDeal.Common.Extensions;

namespace UIT.iDeal.Infrastructure.Web.ActionResults
{
    public class AutoMappedJsonResult : JsonResult, IAutoMapViewModel
    {
        public Type ViewModelType { get; private set; }
        public object Model { get; set; }

        public AutoMappedJsonResult(Type viewmodelType, Object model)
        {
            Model = model;
            ViewModelType = viewmodelType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = BuildViewModel();
            base.ExecuteResult(context);
        }

        public object BuildViewModel()
        {
            return MapperExtensions.Map(Model, Model.GetType(), ViewModelType);
        }
    }
}