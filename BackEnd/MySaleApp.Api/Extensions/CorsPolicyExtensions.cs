using MySaleApp.Infrastructure.Common;

namespace MySaleApp.Api.Extensions
{
    public static class CorsPolicyExtensions
    {
        public static void ConfigureCorsPolicy(this IServiceCollection service, IConfiguration configuration)
        {
            var url = configuration.GetSection(Constants.AppSettings.Client_URL).Value!;
            service.AddCors(opt =>
            {
                opt.AddPolicy("newPolicy", builder =>
                    {
                        builder
                        .WithOrigins(url)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
        }
    }
}
