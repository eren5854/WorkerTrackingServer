using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Machines;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.GetAllMachine;
public sealed record GetAllMachineQuery():IRequest<Result<List<Machine>>>;
