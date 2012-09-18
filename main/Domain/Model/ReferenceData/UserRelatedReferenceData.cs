using System.Collections.Generic;

namespace UIT.iDeal.Domain.Model.ReferenceData
{
    public abstract class UserRelatedReferenceData : ReferenceData
    {
        protected internal virtual ICollection<User> _users { get; set; }

        protected internal UserRelatedReferenceData()
        {
            _users = new List<User>();
        }
    }
}