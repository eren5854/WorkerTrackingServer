using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Departments;

namespace WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.GetAllDepartmentProductionByDepartmentId;
public sealed record GetAllDepartmentProductionByDepartmentIdCommand(Guid Id) : IRequest<Result<List<DepartmentProduction>>>;
