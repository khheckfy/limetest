using Lime.Domain.Entities;
using Lime.Domain.Repositories;

namespace Lime.Data.EntityFramework.Repositories
{
    internal class OrderRepository : Repository<Order>, IOrderRepository
    {
        internal OrderRepository(Model context)
            : base(context)
        {
        }
    }
}
