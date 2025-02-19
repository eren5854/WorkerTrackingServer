using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Auth.ChangePasswordUsingToken;
public sealed record ChangePasswordUsingTokenCommand(
    string Email,
    string NewPassword,
    string Token) : IRequest<Result<string>>;
