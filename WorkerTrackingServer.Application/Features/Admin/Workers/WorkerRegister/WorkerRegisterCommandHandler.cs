using AutoMapper;
using ED.Result;
using GenericFileService.Files;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Application.Services;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.WorkerRegister;
internal sealed class WorkerRegisterCommandHandler(
    UserManager<AppUser> userManager,
    IMapper mapper,
    IGenerateCode generateCode) : IRequestHandler<WorkerRegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(WorkerRegisterCommand request, CancellationToken cancellationToken)
    {
        bool isUserNameExists = await userManager.Users.AnyAsync(x => x.UserName == request.UserName, cancellationToken);
        if (isUserNameExists)
        {
            return Result<string>.Failure("Username already exists");
        }

        string image = "";
        var response = request.ProfilePicture;
        if (response is null)
        {
            image = "";
        }
        if (response is not null)
        {
            image = FileService.FileSaveToServer(request.ProfilePicture!, "wwwroot/ProfilePictures/");
        }

        string password = "Password123*";

        AppUser appUser = mapper.Map<AppUser>(request);
        appUser.ProfilePicture = image;
        appUser.CreatedBy = "Admin";
        appUser.CreatedDate = DateTime.Now;
        appUser.Email = $"{request.UserName}@gmail.com";
        appUser.EmailConfirmed = true;

        string workerCode;
        do
        {
            workerCode = generateCode.GenerateWorkerCode(cancellationToken);
        } while (await userManager.Users.AnyAsync(x => x.WorkerCode == workerCode, cancellationToken));

        appUser.WorkerCode = workerCode;

        IdentityResult result = await userManager.CreateAsync(appUser, password);
        if (!result.Succeeded)
        {
            return Result<string>.Failure("Worker not created");
        }

        return Result<string>.Succeed("Worker created successfully");
    }
}
