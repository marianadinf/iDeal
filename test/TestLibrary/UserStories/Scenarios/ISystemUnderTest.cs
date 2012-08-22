namespace UIT.iDeal.TestLibrary.UserStories.Scenarios
{
    public interface ISystemUnderTest<T>
    {
        T SUT { get; set; }
    }
}