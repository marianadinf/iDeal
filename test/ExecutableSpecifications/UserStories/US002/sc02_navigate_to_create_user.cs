using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using UIT.iDeal.Acceptance.UserStories.US002;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;
using UIT.iDeal.ViewModel.Users;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Acceptance.ExecutableSpecifications.UserStories.US002
{
   public class sc02_navigate_to_create_user : us002_sc02<ViewControllerScenario<UserController, AddUserForm>>
   {
       public override void When_I_select_on_admin_menu_create_user_submenu()
       {
           SUT.ExecuteAction(x =>x.Create());
       }

       public override void Then_I_should_see_the_view_to_create_a_user()
       {
           SUT.ActionResult.ShouldBeDefaultViewForAction();
       }

       public override void AndThen_the_user_form_should_be_blank()
       {
           SUT.ViewModel.ShouldBeEquivalentToModel(new AddUserForm());
       }
   }
}
