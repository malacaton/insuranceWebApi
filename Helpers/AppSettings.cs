using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Helpers
{
  public class AppSettings
  {
    public string Secret { get; set; }
    public string ClientsPath { get; set; }
    public string PoliciesPath { get; set; }

  }
}