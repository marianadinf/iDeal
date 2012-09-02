using FluentAssertions;
using UIT.iDeal.Acceptance.UserStories.US001;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;
using UIT.iDeal.ViewModel.Users;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Acceptance.ExecutableSpecifications.UserStories.US001
{
   public class sc01_navigate_to_create_user : us001_sc01<ViewControllerScenario<UserController, AddUserForm>>
   {
       public override void When_I_select_on_admin_menu_create_user_submenu()
       {
           SUT.ExecuteAction(x =>x.Create());
       }

       public override void Then_I_should_see_the_view_to_create_a_user()
       {
           SUT.ActionResult.ShouldBeDefaultViewForAction();
       }

       public override void AndThen_the_user_first_name_should_be_empty()
       {
           SUT.ViewModel.Firstname.Should().BeBlank();
       }

       public override void AndThen_the_user_last_name_should_be_empty()
       {
           SUT.ViewModel.Lastname.Should().BeBlank();
       }

       public override void AndThen_the_user_name_should_be_empty()
       {
           SUT.ViewModel.Username.Should().BeBlank();
       }

       public override void AndThen_the_user_email_should_be_empty()
       {
           SUT.ViewModel.Email.Should().BeBlank();
       }

       public override void AndThen_the_user_application_role_should_not_be_selected()
       {
           throw new System.NotImplementedException();
       }

       public override void AndThen_the_user_business_unit_should_not_be_selected()
       {
           throw new System.NotImplementedException();
       }
   }
}
