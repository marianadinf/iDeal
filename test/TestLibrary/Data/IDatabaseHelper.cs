using System.Collections.Generic;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Domain;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.TestLibrary.Data
{
    public interface IDatabaseHelper
    {
        void ResetDatabase();
        T SaveOne<T>(T entity) where T : AggregateRoot;
        IList<T> SaveList<T>(IList<T> list) where T : AggregateRoot;
        IRepository<T> Repository<T>() where T : AggregateRoot;
        IQuery<T> Query<T>() where T : Entity;

        void DropAndRecreateDatabase();
    }
}