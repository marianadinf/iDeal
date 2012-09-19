using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UIT.iDeal.Domain.Model.ReferenceData
{
    public abstract class UserRelatedReferenceData : ReferenceData
    {
        internal virtual ICollection<User> internalUsers { get; private set; }

        protected UserRelatedReferenceData()
        {
            internalUsers = new List<User>();
        }

        
    }
}