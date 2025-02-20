using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Departments.UpdateDepartment;
public sealed record UpdateDepartmentCommand(
    Guid Id,
    string DepartmentName,
    string? DepartmentDescription) : IRequest<Result<string>>;
