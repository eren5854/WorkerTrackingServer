using WorkerTrackingServer.Application.Features.Auth.Login;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Services;
public interface IJwtProvider
{
    Task<LoginCommandResponse> CreateToken(AppUser user);
}