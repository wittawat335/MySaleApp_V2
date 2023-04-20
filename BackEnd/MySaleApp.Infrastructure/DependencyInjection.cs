using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySaleApp.Application.Repositories;
using MySaleApp.Application.Services;
using MySaleApp.Application.Services.Contract;
using MySaleApp.Infrastructure.Common;
using MySaleApp.Infrastructure.DBContext;
using MySaleApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaleApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(Constants.ConnnectionString.SqlServer);
            services.AddDbContext<SaledbV2Context>(opt => opt.UseSqlServer(connectionString));
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISaleRepository, SaleRepository>();           
        }
    }
}
