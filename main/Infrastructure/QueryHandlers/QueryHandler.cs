using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using UIT.iDeal.Common.Interfaces.Queries;
using UIT.iDeal.Common.Types;

namespace UIT.iDeal.Infrastructure.QueryHandlers
{
    public abstract class QueryHandler : IQueryHandler
    {
        readonly IList<object> _arguments = new List<object>();
        public IMappingEngine MappingEngine { get; set; }

        public IQueryHandler WithArgument<TArgument>(TArgument argument)
        {
            _arguments.Add(argument);
            return this;
        }


        public abstract object BuildViewModel();

        /// <summary>
        /// Get any argument between the first and last
        /// </summary>
        /// <typeparam name="TArgument">Type of the argument</typeparam>
        /// <param name="argumentNumber">One based index of the argument in order that have been entered</param>
        /// <returns>The argument</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the argument index is outside of the known arguments</exception>
        public TArgument GetArgument<TArgument>(int argumentNumber = 2)
        {
            return (TArgument) _arguments[argumentNumber - 1];
        }

        public TArgument GetFirstArgumentOfType<TArgument>()
        {
            return (TArgument) _arguments.First();
        }

        public TArgument GetLastArgumentOfType<TArgument>()
        {
            return (TArgument) _arguments.Last();
        }
    }
}