using ED.GenericRepository;
using WorkerTrackingServer.Domain.EmailSettings;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Infrastructure.Context;

namespace WorkerTrackingServer.Infrastructure.Repositories;
internal sealed class EmailSettingRepository : Repository<EmailSetting, ApplicationDbContext>, IEmailSettingRepository
{
    public EmailSettingRepository(ApplicationDbContext context) : base(context)
    {
    }
}
