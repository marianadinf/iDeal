using System.Collections.Generic;
using UIT.iDeal.Domain.Model.Base;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Domain.Model
{
    public class User : AggregateRoot
    {
        public virtual string Firstname { get; protected set; }
        public virtual string Lastname { get; protected set; }
        public virtual string Username { get; protected set; }
        public virtual string Email { get; protected set; }

        #region Application Roles

        internal ICollection<ApplicationRole> internalApplicationRoles { get; private set; }
        public virtual IEnumerable<ApplicationRole> ApplicationRoles { get { return internalApplicationRoles; } }
        
        public virtual void AddApplicationRoles(IEnumerable<ApplicationRole> applicationRoles)
        {
            internalApplicationRoles.AddRange(applicationRoles);
        }

        #endregion

        #region Business Units

        internal ICollection<BusinessUnit> internalBusinessUnits { get; private set; }
        public virtual IEnumerable<BusinessUnit> BusinessUnits { get { return internalBusinessUnits; } }

        public virtual void AddBusinessUnits(IEnumerable<BusinessUnit> businessUnits)
        {
            internalBusinessUnits.AddRange(businessUnits);
        }

        #endregion

        #region Contructors

        internal static User Create(string firstname,
                                  string lastname,
                                  string username,
                                  string email)
        {
            return new User
            {
                Firstname = firstname,
                Lastname = lastname,
                Username = username,
                Email = email
            };

        }

        public User()
        {
            internalApplicationRoles = new HashSet<ApplicationRole>();
            internalBusinessUnits = new HashSet<BusinessUnit>();
        }
       
        #endregion
    }
}