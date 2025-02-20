using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.Departments.DeleteDepartmentById;
public sealed record DeleteDepartmentByIdCommand(
    Guid Id) : IRequest<Result<string>>;
