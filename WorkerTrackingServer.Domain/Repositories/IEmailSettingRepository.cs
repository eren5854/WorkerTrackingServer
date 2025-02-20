using ED.GenericRepository;
using WorkerTrackingServer.Domain.EmailSettings;

namespace WorkerTrackingServer.Domain.Repositories;
public interface IEmailSettingRepository : IRepository<EmailSetting>
{
}
