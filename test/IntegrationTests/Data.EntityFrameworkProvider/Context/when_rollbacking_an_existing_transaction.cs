using System;
using Machine.Specifications;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Data.EntityFrameworkProvider.Repositories;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.IntegrationTests.Data.EntityFrameworkProvider.Context
{
    [Subject(typeof(UnitOfWork))]
    public class when_rollbacking_an_existing_transaction : DatabaseSpec
    {
        private static TasksRepository _repository;

        Establish context = () =>
        {
            UnitOfWork.BeginTransaction();
            _repository = new TasksRepository(Context);

            _repository.Save(new TaskBuilder());


        };

        Because of = () =>
            UnitOfWork.RollbackTransaction();

        It should_rollback_any_changes = () =>
            _repository.GetAll().ShouldBeEmpty();
    }
}
