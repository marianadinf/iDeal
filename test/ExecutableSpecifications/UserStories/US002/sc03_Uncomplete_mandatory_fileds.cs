using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using UIT.iDeal.Acceptance.UserStories.US002;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;
using UIT.iDeal.ViewModel.Users;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Acceptance.ExecutableSpecifications.UserStories.US002
{
    public class sc03_Uncomplete_mandatory_fileds : us002_sc03<PostControllerScenario<UserController, AddUserForm>>
    {
        public override void Given_I_am_an_admin()
        {
            base.Given_I_am_an_admin();
            SUT.Form = new AddUserForm();
        }
        public override void When_I_create_a_user_without_completing_all_fields()
        {
            SUT.ExecuteAction(x => x.Create(SUT.Form));
        }

        public override void Then_I_should_see_an_error_message()
        {
            SUT.CommandResult.AllErrorMessages.Should().Contain(_expectedErrorMessages);
        }
    }
}
