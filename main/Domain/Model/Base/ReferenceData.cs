using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIT.iDeal.Domain.Model.Base
{
    public abstract class ReferenceData : Entity
    {
        public string Code { get; protected set; }
        public string Description { get; protected set; }
    }
}
