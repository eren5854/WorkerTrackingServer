﻿using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.CreateWorkerAssignment;
public sealed record CreateWorkerAssignmentCommand(
    Guid AppUserId,
    Guid MachineId,
    Guid WorkerProductionId,
    DateTime StartTime) : IRequest<Result<string>>;
