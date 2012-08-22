using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;

namespace UIT.iDeal.Common.Builders.Entities.Framework
{
    public class ObjectBuilder<T>
    {
        protected ISingleObjectBuilder<T> _itemTemplate;
        protected IListBuilder<T> _listTemplate;
        protected int _listSize = 3;

        public ObjectBuilder(int listSize = 3)
        {
            _listSize = listSize;
            _itemTemplate = Builder<T>.CreateNew();
            _listTemplate = Builder<T>.CreateListOfSize(_listSize);
        }

        protected virtual T Build()
        {
            return _itemTemplate.Build();
        }

        protected virtual List<T> BuildList()
        {
            return _listTemplate.Build().ToList();
        }

        public static implicit operator T(ObjectBuilder<T> builder)
        {
            if (builder._action != null)
                builder._action(builder._itemTemplate);
            return builder.Build();
        }

        protected Action<IListBuilder<T>> _listAction;
        public ObjectBuilder<T> ApplyActions(Action<IListBuilder<T>> action)
        {
            _listAction = action;
            return this;
        }

        protected Action<ISingleObjectBuilder<T>> _action;
        public ObjectBuilder<T> ApplyActions(Action<ISingleObjectBuilder<T>> action)
        {
            _action = action;
            return this;
        }

        public static implicit operator List<T>(ObjectBuilder<T> builder)
        {
            if (builder._listAction != null)
                builder._listAction(builder._listTemplate);
            return builder.BuildList();
        }

    }
}