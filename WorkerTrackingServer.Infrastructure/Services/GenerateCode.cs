using Microsoft.AspNetCore.Http.HttpResults;
using WorkerTrackingServer.Application.Services;

namespace WorkerTrackingServer.Infrastructure.Services;
internal class GenerateCode : IGenerateCode
{
    public int Generate6DigitCode(CancellationToken cancellationToken)
    {
        Random random = new();
        int code = random.Next(100000, 999999);
        return code;
    }

    public string GenerateWorkerCode(CancellationToken cancellationToken)
    {
        Random random = new();
        string code = random.Next(100000, 999999).ToString();
        return code;
    }
}
