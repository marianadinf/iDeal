using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Common.Builders.Entities.Framework
{
    public class EntityBuilder<T> : ObjectBuilder<T> where T : Entity
    {
        public EntityBuilder() :base() { }

        public EntityBuilder(int listSize) :base(listSize) { }

    }
}