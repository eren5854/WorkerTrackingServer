using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Auth.RegisterAdmin;
public sealed record RegisterAdminCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password) : IRequest<Result<string>>;
