using System;
using System.Reflection;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Common.Builders.Entities.Framework
{
    public class PersistableEntityPropertyNamer : SequentialPropertyNamer
    {
        public PersistableEntityPropertyNamer(IReflectionUtil reflectionUtil) : base(reflectionUtil)
        {
        }

        protected override Guid GetGuid(MemberInfo memberInfo)
        {
            return TypeIsEntityAndPropertyIsNamed(memberInfo, "Id") ? Guid.Empty : base.GetGuid(memberInfo);
        }

        protected override int GetInt32(MemberInfo memberInfo)
        {
            return TypeIsEntityAndPropertyIsNamed(memberInfo, "Version") ? 0 : base.GetInt32(memberInfo);
        }

        protected bool TypeIsEntityAndPropertyIsNamed(MemberInfo memberInfo, string propertyName)
        {
            return memberInfo.Name == propertyName && memberInfo.DeclaringType.CanBeCastTo<EntityWithTypedId<Guid>>();
        }
    }
}
