namespace WorkerTrackingServer.Domain.DTOs;
public sealed record GetAllProductCountDto(
    Guid ProductId,
    string? Name,
    int? Count);
