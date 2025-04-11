using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningHubAPI.Identity;

[AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
public class IdentityRequiresClaims: Attribute,IAuthorizationFilter
{

    private readonly string _claimName;
    private readonly string[] _claimvalue;

    public IdentityRequiresClaims(string claimName, string[] claimValue)
    {
        _claimName = claimName;
        _claimvalue = claimValue;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!_claimvalue.Any(v=>user.HasClaim(_claimName,v)))
        {
            context.Result= new ForbidResult("not allowed to access");

        }

     
    }
}