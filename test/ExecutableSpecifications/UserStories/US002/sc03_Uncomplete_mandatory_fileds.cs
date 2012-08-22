using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIT.iDeal.Acceptance.UserStories.US002;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;
using UIT.iDeal.ViewModel.Users;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Acceptance.ExecutableSpecifications.UserStories.US002
{
    public class sc03_Uncomplete_mandatory_fileds : us002_sc03<PostControllerScenario<UserController, AddUserForm>>
    {
        public override void When_I_create_a_user_without_completing_all_fields()
        {
            throw new NotImplementedException();
        }

        public override void Then_I_should_see_an_error_message()
        {
            throw new NotImplementedException();
        }
    }
}
