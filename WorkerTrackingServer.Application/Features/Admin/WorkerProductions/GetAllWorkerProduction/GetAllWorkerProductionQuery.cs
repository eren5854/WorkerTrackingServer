using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.DTOs;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.GetAllWorkerProduction;
public sealed record GetAllWorkerProductionQuery() : IRequest<Result<List<GetAllWorkerProductionDto>>>;
