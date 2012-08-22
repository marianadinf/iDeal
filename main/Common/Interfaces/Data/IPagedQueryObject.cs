namespace UIT.iDeal.Common.Interfaces.Data
{
    public interface IPagedQueryObject
    {
        int PageIndex { get; }

        int RecordsCount { get; }

        string Sort { get; }
    }
}