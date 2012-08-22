using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UIT.iDeal.Common.Errors;

namespace UIT.iDeal.Common.Extensions
{
    public static class ModelStateExtensions
    {
       
        public static List<string> GetAllErrorMessages(this ModelStateDictionary modelState)
        {
            return
                modelState
                    .Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();
        }

        public static void MergeWith(this ModelStateDictionary modelStateDictionary, ExecutionResult executionResult)
        {
            if (executionResult == null) return;

            executionResult
                .Errors
                .Select(error => error.Value)
                .Each(msgGroup => msgGroup
                                      .Messages
                                      .Each(msg => modelStateDictionary.AddModelError(msgGroup.PropertyName, msg)));

        }
    }
}