using Lime.Domain.Entities;
using Lime.Domain.Repositories;

namespace Lime.Data.EntityFramework.Repositories
{
    internal class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        internal OrderDetailRepository(Model context)
            : base(context)
        {
        }
    }
}
