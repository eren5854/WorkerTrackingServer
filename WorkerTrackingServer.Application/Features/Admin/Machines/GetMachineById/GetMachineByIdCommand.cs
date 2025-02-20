using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Machines;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.GetMachineById;
public sealed record GetMachineByIdCommand(Guid Id) : IRequest<Result<Machine>>;
