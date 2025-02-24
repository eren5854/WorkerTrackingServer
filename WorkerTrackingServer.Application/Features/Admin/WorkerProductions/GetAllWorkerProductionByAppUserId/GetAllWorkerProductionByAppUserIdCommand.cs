﻿using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.WorkerProductions;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerProductions.GetAllWorkerProductionByAppUserId;
public sealed record GetAllWorkerProductionByAppUserIdCommand(Guid AppUserId) : IRequest<Result<List<WorkerProduction>>>;
