using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Products.UpdateProduct;
public sealed record UpdateProductCommand(
    Guid Id,
    string ProductName,
    string? ProductCode,
    string? ProductDescription) :IRequest<Result<string>>;
