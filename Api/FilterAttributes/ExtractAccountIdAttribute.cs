using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Api.FilterAttributes
{
    public class ExtractAccountIdAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var accountIdClaim = context.HttpContext.User.Claims
                .Where(c => c.Type.Equals(ClaimTypes.NameIdentifier))
                .FirstOrDefault();
            if (accountIdClaim is null)
                throw new InvalidOperationException("there is no accountId's claim");
            context.ActionArguments.Add("accountId", accountIdClaim.Value);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
