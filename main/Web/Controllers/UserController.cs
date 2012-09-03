using System.Collections.Generic;
using System.Web.Mvc;
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
            return View(new AddUserForm
                {
                    ApplicationRoles = _applicationRoleReferenceDataQuery.GetAll().ToSelectList(),
                    BusinessUnits = _businessUnitReferenceDataQuery.GetAll().ToSelectList()
                });

        }

        [HttpPost]
        public ActionResult Create(AddUserForm addUserForm)
        {
            return
                HandleForm(addUserForm)
                    .WithSuccessResult(this.RedirectToAction(x => x.Index()));
           
        }
        
    }
}
