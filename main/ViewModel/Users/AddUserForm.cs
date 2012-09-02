using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UIT.iDeal.Commands.AddUser;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.ViewModel.Users
{
    public class AddUserForm : IMapToCommand<AddUserCommand>, IHaveCustomMappings
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Guid> ApplicationRoleIds { get; set; }
        public List<Guid> BusinessUnitIds { get; set; }
        
        public void CreateMappings(IConfiguration configuration)
        {
            configuration
                .CreateMap<User, AddUserForm>()
                .ForMember(d => d.ApplicationRoleIds, o => o.MapFrom(s => s.ApplicationRoles.Select(x => x.Id)))
                .ForMember(d => d.BusinessUnitIds, o => o.MapFrom(s => s.BusinessUnits.Select(x => x.Id)));

        }
    }
}

