using ED.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Auth.ChangePasswordUsingToken;
internal sealed class ChangePasswordUsingTokenCommandHandler(
    UserManager<AppUser> userManager) : IRequestHandler<ChangePasswordUsingTokenCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ChangePasswordUsingTokenCommand request, CancellationToken cancellationToken)
    {
        AppUser? appUser = await userManager.FindByEmailAsync(request.Email);
        if (appUser is null)
        {
            return Result<string>.Failure("User not found");
        }

        IdentityResult result = await userManager.ResetPasswordAsync(appUser, request.Token, request.NewPassword);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("Password reset failed");
        }

        return Result<string>.Succeed("Password reset successfully");
    }
}
