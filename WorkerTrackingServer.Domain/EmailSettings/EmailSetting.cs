using WorkerTrackingServer.Domain.Abstract;

namespace WorkerTrackingServer.Domain.EmailSettings;
public sealed class EmailSetting : Entity
{
    public string Email { get; set; } = default!;
    public string AppPassword { get; set; } = default!;
    public string SmtpDomainName { get; set; } = default!;
    public int SmtpPort { get; set; } = default!;
}
