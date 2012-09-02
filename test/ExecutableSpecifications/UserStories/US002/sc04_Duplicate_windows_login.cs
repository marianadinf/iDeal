﻿using FluentAssertions;
using UIT.iDeal.Acceptance.UserStories.US002;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;
using UIT.iDeal.ViewModel.Users;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Acceptance.ExecutableSpecifications.UserStories.US002
{
    public class sc04_Duplicate_windows_login : us002_sc04<PostControllerScenario<UserController, AddUserForm>>
    {
        public override void AndGiven_there_are_2_users()
        {
            base.AndGiven_there_are_2_users();
            
            SUT.Form = new AddUserForm
            {
                Firstname = _existingUser.Firstname,
                Lastname = _existingUser.Lastname,
                Username = _existingUser.Username,
                Email = _existingUser.Email
            };
        }

        public override void When_I_enter_a_user_with_an_existing_windows_login()
        {
            SUT.ExecuteAction(x => x.Create(SUT.Form));
        }

        public override void Then_I_should_see_an_error_message()
        {
            SUT.CommandResult.AllErrorMessages.Should().Contain("A user with user name " + _existingUser.Username + " already exists");
        }

        public override void AndThen_I_should_remain_on_the_same_page()
        {
            SUT.ActionResult.ShouldBeDefaultViewForAction();
        }
    }
}
