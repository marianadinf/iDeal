using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;
using UIT.iDeal.Infrastructure.Web.ActionResults;
using UIT.iDeal.ViewModel.Users;
using MvcContrib;
using UIT.iDeal.Web.Notification;

namespace UIT.iDeal.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserQuery _query;
        public UserController(IUserQuery query)
        {
            _query = query;
        }
        //
        // GET: /User/
      
        public ActionResult Index()
        {
            var users = _query.GetAll();
            return AutoMappedView<IEnumerable<UserViewModel>>(users);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View(new AddUserForm());
        }
        [HttpPost]
        public ActionResult Create(AddUserForm addUserForm)
        {
            var u = _query.GetOne(user => user.Username == addUserForm.Username);
            if (u != null)
            {
                return HandleForm(addUserForm).WithFailureResult(ShowErrorMessage());
            }
            return
                HandleForm(addUserForm)
                    .WithSuccessResult(ShowSuccessMessage());
           
        }
        public ActionResult ShowSuccessMessage()
        {
            this.ShowMessage(MessageType.Success, "User Created", true);
            return this.RedirectToAction(x => x.Index());
        }
        public ActionResult ShowErrorMessage()
        {
            this.ShowMessage(MessageType.Error, "User account already exist.", true);
            return this.RedirectToAction(x => x.Create());
        }
    }
}
