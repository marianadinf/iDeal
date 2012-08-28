using UIT.iDeal.Commands.Factories;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Common.Interfaces.Data;

namespace UIT.iDeal.Commands.AddUser
{
   public class AddUserCommandHandler : ICommandHandler<AddUserCommand>
    {
       private readonly IUserRepository _repository;

       public AddUserCommandHandler(IUserRepository repository)
       {
           _repository = repository;
       }

       public AddUserCommand Command { get; set; }

       public void Handle()
       {
           _repository.Save(UserFactory.Create(Command));
       }



    }
}
