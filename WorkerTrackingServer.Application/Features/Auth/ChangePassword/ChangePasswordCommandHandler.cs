using ED.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Auth.ChangePassword;
internal sealed class ChangePasswordCommandHandler(
    UserManager<AppUser> userManager) : IRequestHandler<ChangePasswordCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        AppUser? appUser = await userManager.FindByIdAsync(request.Id.ToString());
        if (appUser is null)
        {
            return Result<string>.Failure("User not found");
        }

        if(request.CurrentPassword == request.NewPassword)
        {
            return Result<string>.Failure("New password cannot be the same as the current password");
        }

        IdentityResult result = await userManager.ChangePasswordAsync(appUser, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("Password change failed");
        }

        return Result<string>.Succeed("Password changed successfully");
    }
}
