using ED.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using WorkerTrackingServer.Domain.Enums;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.UpdateWorker;
public sealed record UpdateWorkerCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string UserName,
    IFormFile? ProfilePicture,
    DateOnly? DateOfBirth,
    GenderSmartEnum? Gender,
    Guid? DepartmentId) : IRequest<Result<string>>;
