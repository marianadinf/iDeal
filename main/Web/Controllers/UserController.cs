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
        private static IEnumerable<ApplicationRole> _applicationRoles;
        private static IEnumerable<BusinessUnit> _businessUnits;

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
        
        private AddUserForm InitialiseSelectLists(AddUserForm addUserForm)
        {
            addUserForm.ApplicationRoles =
                EnumerableExtensions
                    .LazyInitialiseFor(ref _applicationRoles, () => _applicationRoleReferenceDataQuery.GetAll().ToList())
                    .ToSelectList();

            addUserForm.BusinessUnits =
                EnumerableExtensions
                    .LazyInitialiseFor(ref _businessUnits,() => _businessUnitReferenceDataQuery.GetAll().ToList())
                    .ToSelectList();

            return addUserForm;
        }
        
    }
}
