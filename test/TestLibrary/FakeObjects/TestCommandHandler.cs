using UIT.iDeal.Common.Commands;

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