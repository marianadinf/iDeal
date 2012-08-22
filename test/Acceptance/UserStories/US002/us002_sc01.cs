using System;
using System.Collections.Generic;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.Acceptance.UserStories.US002
{
    public abstract class us002_sc01<T> : ScenarioFor<T, us002_create_new_user_login> where T : class 
    {
        protected List<User> _users;
        protected string _firstname = "firstname3";
        protected string _lastname = "lastname3";
        protected string _username = "username3";
        protected string _email = "_email3";
        protected us002_sc01() : base(1, "Successful creation")
        {
        }
        public virtual void Given_I_am_an_admin()
        {
            
        }
        public virtual void Given_there_are_2_users()
        {
            _users = new UserBuilder(2);
            Database.SaveList(_users);
        }

        public abstract void When_I_create_a_user();
        public abstract void Then_I_should_see_a_quick_message();
        public abstract void And_I_should_be_redirected_to_the_Users_List();
        public abstract void And_there_should_be_3_users_in_the_list();

    }
}
