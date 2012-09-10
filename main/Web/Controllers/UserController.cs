using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Extensions.Web;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Domain.Model.ReferenceData;
using UIT.iDeal.Infrastructure.Web.ActionResults;
using UIT.iDeal.ViewModel.Users;
using MvcContrib;

namespace UIT.iDeal.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserQuery _query;
        private readonly IReferenceDataQuery<ApplicationRole> _applicationRoleReferenceDataQuery;
        private readonly IReferenceDataQuery<BusinessUnit> _businessUnitReferenceDataQuery;

        public UserController(IUserQuery query,
                              IReferenceDataQuery<ApplicationRole> applicationRoleReferenceDataQuery,
                              IReferenceDataQuery<BusinessUnit> businessUnitReferenceDataQuery)
        {
            _query = query;
            _applicationRoleReferenceDataQuery = applicationRoleReferenceDataQuery;
            _businessUnitReferenceDataQuery = businessUnitReferenceDataQuery;
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
        public AutoMappedJsonResult GetPermissionsMatrix(IList<Guid> applicationRoleIds)
        {
            /*var permissionMatrix = _permissionQuery.GetAll(x => x.Contains(applicationRoleIds));
            return AutoMappedJsonResult<List<string>>(permissionMatrix);*/

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
