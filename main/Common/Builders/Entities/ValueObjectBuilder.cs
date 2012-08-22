using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Common.Builders.Entities
{
    public abstract class ValueObjectBuilder<T> : ObjectBuilder<T> where T:ValueObject
    {
    }
}