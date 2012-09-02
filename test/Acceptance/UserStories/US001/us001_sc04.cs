using System.Collections.Generic;
using System.Linq;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.Acceptance.UserStories.US001
{
    public abstract class us001_sc04<T> : ScenarioFor<T, us001_create_new_user_login> where T:class 
    {
        List<User> _users = new UserBuilder(2);
        protected User _existingUser;
        protected string _expectedErrorMessage;

        protected us001_sc04()
            : base(3, "Duplicate windows login")
        {
        }
        

        public virtual void Given_I_am_an_admin()
        {

        }
        public virtual void AndGiven_there_are_2_users()
        {
            _existingUser = _users.First();
            _expectedErrorMessage = string.Format("A user with user name '{0}' already exists", _existingUser.Username);
            Database.SaveList(_users);
        }
        public abstract void When_I_enter_a_user_with_an_existing_windows_login();
        public abstract void Then_I_should_see_an_error_message();
        public abstract void AndThen_I_should_remain_on_the_same_page();
    }
}
