using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Departments;

namespace WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.GetAllDepartmentProduction;
public sealed record GetAllDepartmentProductionQuery() : IRequest<Result<List<DepartmentProduction>>>;