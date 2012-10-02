using System.Collections.Generic;

namespace UIT.iDeal.Domain.Model.ReferenceData
{
    public class ApplicationRole : UserRelatedReferenceData
    {
        internal virtual ICollection<Module> internalModules { get; set; }

        public ApplicationRole()
        {
            internalModules = new HashSet<Module>();
        }
    }
}