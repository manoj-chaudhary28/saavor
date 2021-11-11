using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System; 
using System.Linq; 

namespace saavor.Web.Services
{
    public class CustomAuth : AuthorizeAttribute, IAuthorizationFilter
    {
        //private readonly UserManager<ApplicationUser> userManager;
        //public CustomAuth(UserManager<ApplicationUser> userManagerInstance)
        //{
        //    userManager = userManagerInstance;
        //}
        /// <summary>
        /// This method is used to as a entry point into action method 
        /// First this method is validate in header response had a token or not.
        /// ANd after that get the token and userid from claims and check this token into database.
        /// otherwise return 401 error or 403
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == Constants.Common.UserId);
            //var val = context.HttpContext.Request.Headers.Where(x => x.Key == Constants.Login.Authorization).FirstOrDefault();
            //if (val.Value.Count > 0)
            //{
            //    var token = val.Value[0].Replace(Constants.Login.Bearer, string.Empty);
            //    var userIdClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == Constants.Common.UserId);
            //    if (!UserTokenIsValid(userIdClaim, token))
            //    {
            //        context.Result = new UnauthorizedResult();
            //    }
            //}
        }
    }
}
