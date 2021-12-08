using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Repositories;
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
             services.AddScoped<IPhotoService,PhotoService>();
             services.AddScoped<IUserRepository,UserRepository>();
             services.AddScoped<IMessageRepository,MessageRepository>();
             services.AddScoped<LogUserActivityActionFilter>();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(_conf.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}