using System.Collections.Generic;
using Bddify.Scanners.StepScanners.ExecutableAttribute.GwtAttributes;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.Acceptance.UserStories.US001
{
    public abstract class us001_sc02<T> : ScenarioFor<T, us001_create_new_user_login> where T : class 
    {
     
        protected User _newUser;

        protected us001_sc02() : base(1, "Successful creation") { }
        
        public virtual void Given_I_am_an_admin()  { }
        
        public virtual void AndGiven_there_are_2_users()
        {
          
            List<User> users = new UserBuilder(2);
            Database.SaveList(users);
        }

        [AndGiven(StepTitle = "And I have entered the user's first, last, user names as well as his email")]
        public virtual void AndGiven_I_have_entered_first_last_user_name_as_well_as_his_email()
        {
            _newUser = new UserBuilder()
                .WithUserName("ftheolade")
                .WithFirstName("Franck")
                .WithLastName("Theolade")
                .WithEmail("ftheolade@gmail.com");
        }

        [AndGiven(StepTitle = "And I have selected the Application role 'Investment Analyst'")]
        public virtual void AndGiven_I_have_selected_the_Application_role_Investment_Analyst() { }

        [AndGiven(StepTitle = "And I have selected the Business unit 'Business Unit 1'")]
        public virtual void AndGiven_I_have_selected_the_Business_unit_Business_Unit_1() { }

        public abstract void When_I_create_a_user();
        
        public abstract void Then_I_should_successfully_create_the_new_user();
        
        public abstract void AndThen_I_should_be_redirected_to_the_Users_List();
        
        public abstract void And_there_should_be_3_users_in_the_list();

        [AndThen(StepTitle = "And the last user's details should match the one I have entered")]
        public abstract void AndThen_the_last_user_first_last_user_details_should_match_the_one_I_have_entered();

       

    }
}
