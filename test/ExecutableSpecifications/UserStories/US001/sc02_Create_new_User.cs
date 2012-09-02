using System.Linq;
using FluentAssertions;
using UIT.iDeal.Acceptance.UserStories.US001;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;
using UIT.iDeal.ViewModel.Users;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Acceptance.ExecutableSpecifications.UserStories.US001
{
    public class sc02_Create_new_User : us001_sc02<PostControllerScenario<UserController, AddUserForm>>
    {
        User _lastSavedUser;

        public override void When_I_create_a_user()
        {
            SUT.CreateFormUsing(_newUser);
            SUT.ExecuteAction(x => x.Create(SUT.Form));
        }

        public override void Then_I_should_successfully_create_the_new_user()
        {
            SUT.CommandResult.IsSuccessFull.Should().BeTrue();
        }

        public override void AndThen_I_should_be_redirected_to_the_Users_List()
        {
            SUT.ActionResult.ShouldBeRedirect<UserController>(x => x.Index());
        }

        public override void And_there_should_be_3_users_in_the_list()
        {
            var allUsers = Database.Query<User>().GetAll().ToList();
            _lastSavedUser = allUsers.Last();
            allUsers.Count().Should().Be(3);
        }

        public override void AndThen_the_last_user_first_name_should_match_the_one_I_have_entered()
        {
            _lastSavedUser.Firstname.Should().Be(_newUser.Firstname);
        }

        public override void AndThen_the_last_user_last_name_should_match_the_one_I_have_entered()
        {
            _lastSavedUser.Lastname.Should().Be(_newUser.Lastname);
        }

        public override void AndThen_the_last_user_name_should_match_the_one_I_have_entered()
        {
            _lastSavedUser.Username.Should().Be(_newUser.Username);
        }

        public override void AndThen_the_last_user_application_role_should_be_Investment_Analyst()
        {
            throw new System.NotImplementedException();
        }

        public override void AndThen_the_last_user_business_unit_should_be_Business_Unit_1()
        {
            throw new System.NotImplementedException();
        }
    }
}
