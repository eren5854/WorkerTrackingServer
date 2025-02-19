using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Auth.Login;
public sealed record LoginCommand(
    string EmailOrUserName,
    string Password) : IRequest<Result<LoginCommandResponse>>;
