using MO5.Areas.Code.Models;
using MO5.Controllers;
using MO5.Helpers;
using MO5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MO5.Areas.Code.Controllers
{
  public class EnregController : BaseController
  {
    private readonly IEnregRepository enregRepository;
    private readonly int EnregTypeID = 0;

    public EnregController(IEnregRepository _enregRepository)
    {
      enregRepository = _enregRepository;
    }

    public EnregController(IEnregRepository _enregRepository, int _enregTypeID)
    {
      enregRepository = _enregRepository;
      EnregTypeID = _enregTypeID;
    }

    [Authorize(Roles = "jrpk, jrpki, jrpkr")]
    public ActionResult Index()
    {
      return View("~/Areas/Code/Views/enreg/Index.cshtml");
    }

    [Authorize(Roles = "jrpk, jrpki, jrpkr")]
    public ActionResult getEnregList(DateTime? d1, DateTime? d2, Boolean? sd, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.GetEnregList(d1, d2, sd, User.Identity.Name, User.IsInRole("jrpkm"), EnregTypeID, sort, dir) } };
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult addEnreg(List<tEnregistrement> data)
    {
      //try
      {
        return new JsonnResult { Data = new { success = true, data = enregRepository.AddEnreg(data, EnregTypeID, User.Identity.Name) } };
      }
      //catch (Exception ex)
      //{
      //  return new JsonnResult { Data = new { success = false, Message = ex.Message, Message1 = ex.InnerException != null ? ex.InnerException.Message : "" } };
      //}
    }

    [Authorize(Roles = "jrpk, jrpkm")]
    public ActionResult updEnreg(List<tEnregistrement> data)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.UpdEnreg(data, User.Identity.Name, User.IsInRole("jrpkm"), User.IsInRole("Admin"), EnregTypeID) } };
    }

    [Authorize(Roles = "admin")]
    public ActionResult delEnreg(List<tEnregistrement> data)
    {
      return new JsonnResult { Data = new { success = enregRepository.DelEnreg(data, User.Identity.Name, User.IsInRole("jrpkm"), EnregTypeID) } };
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult getTreaties(string query, int? start, int? limit)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.getTreaties(query ?? "", limit ?? 10) } };
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult getTreatyList(string filter, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.GetTreatyList(filter, sort = "Name", dir) } };
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult getDocType(int id)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.GetDocType(id, EnregTypeID) } };
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult getObjClsByParent(int id)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.getObjClsByParent(id) } };
    }
    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult getMethod()
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.getObjClsByParent(27204) } };
    }
    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult getStatus()
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.getObjClsByParent(27203) } };
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult FUEnreg(int? ID, HttpPostedFileBase fn)
    {
      return FileUpload(ID, fn);
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult FUEnregO(int? ID, HttpPostedFileBase fn)
    {
      return FileUpload(ID, fn, "O");
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult FUEnregD(int? ID, HttpPostedFileBase fn)
    {
      return FileUpload(ID, fn, "D");
    }
    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult FUEnregG(int? ID, HttpPostedFileBase fn)
    {
      return FileUpload(ID, fn, "G");
    }
    private ActionResult FileUpload(int? ID, HttpPostedFileBase fn, string sdir = "")
    {
      if (fn != null && fn.ContentLength > 0)
      {
        var prefix = @"c:\data\Enreg";
        var dir = Path.Combine(prefix, sdir, DateTime.Today.ToString("yy"));
        if (!Directory.Exists(dir))
          Directory.CreateDirectory(dir);
        var file = Path.Combine(DateTime.Today.ToString("yy"), $"{ID}_{Path.GetFileName(fn.FileName)}");
        var path = Path.Combine(prefix, sdir, file);
        if (System.IO.File.Exists(path))
          System.IO.File.Delete(path);
        fn.SaveAs(path);
        return new JsonnResult { Data = new { success = true, message = "Сохранено", file }, ContentType = "text/html" };
      }
      return new JsonnResult { Data = new { success = false, message = "Нет файла" }, ContentType = "text/html" };
    }
    [Authorize]
    public ActionResult GetFile(string data)
    {
      return getFile(data);
    }

    [Authorize]
    public ActionResult GetFileO(string data)
    {
      return getFile(data, "O");
    }

    [Authorize]
    public ActionResult GetFileD(string data)
    {
      return getFile(data, "D");
    }

    [Authorize]
    public ActionResult GetFileG(int id)
    {
      var b = enregRepository.GetFileG(id, User.Identity.Name, User.IsInRole("controller"));
      if (!b.IsAuth)
      {
        return File(new byte[] { }, "application/text", "NotAllowed.txt");
        //return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
      }
      return getFile(b.FileName, "G");
    }
    private ActionResult getFile(string data, string dir = "")
    {
      string mimeType = "application/octet-stream";
      string ext = Path.GetExtension(data).ToLower();
      if (ext == ".zip")
        mimeType = "application/x-zip-compressed";
      else if (ext == ".rar")
        mimeType = "application/x-rar-compressed";
      else
      {
        Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (regKey != null && regKey.GetValue("Content Type") != null)
        {
          mimeType = regKey.GetValue("Content Type").ToString();
        }
      }
      var prefix = @"c:\data\Enreg";
      var path = Path.Combine(prefix, dir, data);
      if (!System.IO.File.Exists(path))
        return new HttpNotFoundResult("File not found");
      using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        BinaryReader reader = new BinaryReader(fs);
        Byte[] bytes = reader.ReadBytes(Convert.ToInt32(fs.Length));
        return File(bytes, mimeType, Path.GetFileName(data));
      }
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult link()
    {
      return View();
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult getUserList(int TypeID)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.GetUserList(TypeID) } };
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult delUser(List<UserList> data)
    {
      return new JsonnResult { Data = new { success = enregRepository.DelUser(data) } };
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult getUserTreaty(int id)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.GetUserTreaty(id) } };
    }
    [Authorize(Roles = "jrpk, jrpki, jrpkr")]
    public ActionResult getEnregStepLog(int id)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.GetEnregStepLog(id) } };
    }
    
    [Authorize(Roles = "jrpk, jrpki")]
    public class UserTreaty
    {
      public int id { get; set; }
      public bool InTreaty { get; set; }
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult setUserTreaty(List<UserTreaty> data, int id)
    {

      foreach (var t in data)
      {
        if (t.InTreaty)
        {
          var treaty = enregRepository.GetTreaty(t.id);
          if (treaty == null)
          {
            return new JsonnResult { Data = new { success = false, message = "Договор не найден" } };
          }
          if (!enregRepository.IsTreatyInUser(t.id, id))
            enregRepository.AddTreatyToUser(t.id, id);
        }
        else
        {
          if (enregRepository.IsTreatyInUser(t.id, id))
            enregRepository.RemoveTreatyFromUser(t.id, id);
        }
      }
      return new JsonnResult { Data = new { success = true, message = "Сохранено", data } };

    }

    [Authorize(Roles = "jrpk")]
    public ActionResult NotExecCourrier()
    {
      var q = enregRepository.GetNotExecEnreg(EnregTypeID);
      if (q.Count > 0)
      {
        SmtpClient sc = new SmtpClient();
        MailMessage message = new MailMessage();
        message.From = new MailAddress(ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
        message.To.Add(((HttpContext.Request).Url.Authority.Contains("localhost")) ? "qbcontrol@qbfin.ru" : "marina.volodina@qbfin.ru,oleg.timohin@qbfin.ru,backoffice@qbfin.ru,Dmitriy.Levin@qbfin.ru,vlada.bytkovskay@qbfin.ru,stanislav.matyukhin@qbfin.ru,maria.kopylova@qbfin.ru,midoffice@qbfin.ru,elena.sazonova@qbfin.ru,marina.palyan@qbfin.ru,anastasia.koval@qbfin.ru");
        //message.CC.Add("qbcontrol@qbfin.ru");
        message.Body = RenderViewToString(ControllerContext, "~/Areas/Code/Views/enreg/NotExecCourrier.cshtml", q);
        message.IsBodyHtml = true;
        message.Priority = MailPriority.High;
        message.Headers.Add("Importance", "High");
        message.IsBodyHtml = true;
        message.Subject = "Неисполненные поручения клиентов";
        try
        {
          sc.Send(message);
        }
        catch (Exception ex)
        {
          return new JsonnResult { Data = new { success = false, message = ex.Message } };
        }
      }
      return new JsonnResult { Data = new { success = true } };
    }

    [Authorize]
    public ActionResult getEMailList(string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.getEMailList(sort, dir) } };
    }

    [Authorize(Roles = "jrpk")]
    public ActionResult DTSteps()
    {
      return View("~/Areas/Code/Views/enreg/DTSteps.cshtml");
    }

    [Authorize(Roles = "jrpk")]
    public ActionResult getDTSteps(int id)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.GetDTSteps(id) } };
    }

    [Authorize(Roles = "jrpk")]
    public ActionResult addDTSteps(List<tEnregDTSteps> data)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.AddDTSteps(data) } };
    }

    [Authorize(Roles = "jrpk")]
    public ActionResult updDTSteps(List<tEnregDTSteps> data)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.UpdDTSteps(data, User.Identity.Name) } };
    }

    [Authorize(Roles = "jrpk")]
    public ActionResult delDTSteps(List<tEnregDTSteps> data)
    {
      return new JsonnResult { Data = new { success = enregRepository.DelDTSteps(data) } };
    }

    [Authorize(Roles = "jrpk, jrpkc")]
    public ActionResult enregConfirm(int id)
    {
      var stepId = enregRepository.GetEnregStepID(id, EnregTypeID);
      if (stepId == null)
      {
        stepId = enregRepository.AddZeroStep(id, EnregTypeID);
      }
      return Redirect(Url.Action("enrcs", new { id = stepId, area = "code" }));
    }

    [Authorize(Roles = "jrpk, jrpkc")]
    public ActionResult enrcs(Guid? id, int a = 1)
    {
      
      if (id.HasValue)
      {
        var es = enregRepository.GetEnregStep(id.Value);
        if (es != null)
        {
          if (es.Step == 0 && !es.IsConfirmed)
          {
            if (enregRepository.enregConfirm(id.Value, User.Identity.Name, EnregTypeID))
            {
              id = enregRepository.GetEnregStepID(es.EnregID, EnregTypeID);
              var res = enregRepository.enrCourriel(es.EnregID, Url.RouteUrl("Code_default", new { id }, Request.Url.Scheme), Url.RouteUrl("Code_default", new { action = "GetFile", id = "" }, Request.Url.Scheme), EnregTypeID);
              return View("~/Areas/Code/Views/enreg/Confirm.cshtml");
            }
          }
          var q = enregRepository.getEnreg(id.Value, EnregTypeID);
          return View("~/Areas/Code/Views/enreg/ChgStep.cshtml", q);
        }
      }
      return new HttpNotFoundResult();
    }

    [Authorize(Roles = "jrpk, jrpkc")]
    [HttpPost]
    public ActionResult enrcs(Guid id)
    {
      var es = enregRepository.GetEnregStep(id);
      if (es != null)
      {
        if (!es.IsConfirmed)
        {
          if (enregRepository.enregConfirm(id, User.Identity.Name, EnregTypeID))
          {
            var stepId = enregRepository.GetEnregStepID(es.EnregID, EnregTypeID);
            var res = enregRepository.enrCourriel(es.EnregID, Url.RouteUrl("Code_default", new { id = stepId }, Request.Url.Scheme), Url.RouteUrl("Code_default", new { action="GetFile", id = "" }, Request.Url.Scheme)/*(HttpContext.Request).Url.Authority*/, EnregTypeID);
          }
        }
        return View("~/Areas/Code/Views/enreg/Confirm.cshtml");
      }
      ModelState.AddModelError("", "Неверный указатель на поручение");
      var q = enregRepository.getEnreg(id, EnregTypeID);
      return View("~/Areas/Code/Views/enreg/ChgStep.cshtml", q);
    }

    [Authorize(Roles = "jrpk, jrpki")]
    public ActionResult getEnreLogList(int id)
    {
      return new JsonnResult { Data = new { success = true, data = enregRepository.GetEnreLog(id, EnregTypeID) } };
    }

  }
}