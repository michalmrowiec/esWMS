using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace esWMS.API.IntegrationTests.Helpers
{
    public class FakeUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.NameIdentifier, "1")
                ]));

            context.HttpContext.User = claimsPrincipal;

            await next();
        }
    }
}
