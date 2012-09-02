using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.Acceptance.UserStories.US001
{
    public abstract class us001_sc01<T> : ScenarioFor<T, us001_create_new_user_login> where T : class 
    {
        protected us001_sc01() : base(2, "Navigating to create a user")
        {
        }
        public virtual void Given_I_am_an_admin()
        {

        }

        public abstract void When_I_select_on_admin_menu_create_user_submenu();
        public abstract void Then_I_should_see_the_view_to_create_a_user();
        public abstract void AndThen_the_user_first_name_should_be_empty();
        public abstract void AndThen_the_user_last_name_should_be_empty();
        public abstract void AndThen_the_user_name_should_be_empty();
        public abstract void AndThen_the_user_email_should_be_empty();
        public abstract void AndThen_the_user_application_role_should_not_be_selected();
        public abstract void AndThen_the_user_business_unit_should_not_be_selected();

        
    }
}
