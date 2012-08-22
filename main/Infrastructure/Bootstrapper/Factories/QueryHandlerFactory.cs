using System.Collections.Generic;
using Castle.Windsor;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Interfaces.Queries;
using UIT.iDeal.Common.Types;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Factories
{
    public class QueryHandlerFactory : Disposable, IQueryHandlerFactory
    {
        private readonly IWindsorContainer _container;
        private readonly IList<IQueryHandler> _createdQueryHandlers = new List<IQueryHandler>();

        public QueryHandlerFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public IQueryHandler Create<T>() 
            where T : class, IQueryHandler
        {
            return AddAndReturn(_container.Resolve<T>());
        }

        protected override void DisposeCore()
        {
            _createdQueryHandlers.Each(q => _container.Release(q));

        }

        private IQueryHandler AddAndReturn(IQueryHandler newQueryHandler)
        {
            _createdQueryHandlers.Add(newQueryHandler);

            return newQueryHandler;
        }
    }
}