﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.Acceptance.UserStories.US002
{
    public abstract class us002_sc04<T> : ScenarioFor<T, us002_create_new_user_login> where T:class 
    {
        protected us002_sc04()
            : base(3, "Duplicate windows login")
        {
        }
        protected List<User> _users;
        protected string _firstname = "firstname3";
        protected string _lastname = "lastname3";
        protected string _username = "username2";
        protected string _email = "email3";
        public virtual void Given_I_am_an_admin()
        {

        }
        public virtual void Given_there_are_2_users()
        {
            _users = new UserBuilder(2);
            Database.SaveList(_users);
        }
        public abstract void When_I_enter_a_user_with_an_existing_windows_login();
        public abstract void Then_I_should_see_an_error_message();
        public abstract void And_I_should_remain_on_the_same_page();
    }
}