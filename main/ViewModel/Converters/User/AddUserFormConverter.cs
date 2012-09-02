using System.Linq;
using AutoMapper;
using UIT.iDeal.Common.Extensions.Web;
using UIT.iDeal.ViewModel.Users;

namespace UIT.iDeal.ViewModel.Converters.User
{
    public class AddUserFormConverter : ITypeConverter<Domain.Model.User,AddUserForm>
    {
        public AddUserForm Convert(ResolutionContext context)
        {
            var source = context.SourceValue as Domain.Model.User;
            var destination = context.DestinationValue as AddUserForm ?? new AddUserForm();

            if (source == null) return destination;
            var applicationRoles = source.ApplicationRoles.ToList();
            var businessUnits = source.BusinessUnits.ToList();

            destination.Firstname = source.Firstname;
            destination.Lastname = source.Lastname;
            destination.Username = source.Username;
            destination.Email = source.Email;

            destination.ApplicationRoleIds = applicationRoles.Select(x => x.Id).ToList();
            destination.BusinessUnitIds = businessUnits.Select(x => x.Id).ToList();
            destination.ApplicationRoles = applicationRoles.ToSelectList();
            destination.BusinessUnits = businessUnits.ToSelectList();

            return destination;


        }
    }
}