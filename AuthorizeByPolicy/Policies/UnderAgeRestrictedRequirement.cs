using Microsoft.AspNetCore.Authorization;

namespace AuthorizeByPolicy.Policies;

public class UnderAgeRestrictedRequirement : IAuthorizationRequirement
{
}