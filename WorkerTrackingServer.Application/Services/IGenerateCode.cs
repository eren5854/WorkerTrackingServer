namespace WorkerTrackingServer.Application.Services;
public interface IGenerateCode
{
    public int Generate6DigitCode(CancellationToken cancellationToken);
}
