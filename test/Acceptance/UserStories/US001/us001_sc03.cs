using System.Collections.Generic;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.Acceptance.UserStories.US001
{
    public abstract class us001_sc03<T> : ScenarioFor<T, us001_create_new_user_login> where T:class
    {

        protected IEnumerable<string> _expectedErrorMessages = new[]
        {
            "'Firstname' must not be empty.",
            "'Firstname' must be between 2 and 50 characters. You entered 0 characters.",
            "'Lastname' must not be empty.",
            "'Lastname' must be between 2 and 50 characters. You entered 0 characters.",
            "'Username' must not be empty.",
            "'Username' must be between 3 and 50 characters. You entered 0 characters.",
            "'Email' must not be empty.",
            "'Email' must be between 3 and 50 characters. You entered 0 characters."
        };
        protected us001_sc03()
            : base(3, "Uncomplete mandatory field")
        {
        }
        public virtual void Given_I_am_an_admin()
        {

        }
        public abstract void When_I_create_a_user_without_completing_all_fields();
        public abstract void Then_I_should_see_an_errors_messages_for_all_mandatory_fields();
    }
}
