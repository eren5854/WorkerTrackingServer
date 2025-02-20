using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Products;

namespace WorkerTrackingServer.Application.Features.Admin.Products.GetProductById;
public sealed record GetProductByIdCommand(Guid Id) : IRequest<Result<Product>>;
