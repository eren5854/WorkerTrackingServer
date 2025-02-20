using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.UpdateMachine;
public sealed record UpdateMachineCommand(
    Guid Id,
    string? MachineName,
    string? MachineBrand,
    string? MachineModel,
    string? MachineSerialNumber,
    string? MachineType,
    string? MachineLocation,
    int? MachineNumber,
    string? MachineDescription) : IRequest<Result<string>>;
