using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.Acceptance.UserStories.US002
{
    public abstract class us002_sc02<T> : ScenarioFor<T, us002_create_new_user_login> where T : class 
    {
        protected us002_sc02() : base(2, "Navigating to create a user")
        {
        }
        public virtual void Given_I_am_an_admin()
        {

        }
        public abstract void When_I_click_on_admin_menu_User_setup_submenu();
        public abstract void And_click_on_create_user_button();
        public abstract void Then_I_should_see_the_view_to_create_a_user();
    }
}
