using ED.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using WorkerTrackingServer.Domain.Enums;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.WorkerRegister;
public sealed record WorkerRegisterCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Password,
    IFormFile? ProfilePicture,
    DateOnly? DateOfBirth,
    GenderSmartEnum? Gender,
    Guid? DepartmentId) : IRequest<Result<string>>;
