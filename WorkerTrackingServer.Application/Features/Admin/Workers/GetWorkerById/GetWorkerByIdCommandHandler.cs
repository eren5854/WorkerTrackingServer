﻿using ED.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.Application.Features.Admin.Workers.GetWorkerById;
internal sealed class GetWorkerByIdCommandHandler(
    IAppUserRepository appUserRepository) : IRequestHandler<GetWorkerByIdCommand, Result<AppUser>>
{
    public async Task<Result<AppUser>> Handle(GetWorkerByIdCommand request, CancellationToken cancellationToken)
    {
        AppUser? appUser = await appUserRepository.Where(w => w.Id == request.Id).Include(i => i.Department).FirstOrDefaultAsync(cancellationToken);
        if (appUser is null)
        {
            return Result<AppUser>.Failure("Worker not found");
        }

        if(appUser.Role == 1 || appUser.Role == 2)
        {
            return Result<AppUser>.Failure("Unauthorized");
        }

        return Result<AppUser>.Succeed(appUser);
    }
}
