using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace API.Helpers
{
    public class LogUserActivityActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext=await next();
            if(!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId=resultContext.HttpContext.User.GetUserId();
            var userRep=resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
            var user= await userRep.GetUserByUserIdAsync(userId);
            user.LastActive=DateTime.Now;
            await userRep.SaveAllAsync();
        }
    }
}