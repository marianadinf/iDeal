using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        
        private IList<ApplicationRole>  _applicationRoles = new List<ApplicationRole>();
        public virtual IEnumerable<ApplicationRole> ApplicationRoles { get { return _applicationRoles; } }
        
        public virtual void AddApplicationRoles(IEnumerable<ApplicationRole> applicationRoles)
        {
            _applicationRoles.AddRange(applicationRoles);
        }

        #endregion

        #region Business Units

        private IList<BusinessUnit>  _businessUnits = new List<BusinessUnit>();
        public virtual IEnumerable<BusinessUnit> BusinessUnits { get { return _businessUnits; } }

        public virtual void AddBusinessUnits(IEnumerable<BusinessUnit> businessUnits)
        {
            _businessUnits.AddRange(businessUnits);
        }

        #endregion

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

        public virtual void Edit(string firstname , string lastname, string username)
        {
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
        }
    }
}