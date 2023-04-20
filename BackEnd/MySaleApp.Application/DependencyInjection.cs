using Microsoft.Extensions.DependencyInjection;
using MySaleApp.Application.Services.Contract;
using MySaleApp.Application.Services;

namespace MySaleApp.Application
{
    public static class DependencyInjection
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPasswordHasService, PasswordHashService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
