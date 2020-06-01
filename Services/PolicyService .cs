using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using WebApi.Models;
using WebApi.Helpers;

namespace WebApi.Services
{
  public interface IPolicyService
  {
    List<Policy> GetPoliciesByUserName(string name);
  }

  public class PolicyService : IPolicyService
  {
    private readonly AppSettings _appSettings;

    public PolicyService(IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings.Value;
    }
    
    public List<Policy> GetPoliciesByUserName(string name)
    {
      var client = Data.Users.FirstOrDefault(x => x.name == name);
      var clientId = client == null ? "" : client.id;

      var policies = Data.Policies
        .Where(x => x.clientId == clientId)
        .ToList();

      return policies;
    }
  }
}