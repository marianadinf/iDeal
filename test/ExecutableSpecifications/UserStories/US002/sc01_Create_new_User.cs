using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using UIT.iDeal.Acceptance.UserStories.US002;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;
using UIT.iDeal.ViewModel.Users;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Acceptance.ExecutableSpecifications.UserStories.US002
{
    public class sc01_Create_new_User : us002_sc01<PostControllerScenario<UserController, AddUserForm>>
    {
        private IEnumerable<User> _allSavedUsers;
        public override void When_I_create_a_user()
        {
            SUT.Form = new AddUserForm();
            SUT.Form.Firstname = _firstname;
            SUT.Form.Lastname = _lastname;
            SUT.Form.Username = _username;
            SUT.Form.Email = _email;
            //change here
            SUT.ExecuteAction(x=>x.Create(SUT.Form));
        }

        public override void Then_I_should_see_a_quick_message()
        {
            SUT.Controller.TempData["Success"].Should().NotBeNull();
        }

        public override void And_I_should_be_redirected_to_the_Users_List()
        {
            SUT.ActionResult.ShouldBeRedirect<UserController>(x => x.Index());
        }

        public override void And_there_should_be_3_users_in_the_list()
        {
            _allSavedUsers = Database.Query<User>().GetAll().ToList();
            _allSavedUsers.Count().Should().Be(3);
        }
    }
}
