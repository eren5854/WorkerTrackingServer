using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Departments;

namespace WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.GetDepartmentProductionById;
public sealed record GetDepartmentProductionByIdCommand(Guid Id) : IRequest<Result<DepartmentProduction>>;
