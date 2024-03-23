using API.Modules.WhatsAppToolKit;

namespace API.Modules
{
    public static class ModulesStartups
    {
        public static void ModulesInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //  WhatsAppToolKit
            services.WhatsAppToolKitInjection(configuration);

        }
    }
}
