using System.Collections.Generic;
using System.Linq;
using Bddify.Scanners.StepScanners.ExecutableAttribute.GwtAttributes;
using UIT.iDeal.Common.Builders.DataSources.ReferenceData;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.Acceptance.UserStories.US001
{
    public abstract class us001_sc02<T> : ScenarioFor<T, us001_create_new_user_login> where T : class 
    {

        private readonly UserBuilder _userBuilder = new UserBuilder();
        private static List<ApplicationRole> _investmentAnalystApplicationRole = new ApplicationRoleReferenceDataSource().Where(x => x.Description == "Investment Analyst").ToList();
        private static List<BusinessUnit> _businessUnit1 = new BusinessUnitReferenceDataSource().Where(x => x.Description == "Business Unit 1").ToList();

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
            _userBuilder
                .WithUserNames("ftheolade")
                .WithFirstNames("Franck")
                .WithLastNames("Theolade")
                .WithEmails("ftheolade@gmail.com");
        }

        [AndGiven(StepTitle = "And I have selected the Application role 'Investment Analyst'")]
        public virtual void AndGiven_I_have_selected_the_Application_role_Investment_Analyst()
        {
            _userBuilder.WithApplicationRoles(_investmentAnalystApplicationRole);
        }

        [AndGiven(StepTitle = "And I have selected the Business unit to 'Business Unit 1'")]
        public virtual void AndGiven_I_have_selected_the_Business_unit_to_Business_Unit_1()
        {
            _newUser = _userBuilder.WithBusinessUnits(_businessUnit1);
        }

        public abstract void When_I_create_a_user();
        
        public abstract void Then_I_should_successfully_create_the_new_user();
        
        public abstract void And_I_should_be_redirected_to_the_Users_List();
        
        public abstract void And_there_should_be_3_users_in_the_list();

        [AndThen(StepTitle = "And the last user's first name should match the one I have entered")]
        public abstract void And_the_last_user_first_name_should_match_the_one_I_have_entered();

        [AndThen(StepTitle = "And the last user's last name should match the one I have entered")]
        public abstract void And_the_last_user_last_name_should_match_the_one_I_have_entered();

        public abstract void And_the_last_user_name_should_match_the_one_I_have_entered();

        [AndThen(StepTitle = "And the last user's application role should be 'Investment Analyst'")]
        public abstract void And_the_last_user_application_role_should_be_Investment_Analyst();

        [AndThen(StepTitle = "And the last user's business unit should be 'Investment Analyst'")]
        public abstract void And_the_last_user_business_unit_should_be_Business_Unit_1();
    }
}
