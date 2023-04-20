using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MySaleApp.Infrastructure.Common;
using System.Text;

namespace MySaleApp.Api.Extensions
{
    public static class JwtExtensions
    {
        public static void ConfigureJwtPolicy(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(configuration.GetSection(Constants.AppSettings.JWT_Key).Value!)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
