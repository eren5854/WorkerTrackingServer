using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Departments;

namespace WorkerTrackingServer.Application.Features.Admin.Departments.GetDepartmentById;
public sealed record GetDepartmentByIdCommand(Guid Id) : IRequest<Result<Department>>;
