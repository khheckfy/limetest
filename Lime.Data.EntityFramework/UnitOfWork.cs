using Lime.Data.EntityFramework.Repositories;
using Lime.Domain;
using Lime.Domain.Repositories;
using System;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace Lime.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly Model _context;

        private IOrderRepository _orderRepository;

        #endregion

        #region Constructors
        public UnitOfWork()
        {
            _context = new Model();
        }
        #endregion

        #region IUnitOfWork Members

        public IOrderRepository OrderRepository
        {
            get { return _orderRepository ?? (_orderRepository = new OrderRepository(_context)); }
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _orderRepository = null;

            _context.Dispose();
        }

        #endregion
    }
}