using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Departments.CreateDepartment;
public sealed record CreateDepartmentCommand(
    string DepartmentName,
    string? DepartmentDescription) : IRequest<Result<string>>;
