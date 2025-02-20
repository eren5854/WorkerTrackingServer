using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Enums;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.UpdateWorker;
public sealed record UpdateWorkerCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string UserName,
    DateOnly? DateOfBirth,
    GenderSmartEnum? Gender,
    Guid? DepartmentId) : IRequest<Result<string>>;
