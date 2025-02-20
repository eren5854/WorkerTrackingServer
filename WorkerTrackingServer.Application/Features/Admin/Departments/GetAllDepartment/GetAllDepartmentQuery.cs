using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Departments;

namespace WorkerTrackingServer.Application.Features.Admin.Departments.GetAllDepartment;
public sealed record GetAllDepartmentQuery() : IRequest<Result<List<Department>>>;
