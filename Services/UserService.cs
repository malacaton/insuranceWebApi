using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;
using WebApi.Helpers;

namespace WebApi.Services
{
  public interface IUserService
  {
    UserAuthenticated Authenticate(string email);
    IEnumerable<UserToList> GetAll();
    User GetById(string id);
    User GetByName(string name);
    User GetUserByPolicy(string id);
  }

  public class UserService : IUserService
  {
    private readonly AppSettings _appSettings;

    public UserService(IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings.Value;
    }

    public UserAuthenticated Authenticate(string email)
    {
      var user = Data.Users.SingleOrDefault(x => x.email == email);

      if (user == null)
          return null;

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[] 
        {
          new Claim(ClaimTypes.Name, user.id.ToString()),
          new Claim(ClaimTypes.Role, user.role)
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var theToken = tokenHandler.CreateToken(tokenDescriptor);
      string token = tokenHandler.WriteToken(theToken);

      return new UserAuthenticated
      {
        id = user.id,
        name = user.name,
        role = user.role,
        token = token
      };

    }

    public IEnumerable<UserToList> GetAll()
    {
      return Data.Users
        .OrderBy(x => x.name)
        .Select(x => new UserToList
        {
          name = x.name,
          email = x.email
        });
    }

    public User GetById(string id)
    {
      var user = Data.Users.FirstOrDefault(x => x.id == id);
      return user;
    }

    public User GetByName(string name)
    {
      var user = Data.Users.FirstOrDefault(x => x.name == name);
      return user;
    }

    public User GetUserByPolicy(string id)
    {
      var policy = Data.Policies.FirstOrDefault(x => x.id == id);
      var clientId = policy == null ? "" : policy.clientId;

      return clientId == null ? null : GetById(clientId);
    }
  }
}