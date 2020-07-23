using MO5.Areas.Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MO5.Areas.Code.Controllers
{
  public class Enreg2Controller : EnregController
  {
    public Enreg2Controller(IEnregRepository _enregRepository) : base(_enregRepository, 1)
    {
    }
  }
}