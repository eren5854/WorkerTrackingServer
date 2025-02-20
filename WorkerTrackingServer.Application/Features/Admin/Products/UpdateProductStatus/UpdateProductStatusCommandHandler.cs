using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Products;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Products.UpdateProductStatus;
internal sealed class UpdateProductStatusCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductStatusCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateProductStatusCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (product is null)
        {
            return Result<string>.Failure("Product not found");
        }

        product.IsActive = !product.IsActive;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Product status update successfully");
    }
}
