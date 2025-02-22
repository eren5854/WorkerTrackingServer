using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Worker.WorkerActualQuantity;
public sealed record WorkerActualQuantityCommand(
    Guid AppUserId,
    int ActualQuantity) : IRequest<Result<string>>;
