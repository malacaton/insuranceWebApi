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
  public class UsersController : ControllerBase
  {
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
      _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody]AuthenticateModel model)
    {
      var user = _userService.Authenticate(model.email);

      if (user == null)
        return BadRequest(new { message = "Bad username or password" });

      return Ok(user);
    }

    [AuthorizeRoles(Role.Admin, Role.User)]
    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
      var user = _userService.GetById(id);

      if (user == null)
        return NotFound();

      return Ok(user);
    }

    [AuthorizeRoles(Role.Admin, Role.User)]
    [HttpGet("name/{name}")]
    public IActionResult GetByName(string name)
    {
      var user = _userService.GetByName(name);

      if (user == null)
        return NotFound();

      return Ok(user);
    }

    [AuthorizeRoles(Role.Admin)]
    [HttpGet("policy/{id}")]
    public IActionResult GetUserByPolicy(string id)
    {
      var user = _userService.GetUserByPolicy(id);

      if (user == null)
        return NotFound();

      return Ok(user);
    }

  }
}
