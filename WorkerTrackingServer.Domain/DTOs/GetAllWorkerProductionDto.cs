namespace WorkerTrackingServer.Domain.DTOs;
public sealed record GetAllWorkerProductionDto(
    Guid? AppUserId,
    string? FullName,
    int? Total);
