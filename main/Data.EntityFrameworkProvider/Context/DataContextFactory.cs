using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Reflection;
using Seterlund.CodeGuard;

using UIT.iDeal.Common;
using UIT.iDeal.Common.Extensions;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Context
{
    public class DataContextFactory : IDataContextFactory
    {
        private static readonly Type EntityType =
            typeof(EntityTypeConfiguration<>);

        private static readonly Type ComplexType =
            typeof(ComplexTypeConfiguration<>);

        private static readonly
            ConcurrentDictionary<string, IDictionary<MethodInfo, object>>
            MappingConfigurations =
                new ConcurrentDictionary<string, IDictionary<MethodInfo, object>>();

        private readonly string _nameOrConnectionString;

        private static Type _contextType = typeof(DataContext);

        public DataContextFactory(string nameOrConnectionString)
        {
            Guard.That(nameOrConnectionString).IsNotNullOrEmpty();           

            _nameOrConnectionString = nameOrConnectionString;
        }

        public static Type ContextType
        {
            get
            {
                return _contextType;
            }

            set
            {
                Guard.That(value).IsNotNull();

                if (!value.IsConcreteTypeOf<DataContext>())
                {
                    throw new ArgumentException(string.Format("{0} must inherit from {1}",value.Name, typeof(DataContext).Name));
                }
                
                _contextType = value;
            }
        }

        
        private static IDictionary<MethodInfo, object> BuildConfigurations()
        {
            var addMethods = typeof(ConfigurationRegistrar).GetMethods()
                .Where(m => m.Name.Equals("Add"))
                .ToList();

            var entityTypeMethod = addMethods.First(m =>
                                                    m.GetParameters()
                                                        .First()
                                                        .ParameterType
                                                        .GetGenericTypeDefinition()
                                                        .IsAssignableFrom(EntityType));

            var complexTypeMethod = addMethods.First(m =>
                                                     m.GetParameters().First()
                                                         .ParameterType
                                                         .GetGenericTypeDefinition()
                                                         .IsAssignableFrom(ComplexType));

            var configurations = new Dictionary<MethodInfo, object>();

            var types = ContextType.Assembly
                .GetExportedTypes()
                .Where(IsMappingType)
                .ToList();

            foreach (var type in types)
            {
                MethodInfo typedMethod;
                Type modelType;

                if (IsMatching(
                    type, out modelType, t => EntityType.IsAssignableFrom(t)))
                {
                    typedMethod = entityTypeMethod.MakeGenericMethod(
                        modelType);
                }
                else if (IsMatching(
                    type, out modelType, t => ComplexType.IsAssignableFrom(t)))
                {
                    typedMethod = complexTypeMethod.MakeGenericMethod(
                        modelType);
                }
                else
                {
                    continue;
                }

                configurations.Add(
                    typedMethod, Activator.CreateInstance(type));
            }

            return configurations;
        }

        private static bool IsMappingType(Type matchingType)
        {
            if (!matchingType.IsClass ||
                matchingType.IsAbstract)
            {
                return false;
            }

            Type temp;

            return IsMatching(
                matchingType,
                out temp,
                t =>
                EntityType.IsAssignableFrom(t) ||
                ComplexType.IsAssignableFrom(t));
        }

        private static bool IsMatching(
            Type matchingType,
            out Type modelType,
            Predicate<Type> matcher)
        {
            modelType = null;

            while (matchingType != null)
            {
                if (matchingType.IsGenericType)
                {
                    var definationType = matchingType
                        .GetGenericTypeDefinition();

                    if (matcher(definationType))
                    {
                        modelType = matchingType.GetGenericArguments().First();
                        return true;
                    }
                }

                matchingType = matchingType.BaseType;
            }

            return false;
        }

        private DataContext CreateContext()
        {
            return (DataContext)Activator.CreateInstance(ContextType, new object[]{ _nameOrConnectionString, GetConfigurations()});
        }

        private IDictionary<MethodInfo, object> GetConfigurations()
        {
            var configurations = MappingConfigurations.GetOrAdd(
                _nameOrConnectionString,
                key => BuildConfigurations());
            return configurations;
        }

        public DataContext Create()
        {
            return CreateContext();
        }
    }
}