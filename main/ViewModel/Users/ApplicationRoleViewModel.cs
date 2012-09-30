using System;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.ViewModel.Users
{
    public class ApplicationRoleViewModel : IMapFromDomain<ApplicationRole>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}