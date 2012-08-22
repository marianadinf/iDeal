using UIT.iDeal.Common.Interfaces.Commands;

namespace UIT.iDeal.TestLibrary.FakeObjects
{
    public class TestCommandHandler : ICommandHandler<FakeCommand>
    {
        public void Handle()
        {
        }

        public FakeCommand Command { get; set; }
    }
}