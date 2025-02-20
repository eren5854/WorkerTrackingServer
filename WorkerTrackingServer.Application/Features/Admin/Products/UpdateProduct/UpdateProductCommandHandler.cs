using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Products;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Products.UpdateProduct;
internal sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (product is null)
        {
            return Result<string>.Failure("Product not found");
        }

        bool isProductNameExists = await productRepository.AnyAsync(a => a.ProductName == request.ProductName, cancellationToken);
        if (isProductNameExists)
        {
            return Result<string>.Failure("Product Name already exists");
        }

        mapper.Map(request, product);
        product.UpdatedBy = "Admin";
        product.UpdatedDate = DateTime.Now;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Product updated successfully");
    }
}
