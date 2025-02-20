using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Products;

namespace WorkerTrackingServer.Application.Features.Admin.Products.GetAllProduct;
public sealed record GetAllProductQuery() : IRequest<Result<List<Product>>>;
