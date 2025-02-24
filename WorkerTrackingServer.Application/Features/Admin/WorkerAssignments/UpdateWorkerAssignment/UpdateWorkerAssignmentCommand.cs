﻿using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Admin.WorkerAssignments.UpdateWorkerAssignment;
public sealed record UpdateWorkerAssignmentCommand(
    Guid Id,
    Guid AppUserId,
    Guid MachineId,
    Guid WorkerProductionId,
    DateTime StartTime) : IRequest<Result<string>>;
