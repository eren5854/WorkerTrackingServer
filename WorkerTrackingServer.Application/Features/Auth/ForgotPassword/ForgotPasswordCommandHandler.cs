using ED.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Auth.ForgotPassword;
internal sealed class ForgotPasswordCommandHandler(
    UserManager<AppUser> userManager) : IRequestHandler<ForgotPasswordCommand, Result<string>>
{
    public async Task<Result<string>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Result<string>.Failure("User not found");
        }

        DateTime currentDateTime = DateTime.Now;
        if (user.ForgotPasswordCodeSendDate.HasValue && (currentDateTime - user.ForgotPasswordCodeSendDate.Value).TotalHours > 6)
        {
            return Result<string>.Failure("Code has expired, please try again.");
        }

        if (user.ForgotPasswordCode != request.ForgotPasswordCode)
        {
            return Result<string>.Failure("Wrong Code!!");
        }

        string token = await userManager.GeneratePasswordResetTokenAsync(user);
        return token;
    }
}
