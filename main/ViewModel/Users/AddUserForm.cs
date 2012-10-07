using System;
using System.Web.Mvc;
using System.Collections.Generic;

using AutoMapper;

using UIT.iDeal.Commands.AddUser;

using UIT.iDeal.Domain.Model;
using UIT.iDeal.Common.Extensions;

using UIT.iDeal.Common.Interfaces.ObjectMapping;

namespace UIT.iDeal.ViewModel.Users
{
    public class AddUserForm : IMapToCommand<AddUserCommand>, IHaveCustomMappings
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        
        public List<Guid> ApplicationRoleIds { get; set; }
        public SelectList ApplicationRoles { get; set; }

        public List<Guid> BusinessUnitIds { get; set; }
        public SelectList BusinessUnits { get; set; }

        public List<Guid> ModuleIds { get; set; }
        public SelectList Modules { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration
                .CreateMap<User, AddUserForm>()
                .IgnoreSelectListAndMapIdsFrom(s => s.ApplicationRoles)
                .IgnoreSelectListAndMapIdsFrom(s => s.BusinessUnits)
                .IgnoreSelectListAndMapIdsFrom(s=> s.Modules);
        }
    }
}

