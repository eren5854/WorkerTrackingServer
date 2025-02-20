using ED.GenericEmailService.Models;
using ED.GenericEmailService;
using ED.GenericRepository;
using ED.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkerTrackingServer.Domain.Repositories;
using WorkerTrackingServer.Domain.Users;
using WorkerTrackingServer.Application.Services;
using WorkerTrackingServer.Domain.EmailSettings;

namespace WorkerTrackingServer.Application.Features.Auth.SendForgotPasswordEmail;
internal sealed class SendForgotPasswordEmailCommandHandler(
    UserManager<AppUser> userManager,
    IAppUserRepository appUserRepository,
    IGenerateCode generateCode,
    IEmailSettingRepository emailSettingRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<SendForgotPasswordEmailCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendForgotPasswordEmailCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Result<string>.Failure("User not found");
        }

        user.ForgotPasswordCode = generateCode.Generate6DigitCode(cancellationToken);  // 6 haneli kod oluştur
        user.ForgotPasswordCodeSendDate = DateTime.Now;

        appUserRepository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        EmailSetting emailSetting = await emailSettingRepository.GetByExpressionAsync(x => x.IsActive, cancellationToken);
        if (emailSetting is null)
        {
            return Result<string>.Failure("Email setting not found");
        }



        string body = CreateBody(user.ForgotPasswordCode, user.Email);
        string subject = "Şifremi Unuttum";

        EmailConfigurations emailConfigurations = new(
           emailSetting.SmtpDomainName,
           emailSetting.AppPassword,
           465,
           true,
           true);

        EmailModel<Stream> emailModel = new(
            emailConfigurations,
            emailSetting.Email,
            new List<string> { user.Email ?? "" },
            subject,
            body,
            null);

        await EmailService.SendEmailWithMailKitAsync(emailModel);

        return Result<string>.Succeed("Şifremi unuttum maili gönderildi");
    }

    private string CreateBody(int? code, string? email)
    {
        string body = $@"Click on the link below to change your password: 
<a href='https://alternatifmuhendis.com/forgot-password/{email}{code}' target='_blank'>https://alternatifmuhendis.com/forgot-password/{email}{code}
</a>";
        return body;
    }
}
