using API.Data;
using API.Helpers;
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
             services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

             services.AddScoped<ITokenService, TokenService>();
             services.AddScoped<IUserRepository,UserRepository>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(_conf.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}