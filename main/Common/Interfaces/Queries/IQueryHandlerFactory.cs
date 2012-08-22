using System;

namespace UIT.iDeal.Common.Interfaces.Queries
{
    public interface IQueryHandlerFactory : IDisposable
    {
        IQueryHandler Create<T>() where T : class, IQueryHandler;
    }

   
}