using UIT.iDeal.Commands.Factories;
using UIT.iDeal.Common.Commands;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Common.Interfaces.Data.Repositories.Read;
using UIT.iDeal.Common.Interfaces.Data.Repositories.Write;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Commands.AddUser
{
    public class AddUserCommandHandler : ICommandHandler<AddUserCommand>
    {
        readonly IUserRepository _repository;
        readonly IReferenceDataQuery<ApplicationRole> _applicationRoleReferenceDataQuery;
        readonly IReferenceDataQuery<BusinessUnit> _businessUnitReferenceDataQuery;
        readonly IReferenceDataQuery<Module> _moduleReferenceDataQuery;
        public AddUserCommandHandler(IUserRepository repository,
                                     IReferenceDataQuery<ApplicationRole> applicationRoleReferenceDataQuery,
                                     IReferenceDataQuery<BusinessUnit> businessUnitReferenceDataQuery,
                                        IReferenceDataQuery<Module> moduleReferenceDataQuery )
        {
            _repository = repository;
            _applicationRoleReferenceDataQuery = applicationRoleReferenceDataQuery;
            _businessUnitReferenceDataQuery = businessUnitReferenceDataQuery;
            _moduleReferenceDataQuery = moduleReferenceDataQuery;
        }

        public AddUserCommand Command { get; set; }

        public void Handle()
        {

            if (_repository.Exists(u => u.Username == Command.Username))
            {
                throw new BusinessRuleExceptionFor<User>(u => u.Username,
                                                         "A user with user name '{0}' already exists",
                                                         Command.Username);
            }
            
            var selectedApplicationRoles = 
                _applicationRoleReferenceDataQuery.GetAll(x => Command.ApplicationRoleIds.Contains(x.Id));

            var selectedBusinessUnits =
                _businessUnitReferenceDataQuery.GetAll(x =>  Command.BusinessUnitIds.Contains(x.Id));

            var selectedModules = _moduleReferenceDataQuery.GetAll(x => Command.ModuleIds.Contains(x.Id));
            
            _repository.Save(UserFactory.Create(Command, selectedApplicationRoles, selectedBusinessUnits, selectedModules));
        }
    }
}
