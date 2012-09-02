using UIT.iDeal.Domain.Model;


namespace UIT.iDeal.Common.Builders.Forms
{
    public class AddUserFormBuilder 
    {
       

        //protected override AddUserFormBuilder Build()
        //{
        //    var entity =
        //    _itemTemplate
        //        .With(x => x.Id, _id)
        //        .With(x => x.Operation, _operation)
        //        .With(x => x.Entity, _entity)
        //    .Build();

        //    return entity;
        //}

        //protected override List<AbbreviationForm> BuildList()
        //{
        //    var list =
        //    _listTemplate
        //    .All()
        //        .With(x => x.Id, _id)
        //        .With(x => x.Operation, _operation)
        //        .With(x => x.Entity, _entity)
        //    .Build();

        //    return list.ToList();
        //}

        //protected String _id;
        //protected AbbreviationFormBuilder WithId(String id)
        //{
        //    _id = id;
        //    return this;
        //}

        //protected Int32 _operation;
        //protected AbbreviationFormBuilder WithOperation(Int32 operation)
        //{
        //    _operation = operation;
        //    return this;
        //}

        protected User _entity;
        protected AddUserFormBuilder WithEntity(User entity)
        {
            _entity = entity;
            return this;
        }

    }



}