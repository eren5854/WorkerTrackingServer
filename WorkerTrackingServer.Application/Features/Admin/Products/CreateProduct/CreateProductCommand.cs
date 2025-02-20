using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Products.CreateProduct;
public sealed record CreateProductCommand(
    string ProductName,
    string? ProductCode,
    string? ProductDescription) : IRequest<Result<string>>;
