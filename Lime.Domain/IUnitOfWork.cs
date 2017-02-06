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

        #endregion

        #region Methods
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion
    }
}
