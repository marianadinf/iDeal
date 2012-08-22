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
        public virtual IEnumerable<ApplicationRole> ApplicationRoles { get; protected set; }
        public virtual IEnumerable<BusinessUnit> BusinessUnits { get; protected set; }

        public static User Create(string firstname, string lastname,
                        string username,string email, IEnumerable<ApplicationRole> applicationRoles, IEnumerable<BusinessUnit> businessUnits  )
        {
            //Guard.That(applicationRoles).IsNotEmpty();
            Guard.That(username).IsNotEmpty();
            return new User
                       {
                           Firstname = firstname,
                           Lastname = lastname,
                           Username = username,
                           Email = email,
                           ApplicationRoles = applicationRoles ,
                           BusinessUnits = businessUnits 
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