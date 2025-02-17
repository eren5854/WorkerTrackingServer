using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace WorkerTrackingServer.Infrastructure.Options;
public sealed class JwtOptionSetup(
    IConfiguration configuration) : IConfigureOptions<JwtOption>
{
    public void Configure(JwtOption options)
    {
        configuration.GetSection("Jwt").Bind(options);
    }
}
