using API.Modules.WhatsAppToolKit;

namespace API.Modules
{
    public static class ModulesStartups
    {
        public static async void ModulesInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //  WhatsAppToolKit
            await services.WhatsAppToolKitInjection(configuration);
        }
    }
}
