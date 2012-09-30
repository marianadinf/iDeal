using System.Collections.Generic;

namespace UIT.iDeal.ViewModel.Users
{
    public class ModulePermissionViewModel
    {
        public IEnumerable<ApplicationRoleViewModel> ApplyForApplicationRoles { get; set; }
        public string Description { get; set; }
        public bool isAuthorised { get; set; }
    }
}