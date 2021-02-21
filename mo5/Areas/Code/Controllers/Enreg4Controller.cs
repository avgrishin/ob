using MO5.Areas.Code.Models;
using MO5.Helpers;

namespace MO5.Areas.Code.Controllers
{
  public class Enreg4Controller : EnregController
  {
    public Enreg4Controller(IEnregRepository _enregRepository, IConfigurationProvider configProvider) : base(_enregRepository, configProvider, 3, "ЖРПК ВОССТАНОВЛЕННЫЙ")
    {
    }
  }
}