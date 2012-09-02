using UIT.iDeal.Commands.Factories;
using UIT.iDeal.Common.Commands;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Domain.Model;

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
           
           if (_repository.Exists(u => u.Username == Command.Username))
           {
               throw new BusinessRuleExceptionFor<User>(u => u.Username, 
                                                       "A user with user name {0} already exists",
                                                        Command.Username);
           }

           
           
           _repository.Save(UserFactory.Create(Command));
       }



    }
}
