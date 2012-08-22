using System;

namespace UIT.iDeal.Common.Errors
{
    public class PropertyNotFoundException : Exception
    {  
        public PropertyNotFoundException(string message) : base(message)
        { }
    }
}
