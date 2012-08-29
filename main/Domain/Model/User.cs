using System.Collections;
using System.Collections.Generic;
using Seterlund.CodeGuard;

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

        protected ICollection<ApplicationRole> _applicationRoles { get; set; }
        public virtual IEnumerable<ApplicationRole> ApplicationRoles { get { return _applicationRoles; } }

        public virtual void AddApplicationRoles(IEnumerable<ApplicationRole> applicationRoles)
        {
            _applicationRoles.AddRange(applicationRoles);
        }

        #endregion

        #region Business Units

        protected ICollection<BusinessUnit> _businessUnits { get; set; }
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