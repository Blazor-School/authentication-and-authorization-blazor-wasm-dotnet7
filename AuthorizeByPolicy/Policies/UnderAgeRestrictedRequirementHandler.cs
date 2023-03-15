using Microsoft.AspNetCore.Authorization;

namespace AuthorizeByPolicy.Policies;

public class UnderAgeRestrictedRequirementHandler : AuthorizationHandler<UnderAgeRestrictedRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UnderAgeRestrictedRequirement requirement)
    {
        int userAge = Convert.ToInt32(context.User.Claims.First(c => c.Type == "Age").Value);
        int minimumAgeRequired = Convert.ToInt32(context.Resource);

        if(userAge < minimumAgeRequired)
        {
            context.Fail(new (this, "User is underage"));
        }
        else
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
