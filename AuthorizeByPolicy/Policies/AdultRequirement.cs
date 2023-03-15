using Microsoft.AspNetCore.Authorization;

namespace AuthorizeByPolicy.Policies;

public class AdultRequirement : IAuthorizationRequirement
{
    public int MinimumAgeToConsiderAnAdult { get; set; } = 18;
}