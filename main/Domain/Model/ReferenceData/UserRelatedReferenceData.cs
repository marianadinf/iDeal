using System.Collections.Generic;

namespace UIT.iDeal.Domain.Model.ReferenceData
{
    public abstract class UserRelatedReferenceData : ReferenceData
    {
        internal ICollection<User> internalUsers { get; private set; }

        protected UserRelatedReferenceData()
        {
            internalUsers = new List<User>();
        }

        
    }
}