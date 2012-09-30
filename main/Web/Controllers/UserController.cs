using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Extensions.Web;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Domain.Model.ReferenceData;
using UIT.iDeal.Infrastructure.Web.ActionResults;

using MvcContrib;
using UIT.iDeal.ViewModel.Users;



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
        public JsonResult GetAllModulePermissions()
        {

            //temporary until changes made in domain
            var modulePermissions = new[]
            {
                new ModulePermissionViewModel
                {
                    Description = "Asset Module Lookup - Create/ Edit",
                    ApplyForApplicationRoles =
                        _applicationRoleReferenceDataQuery
                            .GetAllCached()
                            .Where(x => x.Code == "ADMIN" || x.Code == "ASSPWRUSR")
                            .MapViewModel<ApplicationRole, ApplicationRoleViewModel>()

                },

                new ModulePermissionViewModel
                {
                    Description = "Asset - View",
                    ApplyForApplicationRoles =
                        _applicationRoleReferenceDataQuery
                            .GetAllCached()
                            .Where(x => new[]{"ASSMAN","ASSAN","ADMIN", "ASSGUEST"}.Contains(x.Code))
                            .MapViewModel<ApplicationRole, ApplicationRoleViewModel>()
                },

                new ModulePermissionViewModel
                {
                    Description = "Asset - Create",
                    ApplyForApplicationRoles =
                        _applicationRoleReferenceDataQuery
                            .GetAllCached()
                            .Where(x => new[]{"ASSMAN,ASSAN,ADMIN"}.Contains(x.Code))
                            .MapViewModel<ApplicationRole, ApplicationRoleViewModel>()
                },
                
            };

            return Json(modulePermissions);

            //throw new NotImplementedException();
        }
        
        private AddUserForm InitialiseSelectLists(AddUserForm addUserForm)
        {
            addUserForm.ApplicationRoles = _applicationRoleReferenceDataQuery.GetAllCached().ToSelectList();
            addUserForm.BusinessUnits = _businessUnitReferenceDataQuery.GetAllCached().ToSelectList();
            return addUserForm;
        }
        
    }
}
