namespace UIT.iDeal.Common.Interfaces.Queries
{
    public interface IQueryHandler 
    {
        IQueryHandler WithArgument<TArgument>(TArgument argument);

        object BuildViewModel();
    }
}