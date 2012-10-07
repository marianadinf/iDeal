using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UIT.iDeal.Common.Extensions.Web;
using UIT.iDeal.Common.Interfaces.Data.Repositories.Read;
using UIT.iDeal.Domain.Model.ReferenceData;
using UIT.iDeal.Infrastructure.Web.ActionResults;

using MvcContrib;
using UIT.iDeal.ViewModel.Users;



namespace UIT.iDeal.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserQuery _userQuery;
        private readonly IModuleQuery _moduleQuery;
        private readonly IReferenceDataQuery<ApplicationRole> _applicationRoleReferenceDataQuery;
        private readonly IReferenceDataQuery<BusinessUnit> _businessUnitReferenceDataQuery;

        public UserController(IUserQuery userQuery,
                              IModuleQuery moduleQuery,
                              IReferenceDataQuery<ApplicationRole> applicationRoleReferenceDataQuery,
                              IReferenceDataQuery<BusinessUnit> businessUnitReferenceDataQuery
            )
        {
            _userQuery = userQuery;
            _moduleQuery = moduleQuery;
            _applicationRoleReferenceDataQuery = applicationRoleReferenceDataQuery;
            _businessUnitReferenceDataQuery = businessUnitReferenceDataQuery;
        }

        //
        // GET: /User/
        public ActionResult Index()
        {
            var users = _userQuery.GetAll();
            return AutoMappedView<IEnumerable<UserViewModel>>(users);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View(InitialiseSelectLists(new AddUserForm()));
        }

        [HttpPost]
        public ActionResult Create(AddUserForm addUserForm)
        {
            return
                HandleForm(InitialiseSelectLists(addUserForm))
                    .WithSuccessResult(this.RedirectToAction(x => x.Index()));
           
        }

        [HttpPost]
        public AutoMappedJsonResult GetAllModulePermissions()
        {
            var applicationPermissions = _moduleQuery.GetAll().ToList();
            return AutoMappedJsonResult<IEnumerable<ApplicationPermissionViewModel>>(applicationPermissions);
        }
        [HttpPost]
        public AutoMappedJsonResult GetModulePermissionsByApplicationRole(string applicationRoleId)
        {
            throw new NotImplementedException();
        }

        private AddUserForm InitialiseSelectLists(AddUserForm addUserForm)
        {
            addUserForm.ApplicationRoles = _applicationRoleReferenceDataQuery.GetAllCached().ToSelectList();
            addUserForm.BusinessUnits = _businessUnitReferenceDataQuery.GetAllCached().ToSelectList();
            return addUserForm;
        }
        
    }
}
