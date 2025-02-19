using Microsoft.AspNetCore.Identity;
using WorkerTrackingServer.Domain.Enums;
using WorkerTrackingServer.Domain.Users;

namespace WorkerTrackingServer.WebAPI.Middlewares;

public static class ExtensionsMiddleware
{
    public static void CreateAdmin(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if (!userManager.Users.Any(p => p.Email == "info@erendelibas.com"))
            {
                AppUser user = new()
                {
                    FirstName = "Eren",
                    LastName = "Delibaş",
                    UserName = "admin",
                    Email = "info@erendelibas.com",
                    Role = UserRoleSmartEnum.Admin,
                    EmailConfirmed = true,
                    IsDeleted = false,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                };

                userManager.CreateAsync(user, "Password123*").Wait();
            }
        }
    }
}
