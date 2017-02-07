using Lime.Domain.Entities;
using Lime.Domain.Repositories;

namespace Lime.Data.EntityFramework.Repositories
{
    internal class ProductRepository: Repository<Product>, IProductRepository
    {
        internal ProductRepository(Model context)
            : base(context)
        {
        }
    }
}
