using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Models;
using InsuranceWebApi.Helpers;

namespace WebApi.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class PoliciesController : ControllerBase
  {
    private IPolicyService _policyService;

    public PoliciesController(IPolicyService policyService)
    {
      _policyService = policyService;
    }
    
    [AuthorizeRoles(Role.Admin)]
    [HttpGet("user/{name}")]
    public IActionResult GetPoliciesByUserName(string name)
    {
      var policies = _policyService.GetPoliciesByUserName(name);

      if (policies == null)
        return NotFound();

      return Ok(policies);
    }

  }
}
