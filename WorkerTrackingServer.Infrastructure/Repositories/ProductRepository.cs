using ED.GenericRepository;
using WorkerTrackingServer.Domain.Products;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
public sealed class ProductRepository : Repository<Product, ApplicationDbContext>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }
}
