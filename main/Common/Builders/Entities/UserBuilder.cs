using System.Linq;
using System.Collections.Generic;

using FizzWare.NBuilder;
using UIT.iDeal.Common.Builders.Base;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;
using UIT.iDeal.Common.Builders.DataSources.Base;
using UIT.iDeal.Common.Builders.DataSources.Domain;

namespace UIT.iDeal.Common.Builders.Entities
{

    public class UserBuilder : EntityBuilder<User>
    {
        #region members

        DatasourceBase<User> _userDataSource;
        private IEnumerable<User> UserDataSource
        {
            get { return LazyInitialiseFor(_userDataSource, size => new UserDataSource().Take(size)); }
        }

        private string[] _firstNames;
        private IEnumerable<string>  FirstNames
        {
            get { return LazyInitialiseFor(_firstNames, listOfSize => UserDataSource.Select(x => x.Firstname).Take(listOfSize)); }
        }

        private string[] _lastNames;
        private IEnumerable<string> LastNames
        {
            get { return LazyInitialiseFor(_lastNames, size => UserDataSource.Select(x => x.Lastname).Take(size)); }
        }

        private string[] _userNames;
        private IEnumerable<string> UserNames
        {
            get { return LazyInitialiseFor(_userNames, size => UserDataSource.Select(x => x.Username).Take(size)); }
        }

        private string[] _emails;
        private IEnumerable<string> Emails
        {
            get { return LazyInitialiseFor(_emails, size => UserDataSource.Select(x => x.Email).Take(size)); }
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
                    .Do(user => PopulateUser(user,ref index))
                    .Build()
                    .ToList();
        }

        
        public UserBuilder WithDataSource(DatasourceBase<User> userDataSource)
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

        private void PopulateUser(User destinationUser, ref int index )
        {
            var fromUser = User.Create(FirstNames.ElementAt(index),
                                       LastNames.ElementAt(index),
                                       UserNames.ElementAt(index),
                                       Emails.ElementAt(index));

            CopyValues(fromUser, destinationUser);
            destinationUser.AddApplicationRoles(fromUser.ApplicationRoles);
            destinationUser.AddBusinessUnits(fromUser.BusinessUnits);
            index++;
        }
    }
}