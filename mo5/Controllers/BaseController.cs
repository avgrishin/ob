using System.IO;
using System.Web.Mvc;

namespace MO5.Controllers
{
  public class BaseController : Controller
  {
    public static string RenderViewToString(ControllerContext context, string viewPath, object model = null, bool partial = false)
    {
      ViewEngineResult viewEngineResult = null;
      if (partial)
        viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
      else
        viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

      if (viewEngineResult == null)
        throw new FileNotFoundException("View cannot be found.");

      var view = viewEngineResult.View;
      context.Controller.ViewData.Model = model;
      string result = null;
      using (var sw = new StringWriter())
      {
        var ctx = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
        view.Render(ctx, sw);
        result = sw.ToString();
      }
      return result;
    }
  }
}