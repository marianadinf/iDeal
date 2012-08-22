using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Data.EntityFrameworkProvider.Context;
using UIT.iDeal.Data.EntityFrameworkProvider.Repositories;
using UIT.iDeal.Domain.Model.Base;
using UIT.iDeal.Infrastructure.Configuration.Flavours;

namespace UIT.iDeal.TestLibrary.Data
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly IDatabaseStrategyInitialiser _databaseStrategyInitialiser;
        private readonly DataContext _context;

        public DatabaseHelper(IDatabaseStrategyInitialiser databaseStrategyInitialiser,DataContext context)
        {
            _databaseStrategyInitialiser = databaseStrategyInitialiser;
            _context = context;
        }

        public static ProjectFlavour CurrentProjectFlavour { get; set; }

        public void ResetDatabase()
        {
            DeleteAllData();
        }

        public T SaveOne<T>(T entity) where T : AggregateRoot
        {
            return AddOne(entity);
        }

        public IList<T> SaveList<T>(IList<T> list) where T : AggregateRoot
        {
            return list.Select(entity => AddOne(entity)).ToList();
        }

        public IRepository<T> Repository<T>() where T : AggregateRoot
        {
            return new Repository<T>(_context);
        }

        public IQuery<T> Query<T>() where T : Entity
        {
            return new Query<T>(_context);
        }

        public void DropAndRecreateDatabase()
        {
            _context.Database.Connection.Close();
            _databaseStrategyInitialiser.Apply();
            _context.Database.Initialize(true);
            _context.Database.Connection.Open();
        }

        private T AddOne<T>(T entity) where T : AggregateRoot
        {
            return  Repository<T>().Save(entity);
            
        }

        private void DeleteAllData()
        {
            var sets =
              _context
                  .GetType()
                  .GetProperties()
                  .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(IDbSet<>))
                  .Select(p => (dynamic)p.GetValue(_context, null))
                  .ToList();


            foreach (var set in sets)
            {
                foreach (var item in set)
                {
                    set.Remove(item);
                }
            }
        }
    }
}