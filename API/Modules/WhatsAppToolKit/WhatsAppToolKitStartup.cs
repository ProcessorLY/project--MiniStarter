using API.Modules.WhatsAppToolKit.Interfaces;

namespace API.Modules.WhatsAppToolKit;

public static class WhatsAppToolKitStartup
{
    public static void WhatsAppToolKitInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IWhatsAppMessangerAsync, WhatsAppMessanger>();
        services.AddScoped<IWhatsAppClientAsync, SeleniumWhatsAppClient>();
    }
}
