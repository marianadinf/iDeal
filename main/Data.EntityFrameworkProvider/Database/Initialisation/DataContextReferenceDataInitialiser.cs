using System.Collections.Generic;

using UIT.iDeal.Common.Builders.Entities;

using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Data.EntityFrameworkProvider.Repositories;
using UIT.iDeal.Data.EntityFrameworkProvider.Repositories.Write;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Database.Initialisation
{
    public class DataContextReferenceDataInitialiser : IDataContextReferenceDataInitialiser
    {
        public void Populate(DataContext context)
        {
            GenerateAndSaveReferenceDataFor<BusinessUnit>(context);
            GenerateAndSaveReferenceDataFor<Stage>(context);
            
            var applicationRoles = GenerateAndSaveReferenceDataFor<ApplicationRole>(context);
            GenerateAndSaveReferenceDataFor<Module>(context, new ModuleReferenceDataBuilder(applicationRoles));
        }

        private IEnumerable<TReferenceData> GenerateAndSaveReferenceDataFor<TReferenceData>(DataContext context,
                                                                                            List<TReferenceData> referenceDatas = null)
            where TReferenceData : ReferenceData, new()
        {
            return new Repository<TReferenceData>(context).SaveList(referenceDatas ?? new ReferenceDataBuilderFor<TReferenceData>());
        }

        
    }
}