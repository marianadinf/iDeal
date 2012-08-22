using System;
using System.Collections.Generic;
using System.Linq;
using Seterlund.CodeGuard;

namespace UIT.iDeal.Common.Extensions
{
    public class SourceAndDestinationTypes
    {
        public Type SourceType { get; protected set; }
        public Type DestinationType { get; protected set; }

        public SourceAndDestinationTypes(Type sourceType, Type destinationType)
        {
            SourceType = sourceType;
            DestinationType = destinationType;
        }

    }

    public static class MapperExtensions
    {
        public static Func<object, Type, Type, object> Map =
            (entity, entityType, dtoType) =>
                {
                    throw new InvalidOperationException(
                        "The Mapping function must be set");
                };

        public static IEnumerable<TViewModel> MapViewModel<TEntity, TViewModel>(this IQueryable<TEntity> entities)
            where TEntity : class
            where TViewModel : class
        {
            return entities.Select(x => x.MapViewModel<TEntity, TViewModel>());
        }

        public static TViewModel MapViewModel<TEntity, TViewModel>(this TEntity entity)
            where TEntity : class
            where TViewModel : class
        {
            return Map(entity, typeof(TEntity), typeof(TViewModel)) as TViewModel;
        }

     


        public static IEnumerable<SourceAndDestinationTypes> GetSourceTypeFromMarkerInterfaceAndDestinationTypeFromModel(this IEnumerable<Type> withinTypes, Type genericMarkerMappingInterface)
        {
            return withinTypes.GetSourceAndDestinationTypesFor(genericMarkerMappingInterface);
        }

        public static IEnumerable<SourceAndDestinationTypes> GetSourceTypeFromModelAndDestinationTypeFromMakerInterface(this IEnumerable<Type> withinTypes, Type genericMarkerMappingInterface)
        {
            return withinTypes.GetSourceAndDestinationTypesFor(genericMarkerMappingInterface, swapDestinationAndSourceTypes:true);
        }

        private static IEnumerable<SourceAndDestinationTypes> GetSourceAndDestinationTypesFor(this IEnumerable<Type> withinTypes, Type genericMarkerMappingInterface, Boolean swapDestinationAndSourceTypes = false)
        {
            return
                from t in withinTypes
                from i in t.GetInterfaces()
                where i.IsGenericType && i.GetGenericTypeDefinition() == genericMarkerMappingInterface && t.IsConcrete()
                select swapDestinationAndSourceTypes ? new SourceAndDestinationTypes(t, i.GetGenericArguments()[0]) : new SourceAndDestinationTypes(i.GetGenericArguments()[0], t);

            
        }
    }
}