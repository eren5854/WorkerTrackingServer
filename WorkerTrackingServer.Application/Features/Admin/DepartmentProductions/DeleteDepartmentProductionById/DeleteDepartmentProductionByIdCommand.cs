using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.DepartmentProductions.DeleteDepartmentProductionById;
public sealed record DeleteDepartmentProductionByIdCommand(Guid Id) : IRequest<Result<string>>;
