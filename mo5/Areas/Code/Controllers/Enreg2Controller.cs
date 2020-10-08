using MO5.Areas.Code.Models;
using MO5.Helpers;

namespace MO5.Areas.Code.Controllers
{
  public class Enreg2Controller : EnregController
  {
    public Enreg2Controller(IEnregRepository _enregRepository, IConfigurationProvider configProvider) : base(_enregRepository, configProvider, 1)
    {
    }
  }
}