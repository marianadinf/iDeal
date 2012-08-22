using System.Collections.Generic;
using System.Data.Entity;

using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;

using Machine.Fakes;
using Machine.Specifications;

using UIT.iDeal.Common.Builders.DataSources.ReferenceData;
using UIT.iDeal.Common.Builders.Entities.Framework;
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
        static DataContext _dataContext;
        static readonly IEnumerable<BusinessUnit> ExpectedBusinessUnits = MakeReferenceDataPersistable(new BusinessUnitReferenceDataSource());
        static readonly IEnumerable<ApplicationRole> ExpectedApplicationRoles = MakeReferenceDataPersistable(new ApplicationRoleReferenceDataSource());
        static readonly IEnumerable<Stage> ExpectedStages = MakeReferenceDataPersistable(new StageReferenceDataSource());

        Establish context = () =>
        {
            BuilderSetup.SetDefaultPropertyNamer(new PersistableEntityPropertyNamer(new ReflectionUtil()));
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


        private static IEnumerable<TRefenceData> MakeReferenceDataPersistable<TRefenceData>(IEnumerable<TRefenceData> referenceDatas)
            where TRefenceData : ReferenceData
        {
            GuidComb.Reset();
            return referenceDatas.Each(r => r.SetValue(x => x.Id, GuidComb.Generate()));
        }

        
    }
}
