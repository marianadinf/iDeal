using System.Linq;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Common.Builders.Entities.Framework
{
    public class EntityBuilder<T> : ObjectBuilder<T> where T : Entity
    {
        public EntityBuilder() :base() { }

        public EntityBuilder(int listSize) :base(listSize) { }


        protected void CopyValues(T from, T to)
        {

            var entityType = typeof(T);

            entityType
                .GetProperties()
                .Where(p => p.Name != "Id" && p.CanWrite)
                .Each(p => p.SetValue(to, entityType.GetProperty(p.Name).GetValue(from, null), null));

        }
    }
}