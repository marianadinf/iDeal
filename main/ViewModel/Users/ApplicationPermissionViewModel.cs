using System.Collections.Generic;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.ViewModel.Users
{
    public class ApplicationPermissionViewModel : IMapFromDomain<Module>
    {
        public IEnumerable<ApplicationRoleViewModel> ApplicationRoles { get; set; }
        public string Description { get; set; }
        public bool isAuthorised { get; set; }
    }
}