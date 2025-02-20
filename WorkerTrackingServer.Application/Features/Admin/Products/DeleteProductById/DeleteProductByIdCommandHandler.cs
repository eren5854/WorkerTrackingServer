using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Products;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Products.DeleteProductById;
internal sealed class DeleteProductByIdCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (product is null)
        {
            return Result<string>.Failure("Product not found");
        }

        product.IsDeleted = true;
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Product delete successfully");
    }
}
