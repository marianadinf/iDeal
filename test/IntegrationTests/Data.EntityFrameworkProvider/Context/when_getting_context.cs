using System.Data.Metadata.Edm;
using System.Linq;
using Machine.Specifications;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Domain;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.IntegrationTests.Data.EntityFrameworkProvider.Context
{

    [Subject(typeof(DataContextFactory))]
    public class when_getting_context : DatabaseSpec
    {
     
        It should_not_be_null = () => 
            Context.ShouldNotBeNull();


        It should_be_data_context_type = () =>
            Context.ShouldBeOfType<DataContext>();

            //[Ignore("No custom mapping defined for now")]
        It should_automatically_register_available_entity_mappings = () =>
            {


                var availableMappings =
                    AssemblyScanner
                        .GetExportedTypesFromAssemblyContaining<Entity>()
                        .Matching(t => t.IsConcreteTypeOf<Entity>() || t.IsConcreteTypeOf<ValueObject>())
                        .Select(t => t.Name)
                        .ToList();


                var items =
                    ObjectContextAdapter
                        .ObjectContext
                        .MetadataWorkspace
                        .GetItems(DataSpace.OSpace);

                items.ShouldNotBeNull();

                var registeredMappings =
                    items
                    .OfType<EntityType>()
                    .Select(e => e.Name)
                    .Concat(items.OfType<ComplexType>()
                                .Select(e => e.Name))
                    .Where(e => !e.Equals("EdmMetadata"))
                    .ToList();

                registeredMappings.ShouldContain(availableMappings);
            };
    }
}