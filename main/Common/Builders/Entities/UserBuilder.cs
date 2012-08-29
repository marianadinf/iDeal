using System.Linq;
using System.Collections.Generic;

using FizzWare.NBuilder;

using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;
using UIT.iDeal.Common.Builders.DataSources.Base;
using UIT.iDeal.Common.Builders.DataSources.Domain;
using UIT.iDeal.Common.Builders.Entities.Framework;

namespace UIT.iDeal.Common.Builders.Entities
{

    public class UserBuilder : EntityBuilder<User>
    {
        public UserBuilder(int listSize)
            : base(listSize)
        { }

        public UserBuilder()
        { }

        protected override User Build()
        {
            var item =
                _itemTemplate
                    .With(user => user.Firstname, _firstName)
                    .With(user => user.Lastname, _lastName)
                    .With(user => user.Username, _userName)
                    .With(user => user.Email, _email)
                    .Do(user => user.AddApplicationRoles(_applicationRoles))
                    .Do(user => user.AddBusinessUnits(_businessUnits));

            return item.Build();
        }

        protected override List<User> BuildList()
        {
            return _userDataSource.Take(_listSize).ToList();
        }

        protected DatasourceBase<User> _userDataSource = new UserDataSource();
        public UserBuilder WithDataSource(DatasourceBase<User> userDataSource)
        {
            _userDataSource = userDataSource;
            return this;
        }

        protected string _firstName = "Mckinney";
        public UserBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        protected string _lastName = "Fitzgerald";
        public UserBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        protected string _userName = "MFitzgerald";
        public UserBuilder WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }

        protected string _email = "MFitzgerald@host.com";
        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        protected List<ApplicationRole> _applicationRoles = new ReferenceDataBuilderFor<ApplicationRole>();
        public UserBuilder WithApplicationRoles(List<ApplicationRole> applicationRoles)
        {
            _applicationRoles = applicationRoles;
            return this;
        }

        protected List<BusinessUnit> _businessUnits = new ReferenceDataBuilderFor<BusinessUnit>();
        public UserBuilder WithBusinessUnits(List<BusinessUnit> businessUnits)
        {
            _businessUnits = businessUnits;
            return this;
        }
    }
}