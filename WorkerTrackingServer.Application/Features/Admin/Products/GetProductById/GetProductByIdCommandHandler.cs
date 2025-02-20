using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Products;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Products.GetProductById;
internal sealed class GetProductByIdCommandHandler(
    IProductRepository productRepository) : IRequestHandler<GetProductByIdCommand, Result<Product>>
{
    public async Task<Result<Product>> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (product is null)
        {
            return Result<Product>.Failure("Product not found");
        }
        return Result<Product>.Succeed(product);
    }
}
