using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Common.Data;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Data.EntityFrameworkProvider.Database.Initialisation;
using UIT.iDeal.Domain.Model.ReferenceData;
using UIT.iDeal.Infrastructure.Configuration;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.IntegrationTests.Data.EntityFrameworkProvider.Database
{
    public class when_populating_data_context_with_reference_datas : WithSubject<DataContextReferenceDataInitialiser>
    {

        private static IEnumerable<IEnumerable<ReferenceData>> ReferenceDatasListWithSameGuidCombSequence
        {
            get
            {
                GuidComb.GuidStartTemplate = Guid.NewGuid;
                GuidComb.DateTimeStartTemplate = () => DateTime.UtcNow;

                var referenceDatasCollection = new List<IEnumerable<ReferenceData>>
                    {
                        ReferenceDataSourceFor<BusinessUnit>(),
                        ReferenceDataSourceFor<ApplicationRole>(),
                        ReferenceDataSourceFor<Stage>()
                    };
                
                referenceDatasCollection
                    .SelectMany(referenceDatas => referenceDatas)
                    .Each(r => r.SetValue(x => x.Id, GuidComb.Generate()));

                return referenceDatasCollection;
            }
        }
        
        static DataContext _dataContext;
        static readonly IEnumerable<BusinessUnit> ExpectedBusinessUnits = GetReferenceDatasFor<BusinessUnit>();
        static readonly IEnumerable<ApplicationRole> ExpectedApplicationRoles = GetReferenceDatasFor<ApplicationRole>();
        static readonly IEnumerable<Stage> ExpectedStages = GetReferenceDatasFor<Stage>();

        Establish context = () =>
        {
            GuidComb.GuidStartTemplate = Guid.NewGuid;
            GuidComb.DateTimeStartTemplate = () => DateTime.UtcNow;
            
            System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<DataContext>());
            var environment = ConfigurationFactory.DevelopmentEnvironment();
            var contextFactory = new DataContextFactory(environment.ConnectionString(ProjectFlavour.IntegrationTests));
            _dataContext = contextFactory.Create();

            _dataContext.Database.Initialize(true);

        };
        Because of = () =>
            Subject.Populate(_dataContext);

        It the_data_context_should_only_contain_the_list_of_business_units_specified_in_its_data_source = () =>
            _dataContext.BusinessUnits.ShouldContainOnly(ExpectedBusinessUnits);

        It the_data_context_should_only_contain_the_list_of_application_roles_specified_in_its_data_source = () =>
            _dataContext.ApplicationRoles.ShouldContainOnly(ExpectedApplicationRoles);

        It the_data_context_should_only_contain_the_list_of_stages_specified_in_its_data_source = () =>
            _dataContext.Stages.ShouldContainOnly(ExpectedStages);


        private static IEnumerable<TRefenceData> ReferenceDataSourceFor<TRefenceData>()
            where TRefenceData : ReferenceData, new()
        {
            return ReferenceDataBuilderFor<TRefenceData>.GetInstanceOfReferenceDataSource().ToList();
        }

        public static IEnumerable<TReferenceData>  GetReferenceDatasFor<TReferenceData>()
        {
            return
                ReferenceDatasListWithSameGuidCombSequence
                .First(x => x.GetType() == typeof (IEnumerable<TReferenceData>))
                .Cast<TReferenceData>();
        }

        
    }
}
