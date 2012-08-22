using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

using Machine.Specifications;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Domain.Model.Base;
using UIT.iDeal.TestLibrary.Extensions;

namespace UIT.iDeal.UnitTests.Domain.Model
{
    public class when_checking_domain_entity_conventions 
    {
        protected static IEnumerable<Type> _domainEntityTypes;

        Because of = () =>
            _domainEntityTypes = DomainEntityTypes;

        It should_have_all_members_virtual = () =>
            MethodsThatAreNotMarkedAsVirtual.ShouldBeEmpty("Non-virtual members found in domain type:");

        It should_have_all_entities_with_parameterless_constructor = () =>
            TypesWithoutParameterlessConstructor.ShouldBeEmpty("Entities found without parameterless constructors");

        It should_have_all_entities_wirh_non_public_setters_on_properties = () =>
           PropertiesThatHavePublicSetters.ShouldBeEmpty("Properties on entities found with public setters");

        It should_have_all_entities_with_both_read_only_collection_and_properties = () =>
           NonReadOnlyCollectionPropertiesThatHaveSetters.ShouldBeEmpty("Found non readonly collections properties on entities ");

        static IEnumerable<Type> DomainEntityTypes
        {
            get
            {
                return AssemblyScanner.FromAssemblyContaining<Entity>()
                    .Matching(t => t.IsConcreteTypeOf<Entity>() && t.IsInNamespace("UIT.iDeal.Domain.Model"));
            }
        }

        static IEnumerable<string> NonReadOnlyCollectionPropertiesThatHaveSetters
        {
            get
            {
                var propertyNames =
                    (from t in _domainEntityTypes
                     from p in t.GetProperties()
                     from i in p.PropertyType.GetInterfaces()
                     where i.IsGenericType && i.GetGenericTypeDefinition() == (typeof (ICollection<>))
                     select string.Format("On Domain {0}, property {1}", t.Name, p.Name)).ToList();

                return propertyNames;
            }
        }

        static IEnumerable<string> MethodsThatAreNotMarkedAsVirtual
        {
            get
            {
                var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;

                var nonVirtualMembers = (from t in _domainEntityTypes
                                         from m in t.GetMethods(bindingFlags)
                                         where !m.IsVirtual
                                         select t.Name + "." + m.Name).Union(
                                             from t in _domainEntityTypes
                                             from p in t.GetProperties(bindingFlags)
                                             where !p.GetGetMethod(true).IsVirtual
                                             select t.Name + "." + p.Name);

                return nonVirtualMembers;
            }
        }

        static IEnumerable<string> PropertiesThatHavePublicSetters
        {
            get
            {
                var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;

                var propertiesWithPublicSetters = (from t in _domainEntityTypes
                                                   from m in t.GetMethods(bindingFlags)
                                                   where m.Name.StartsWith("set_")
                                                   select t.Name + "." + m.Name);

                return propertiesWithPublicSetters;
            }
        }

        static IEnumerable<string> TypesWithoutParameterlessConstructor
        {
            get
            {
                IEnumerable<string> values = (from t in _domainEntityTypes.Where(x => !x.IsConcreteWithDefaultCtor())
                                              select t.Name);
                return values;
            }
        }
    }

}
