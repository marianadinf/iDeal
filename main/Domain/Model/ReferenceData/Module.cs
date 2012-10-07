using System.Collections.Generic;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Domain.Model.ReferenceData
{
    public class Module : ReferenceData 
    {
        internal ICollection<User> internalUsers { get; private set; }

       
        #region Application Roles
        
        internal virtual ICollection<ApplicationRole> internalApplicationRoles { get; set; }
        public virtual IEnumerable<ApplicationRole> ApplicationRoles { get { return internalApplicationRoles; } }

        #endregion

        #region Constructor

        public Module()
        {
            internalApplicationRoles = new HashSet<ApplicationRole>();
            internalUsers = new List<User>();
        }

        internal static Module Create(string code, string description)
        {
            return new Module
            {
                Code = code,
                Description = description
            };

        }

        #endregion

        public virtual void AddApplicationRoles(IEnumerable<ApplicationRole> applicationRoles)
        {
            internalApplicationRoles.AddRange(applicationRoles);
        }
    }
}