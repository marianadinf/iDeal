using System;
using System.Collections.Generic;

namespace UIT.iDeal.Common.Errors
{
    public class MessageGroup
    {
        public String PropertyName { get; private set; }

        public List<String> Messages { get; private set; }

        public MessageGroup(List<String> messages = null,String propertyName = null)
        {
            Messages = messages ?? new List<String>();
            PropertyName = propertyName ?? String.Empty;
        }


    }
}