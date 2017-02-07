using Lime.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Lime.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        #region Properties
        
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IProductRepository ProductRepository { get; }

        #endregion

        #region Methods

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
        #endregion
    }
}
