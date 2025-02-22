using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Workers;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerLogins.GetAllWorkerLogin;
public sealed record GetAllWorkerLoginQuery() : IRequest<Result<List<WorkerLogin>>>;
