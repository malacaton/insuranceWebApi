using Microsoft.AspNetCore.Authorization;

namespace InsuranceWebApi.Helpers
{
  public class AuthorizeRolesAttribute : AuthorizeAttribute
  {
    public AuthorizeRolesAttribute(params string[] roles) : base()
    {
      Roles = string.Join(",", roles);
    }
  }
}