using System.Collections.Generic;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Domain.Model.ReferenceData
{
    public class Module : ReferenceData
    {
        #region Application Roles
        
        internal virtual ICollection<ApplicationRole> internalApplicationRoles { get; private set; }
        public virtual IEnumerable<ApplicationRole> ApplicationRoles { get { return internalApplicationRoles; } }

        #endregion

        #region Constructor

        public Module()
        {
            internalApplicationRoles = new List<ApplicationRole>();
        }

        internal static Module Create(string code, string description, IEnumerable<ApplicationRole> applicationRoles = null )
        {
            var newModule = new Module
                                {
                                    Code = code,
                                    Description = description
                                };

            newModule.AddApplicationRoles(applicationRoles);
            return newModule;
        }

        internal static Module Create(string description)
        {
            return Create(null,description);
        }


        #endregion

        

        public virtual void AddApplicationRoles(IEnumerable<ApplicationRole> applicationRoles)
        {
            internalApplicationRoles.AddRange(applicationRoles);
        }
    }
}