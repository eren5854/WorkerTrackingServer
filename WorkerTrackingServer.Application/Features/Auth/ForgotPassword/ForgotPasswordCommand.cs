using ED.Result;
using MediatR;

namespace WorkerTrackingServer.Application.Features.Auth.ForgotPassword;
public sealed record ForgotPasswordCommand(
    string Email,
    int ForgotPasswordCode) : IRequest<Result<string>>;
