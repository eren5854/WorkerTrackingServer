using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Products;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Products.CreateProduct;
internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool isProductNameExists = await productRepository.AnyAsync(a => a.ProductName == request.ProductName,cancellationToken);
        if (isProductNameExists)
        {
            return Result<string>.Failure("Product Name already exists");
        }

        Product product = mapper.Map<Product>(request);
        product.CreatedBy = "Admin";
        product.CreatedDate = DateTime.Now;

        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Product created successfully");
    }
}
