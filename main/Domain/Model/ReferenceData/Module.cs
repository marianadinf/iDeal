using System.Collections.Generic;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Domain.Model.ReferenceData
{
    public class Module : ReferenceData
    {
        internal virtual ICollection<ApplicationRole> internalApplicationRoles { get; private set; }
        public virtual IEnumerable<ApplicationRole> ApplicationRoles { get { return internalApplicationRoles; } }

        public virtual void AddApplicationRoles(IEnumerable<ApplicationRole> applicationRoles)
        {
            internalApplicationRoles.AddRange(applicationRoles);
        }
    }
}