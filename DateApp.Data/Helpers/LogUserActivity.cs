using DateApp.Data.Extensions;
using DateApp.Data.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DateApp.Data.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var username = resultContext.HttpContext.User.GetUsername();
            if (username == null) return;
            var uow = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
            if (uow == null) return;
            var user = await uow.UserRepository.GetUserByUsernameAsync(username);
            user.LastActive = DateTime.UtcNow;
            await uow.Complete();
        }
    }
}
