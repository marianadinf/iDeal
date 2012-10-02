namespace UIT.iDeal.Common.Interfaces.Data.Repositories.Read
{
    public interface IPagedQueryObject
    {
        int PageIndex { get; }

        int RecordsCount { get; }

        string Sort { get; }
    }
}