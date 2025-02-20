using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.UpdateMachineStatus;
public sealed record UpdateMachineStatusCommand(Guid Id) : IRequest<Result<string>>;
