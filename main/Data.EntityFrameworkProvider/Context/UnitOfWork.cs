using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using UIT.iDeal.Common;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Common.Types;

namespace UIT.iDeal.Data.EntityFrameworkProvider.Context
{
    public class UnitOfWork : Disposable, IUnitOfWork
    {
        private readonly DataContext _context;
        private IDbTransaction _transaction;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IDisposable BeginTransaction()
        {
            IObjectContextAdapter objectContextAdapter = _context;
            var connection = objectContextAdapter.ObjectContext.Connection;
            if ((connection.State &
                ConnectionState.Open) != ConnectionState.Open)
            {
                connection.Open();
            }
        
            return (_transaction = connection.BeginTransaction());
        }

       
        public void CommitChanges()
        {
            if (_context.ChangeTracker.Entries().Any(HasChanged))
            {
                _context.SaveChanges();
            }
        }

        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
            }
        }

        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
        }

        protected override void DisposeCore()
        {
            if (_context != null)
            {
                _context.Dispose();
            }

            if (_transaction != null)
            {
                _transaction.Dispose();

            }
            
        }

        private static bool HasChanged(DbEntityEntry entity)
        {
            return IsState(entity, EntityState.Added) ||
                   IsState(entity, EntityState.Deleted) ||
                   IsState(entity, EntityState.Modified);
        }

        private static bool IsState(DbEntityEntry entity, EntityState state)
        {
            return (entity.State & state) == state;
        }
    }
}
