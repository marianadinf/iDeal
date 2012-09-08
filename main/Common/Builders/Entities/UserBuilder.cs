using System.Linq;
using System.Collections.Generic;

using FizzWare.NBuilder;
using UIT.iDeal.Common.Builders.Base;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;
using UIT.iDeal.Common.Builders.DataSources.Base;
using UIT.iDeal.Common.Builders.DataSources.Domain;

namespace UIT.iDeal.Common.Builders.Entities
{

    public class UserBuilder : EntityBuilder<User>
    {
        #region members

        IEnumerable<User> _userDataSource;
        private IEnumerable<User> UserDataSource
        {
            get
            {
                return EnumerableExtensions.LazyInitialiseFor(ref _userDataSource,
                                                              () => new UserDataSource().Take(_listSize));
            }
        }

        private IEnumerable<string> _firstNames;
        private IEnumerable<string>  FirstNames
        {
            get
            {
                return EnumerableExtensions.LazyInitialiseFor(ref _firstNames,
                                                              () => UserDataSource.Select(x => x.Firstname).Take(_listSize));
            }
        }

        private IEnumerable<string> _lastNames;
        private IEnumerable<string> LastNames
        {
            get
            {
                return EnumerableExtensions.LazyInitialiseFor(ref _lastNames,
                                                              () => UserDataSource.Select(x => x.Lastname).Take(_listSize));
            }
        }

        private IEnumerable<string> _userNames;
        private IEnumerable<string> UserNames
        {
            get
            {
                return EnumerableExtensions.LazyInitialiseFor(ref _userNames,
                                                              () => UserDataSource.Select(x => x.Username).Take(_listSize));
            }
        }

        private IEnumerable<string> _emails;
        private IEnumerable<string> Emails
        {
            get { return EnumerableExtensions.LazyInitialiseFor(ref _emails, 
                                                                () => UserDataSource.Select(x => x.Email).Take(_listSize)); }
        }
       

        #endregion

        public UserBuilder(int listSize)
            : base(listSize)
        { }

        public UserBuilder()
        { }

        protected override User Build()
        {
            var item =
                _itemTemplate
                    .With(user => user.Firstname, FirstNames.First())
                    .With(user => user.Lastname, LastNames.First())
                    .With(user => user.Username, UserNames.First())
                    .With(user => user.Email, Emails.First())
                    .Do(user => user.AddApplicationRoles(_applicationRoles))
                    .Do(user => user.AddBusinessUnits(_businessUnits));

            return item.Build();
        }

        protected override List<User> BuildList()
        {
            var index = 0;
            return
                _listTemplate
                    .All()
                    .Do(user => PopulateUser(user,index++))
                    .Build()
                    .ToList();
        }

        
        public UserBuilder WithDataSource(IEnumerable<User> userDataSource)
        {
            _userDataSource = userDataSource;
            return this;
        }
        
        public UserBuilder WithFirstNames(params string[] firstNames)
        {
            _firstNames = firstNames;
            return this;
        }

        
        public UserBuilder WithLastNames(params string[] lastNames)
        {
            _lastNames = lastNames;
            return this;
        }
        
        public UserBuilder WithUserNames(params string[] userNames)
        {
            _userNames = userNames;
            return this;
        }
        
        public UserBuilder WithEmails(params string[] emails)
        {
            _emails = emails;
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

        private void PopulateUser(User destinationUser, int index)
        {
            var fromUser = User.Create(FirstNames.ElementAt(index),
                                       LastNames.ElementAt(index),
                                       UserNames.ElementAt(index),
                                       Emails.ElementAt(index));

            CopyValues(fromUser, destinationUser);
            destinationUser.AddApplicationRoles(fromUser.ApplicationRoles);
            destinationUser.AddBusinessUnits(fromUser.BusinessUnits);
        }
    }
}