using MySaleApp.Infrastructure.Common;

namespace MySaleApp.Api.Extensions
{
    public static class CorsPolicyExtensions
    {
        public static void ConfigureCorsPolicy(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddCors(opt =>
            {
                opt.AddPolicy("newPolicy", builder =>
                    {
                        builder
                        .WithOrigins(configuration.GetSection(Constants.AppSettings.Client_URL).Value!)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }
    }
}
