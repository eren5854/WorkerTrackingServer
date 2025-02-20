using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Products.DeleteProductById;
public sealed record DeleteProductByIdCommand(Guid Id) : IRequest<Result<string>>;
