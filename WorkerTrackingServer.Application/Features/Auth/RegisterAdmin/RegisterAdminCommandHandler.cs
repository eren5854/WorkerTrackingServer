using AutoMapper;
using ED.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Enums;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Auth.RegisterAdmin;
internal sealed class RegisterAdminCommandHandler(
    UserManager<AppUser> userManager,
    IMapper mapper) : IRequestHandler<RegisterAdminCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
    {
        bool isEmailExists = await userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (isEmailExists)
        {
            return Result<string>.Failure("Email already exists");
        }

        bool isUserNameExists = await userManager.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken);
        if (isUserNameExists)
        {
            return Result<string>.Failure("Username already exists");
        }

        AppUser appUser = mapper.Map<AppUser>(request);
        appUser.CreatedBy = "MasterAdmin";
        appUser.CreatedDate = DateTime.Now;
        appUser.Role = UserRoleSmartEnum.Admin;

        IdentityResult result = await userManager.CreateAsync(appUser, request.Password);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("Failed to create admin");
        }

        return Result<string>.Succeed("Admin created successfully");
    }
}
