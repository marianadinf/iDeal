using System.Collections.Generic;
using System.Linq;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Common.Builders.Entities
{
    public class ModuleReferenceDataBuilder : ReferenceDataBuilderFor<Module>
    {
        readonly IEnumerable<ApplicationRole> _applicationRoles;

        public ModuleReferenceDataBuilder(IEnumerable<ApplicationRole> applicationRoles)
        {
            _applicationRoles = applicationRoles;
        }

        protected override List<Module> BuildList()
        {
            var modules = base.BuildList();
            var index = 0;
            
            foreach (var module in modules)
            {
                var applicationCodesForModule = _applicationCodeGroups.ElementAtOrDefault(index++);
                if (applicationCodesForModule != null)
                {
                    module.AddApplicationRoles(_applicationRoles.Where(x => applicationCodesForModule.Contains(x.Code)));
                }
            }

            return modules;
        }

        IEnumerable<string>[] _applicationCodeGroups = new[]
        {
            new[] {"ASSPWRUSR", "ADMIN"},
            new[] {"ASSMAN","ASSAN","ADMIN","ASSGUEST"},
        };
        public ModuleReferenceDataBuilder WithAllApplicationCodeGroups(params IEnumerable<string>[] applicationCodeGroups)
        {
            _applicationCodeGroups = applicationCodeGroups;
            return this;
        }



        
    }
}