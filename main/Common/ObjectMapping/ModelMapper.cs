using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UIT.iDeal.Common.Commands;
using UIT.iDeal.Common.Interfaces.ObjectMapping;

namespace UIT.iDeal.Common.ObjectMapping
{
    public class ModelMapper : IModelMapper
    {
        readonly IMappingEngine _mapper;

        public ModelMapper(IMappingEngine mapper)
        {
            _mapper = mapper;
        }

        public ICommand MapFormToCommand(object form)
        {
            var sourceType = form.GetType();
            TypeMap typeMap = Mapper.GetAllTypeMaps().SingleOrDefault(map => map.SourceType == sourceType);
            if (typeMap == null)
            {
                throw new ArgumentException("There is no Command associated with Form {0}", form.GetType().ToString());
            }
            Type destinationType = typeMap.DestinationType;
            return (ICommand) _mapper.Map(form, sourceType, destinationType);
        }

        public virtual TDestination CreateInstance<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public virtual IEnumerable<TDestination> CreateSet<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return source.Select(item => _mapper.Map<TSource, TDestination>(item));
        }

    }
}
