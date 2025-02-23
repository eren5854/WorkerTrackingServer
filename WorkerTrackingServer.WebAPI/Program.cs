using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using WorkerTrackingServer.Application;
using WorkerTrackingServer.Infrastructure;
using WorkerTrackingServer.WebAPI.BackgroundServices;
using WorkerTrackingServer.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddHangfireServer();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});
//builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapOpenApi();
}

app.UseCors();

ExtensionsMiddleware.CreateAdmin(app);
ExtensionsMiddleware.CreateMasterAdmin(app);

app.UseHangfireDashboard();

//RecurringJob.AddOrUpdate<WorkerProductionBackgroundService>(x => x.WorkerProductionWeekly(), Cron.Daily());

// Her g�n 00:00 - 00:59 aras�nda her 10 dakikada bir �al��t�r
for (int i = 0; i < 6; i++)
{
    RecurringJob.AddOrUpdate<WorkerAssignmentBackgroundService>(
        $"WorkerAssignmentReset_{i}",
        x => x.WorkerAssignmentReset(),
        $"0 {i * 10}-59 0 * * *"
    );
}

// Sunucu ba�lat�ld���nda e�er saat 00:00 - 01:00 aras�ndaysa �al��t�r
if (DateTime.Now.Hour == 0)
{
    BackgroundJob.Enqueue<WorkerAssignmentBackgroundService>(x => x.WorkerAssignmentReset());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
