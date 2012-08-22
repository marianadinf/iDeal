using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Domain.Model.ReferenceData
{
    public abstract class ReferenceData : AggregateRoot
    {
        public virtual string Code { get; protected set; }
        public virtual string Description { get; protected set; }
    }
}
