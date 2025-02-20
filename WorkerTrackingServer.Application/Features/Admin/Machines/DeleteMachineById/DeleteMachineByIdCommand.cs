using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.DeleteMachineById;
public sealed record DeleteMachineByIdCommand(Guid Id) : IRequest<Result<string>>;
