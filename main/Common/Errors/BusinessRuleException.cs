using System;

namespace UIT.iDeal.Common.Errors
{
    public class BusinessRuleException : ApplicationException
    {
        public string PropertyName { get; private set; }


        public BusinessRuleException(string propertyName, string error)
            : base(error)
        {
            PropertyName = propertyName;
        }

        public BusinessRuleException()
        {
        }

        public BusinessRuleException(string error)
            : base(error)
        {
        }
    }
}