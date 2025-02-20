using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Products.UpdateProductStatus;
public sealed record UpdateProductStatusCommand(Guid Id) :  IRequest<Result<string>>;
