using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Interfaces;

namespace UIT.iDeal.Common.Extensions
{
    /// <summary>
    /// Extension utilities on application message
    /// </summary>
    public static class ExecutionResultExtension
    {

        /// <summary>
        /// Merge all error found in a validation dictionary to the current application message
        /// </summary>
        /// <param name="executionResult">instance of IApplicationMessage</param>
        /// <param name="validationDictionary">instance of IValidationDictionary</param>
        /// <param name="againstMessageCategory">add error message into this category</param>
        /// <returns>instance of application message in order to chain to other operations</returns>
        public static ExecutionResult Merge(this ExecutionResult executionResult, IValidationDictionary validationDictionary, MessageCategory againstMessageCategory = MessageCategory.BrokenBusinessRule)
        {
            if (executionResult == null) return null;

            if (validationDictionary != null)
            {

                validationDictionary
                    .Each(item => executionResult.Add(againstMessageCategory, new MessageGroup(item.Value.ToList())));
            }

            return executionResult;
        }

        /// <summary>
        /// Merge all error found in an application message into the current validation dictionary
        /// </summary>
        /// <param name="validationDictionary">instance of IValidationDictionary</param>
        /// <param name="executionResult">instance of IApplicationMessage</param>
        /// <param name="fromMessageCategory">look for error messages from this category</param>
        /// <returns></returns>
        public static IValidationDictionary Merge(this IValidationDictionary validationDictionary, ExecutionResult executionResult, MessageCategory fromMessageCategory = MessageCategory.BrokenBusinessRule)
        {

            if ((validationDictionary != null) && (executionResult != null) && !executionResult.IsSuccessFull)
            {
                var propertyName = executionResult[fromMessageCategory].PropertyName;
                executionResult[fromMessageCategory]
                    .Messages
                    .Each(errorMessage => validationDictionary.AddError(propertyName, errorMessage));
            }

            return validationDictionary;
        }

        public static ExecutionResult Merge(this ExecutionResult executionResult, IEnumerable<ValidationFailure> validationFailures, MessageCategory withMessageCategory = MessageCategory.BrokenBusinessRule)
        {
            if (executionResult != null)
            {
                validationFailures.Each(
                    x => executionResult.Add(MessageCategory.BrokenBusinessRule, x.ErrorMessage, x.PropertyName));
            }
            return executionResult;
        }
        
        /// <summary>
        /// Clone a message group to a brand new
        /// </summary>
        /// <param name="messageGroup"></param>
        /// <returns></returns>
        public static MessageGroup Clone(this MessageGroup messageGroup)
        {
            return new MessageGroup(messageGroup.Messages, messageGroup.PropertyName);
        }

        public static String ToTitle(this MessageCategory messageCategory)
        {
            return messageCategory.ToDescription().ToWords();
        }
    }
}