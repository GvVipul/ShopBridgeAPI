
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShowBridge.Services.TokenService;

namespace ShowBridge.Attribute
{
    public class APIPermission : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly IAPITokenValidationService _apiTokenValidationLogic;

        public APIPermission(IAPITokenValidationService apiTokenValidationService)
        {
            this._apiTokenValidationLogic = apiTokenValidationService;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!this._apiTokenValidationLogic.IsValid(context.HttpContext.Request.Headers["X-BMM-ClientSecret"].ToString()))
            {
                context.Result = new UnauthorizedResult();
            }
            return;
        }
    }
}
