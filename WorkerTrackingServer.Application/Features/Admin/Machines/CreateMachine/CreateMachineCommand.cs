using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.CreateMachine;
public sealed record CreateMachineCommand(
    string? MachineName,
    string? MachineBrand,
    string? MachineModel,
    string? MachineSerialNumber,
    string? MachineType,
    string? MachineLocation,
    int? MachineNumber,
    string? MachineDescription) : IRequest<Result<string>>;
