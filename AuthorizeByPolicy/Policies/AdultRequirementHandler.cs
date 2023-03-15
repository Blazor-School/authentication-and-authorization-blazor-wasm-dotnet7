using Microsoft.AspNetCore.Authorization;

namespace AuthorizeByPolicy.Policies;

public class AdultRequirementHandler : AuthorizationHandler<AdultRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdultRequirement requirement)
    {
        int userAge = Convert.ToInt32(context.User.Claims.First(c => c.Type == "Age").Value);

        if(userAge >= requirement.MinimumAgeToConsiderAnAdult)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
