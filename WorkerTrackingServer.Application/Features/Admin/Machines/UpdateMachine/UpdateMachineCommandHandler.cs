using AutoMapper;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using WorkerTrackingServer.Domain.Machines;
using WorkerTrackingServer.Domain.Repositories;

namespace WorkerTrackingServer.Application.Features.Admin.Machines.UpdateMachine;
internal sealed class UpdateMachineCommandHandler(
    IMachineRepository machineRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateMachineCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateMachineCommand request, CancellationToken cancellationToken)
    {
        Machine machine = await machineRepository.GetByExpressionAsync(g => g.Id == request.Id, cancellationToken);
        if (machine is null)
        {
            return Result<string>.Failure("Machine not found");
        }

        bool isMachineNumberExists = await machineRepository.AnyAsync(a => a.MachineNumber == request.MachineNumber, cancellationToken);
        if (isMachineNumberExists)
        {
            return Result<string>.Failure("Machine Number already exists!");
        }

        bool isMachineNameIsExists = await machineRepository.AnyAsync(a => a.MachineName == request.MachineName, cancellationToken);
        if (isMachineNameIsExists)
        {
            return Result<string>.Failure("Machine Name already exists");
        }

        mapper.Map(request, machine);
        machine.UpdatedBy = "Admin";
        machine.UpdatedDate = DateTime.Now;

        machineRepository.Update(machine);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Machine updated successfully");
    }
}
