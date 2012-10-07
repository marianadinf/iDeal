using System.Collections.Generic;
using System.ComponentModel;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.ViewModel.Users
{
    public class ApplicationPermissionViewModel : IMapFromDomain<Module>
    {
        [DisplayName("Apply for Application Roles")]
        public IEnumerable<ApplicationRoleViewModel> ApplicationRoles { get; set; }
        
        [DisplayName("Module name")]
        public string Description { get; set; }
        
        public bool IsAuthorised { get; set; }
    }
}