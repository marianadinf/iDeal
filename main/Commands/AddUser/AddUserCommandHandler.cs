using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Commands.AddUser
{
   public class AddUserCommandHandler : ICommandHandler<AddUserCommand>
    {
       private readonly IUserRepository _repository;
       public AddUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public AddUserCommand Command
       {
           get; set; }
       public void Handle()
       {
           _repository.Save(User.Create(Command.Firstname, Command.Lastname,Command.Username, Command.Email, Command.ApplicationRoles, Command.BusinessUnits  ));
       }

      
       
    }
}
