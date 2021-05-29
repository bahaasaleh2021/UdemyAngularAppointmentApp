using API.Data;
using API.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,IConfiguration _conf)
        {
             services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(_conf.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}