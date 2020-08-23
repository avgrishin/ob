using MO5.Areas.Code.Models;
using MO5.Controllers;
using MO5.Helpers;
using MO5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MO5.Areas.Code.Controllers
{
  [Authorize(Roles = "jur, jurv")]
  public class JurController : BaseController
  {
    const string prefixJ = @"c:\data\jur";
    const string prefixR = @"c:\data\reestr";
    const string prefixE = @"c:\data\edo";

    public IJurRepository jurRepository;

    public JurController(IJurRepository _jurRepository)
    {
      jurRepository = _jurRepository;
    }

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult getWarrantList(string sort, string dir, DateTime? d1, DateTime? d2, int? type, bool? all)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.getWarrantList(sort, dir, d1, d2, type, all) } };
    }

    public ActionResult addWarrant(List<tWarrant> data)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.addWarrant(data, User.Identity.Name) } };
    }

    public ActionResult updWarrant(List<tWarrant> data)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.updWarrant(data, User.Identity.Name) } };
    }

    public ActionResult delWarrant(List<tWarrant> data)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.delWarrant(data) } };
    }

    public ActionResult warrantCourriel()
    {
      var q = jurRepository.warrantCourriel();
      if (q.Count > 0)
      {
        SmtpClient sc = new SmtpClient();
        MailMessage message = new MailMessage();
        message.From = new MailAddress(ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
        message.To.Add(((HttpContext.Request).Url.Authority.Contains("localhost")) ? "qbcontrol@qbfin.ru" : string.Join(",", jurRepository.getJuristList()));
        message.Body = RenderViewToString(ControllerContext, "warrantCourriel", q);
        message.IsBodyHtml = true;
        message.Priority = MailPriority.High;
        message.Headers.Add("Importance", "High");
        message.IsBodyHtml = true;
        message.Subject = "Доверенности, истекающие через неделю и менее";
        try
        {
          sc.Send(message);
        }
        catch
        {
          return new JsonnResult { Data = new { success = false } };
        }
      }
      return new JsonnResult { Data = new { success = true } };
    }

    public ActionResult confirmCloseWarrant(int? ID)
    {
      var q = jurRepository.getWarrant(ID);
      return View(q);
    }

    //[Authorize(Roles = "jur")]
    [HttpPost]
    public ActionResult closeWarrant(int? ID)
    {
      var q = jurRepository.closeWarrant(ID, User.Identity.Name);
      if (q)
      {
        return View();
      }
      return View("errorCloseWarrant");
    }

    private ActionResult FileUpload(int? id, HttpPostedFileBase fn, string prefix)
    {
      if (fn != null && fn.ContentLength > 0)
      {
        var dir = Path.Combine(prefix, DateTime.Today.ToString("yy"));
        if (!Directory.Exists(dir))
          Directory.CreateDirectory(dir);
        var file = Path.Combine(DateTime.Today.ToString("yy"), string.Format("{0}_{1}", id, Path.GetFileName(fn.FileName)));
        var path = Path.Combine(prefix, file);
        if (System.IO.File.Exists(path))
          System.IO.File.Delete(path);
        fn.SaveAs(path);
        return new JsonnResult { Data = new { success = true, message = "Сохранено", file = file }, ContentType = "text/html" };
      }
      return new JsonnResult { Data = new { success = false, message = "Нет файла" }, ContentType = "text/html" };
    }

    public ActionResult FileUploadJ(int? id, HttpPostedFileBase fn)
    {
      return FileUpload(id, fn, prefixJ);
    }

    private ActionResult GetFile(string data, string prefix)
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
      var path = Path.Combine(prefix, data);
      if (!System.IO.File.Exists(path))
        return new HttpNotFoundResult("File not found");
      using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
      {
        BinaryReader reader = new BinaryReader(fs);
        Byte[] bytes = reader.ReadBytes(Convert.ToInt32(fs.Length));

        return File(bytes, mimeType, Path.GetFileName(data));
      }
    }

    public ActionResult GetFileJ(string data)
    {
      return GetFile(data, prefixJ);
    }

    public ActionResult Reestr()
    {
      return View();
    }

    public ActionResult getReestrList(string sort, string dir, DateTime? d1, DateTime? d2, int? InstOwnerID)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.GetReestrList(sort, dir, d1, d2, InstOwnerID) } };
    }

    public ActionResult addReestr(List<tReestr> data)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.AddReestr(data) } };
    }

    public ActionResult updReestr(List<tReestr> data)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.UpdReestr(data) } };
    }

    public ActionResult delReestr(List<tReestr> data)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.DelReestr(data) } };
    }

    public ActionResult getEMailList(string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.getEMailList(sort, dir) } };
    }

    public ActionResult GetCmp()
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.GetObjClsByParent(26186) } };
    }

    public ActionResult FileUploadR(int? id, HttpPostedFileBase fn)
    {
      return FileUpload(id, fn, prefixR);
    }

    public ActionResult GetFileR(string data)
    {
      return GetFile(data, prefixR);
    }

    public ActionResult Edo()
    {
      return View();
    }

    public ActionResult getEdoList(string sort, string dir, DateTime? d1, DateTime? d2)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.GetEdoList(sort, dir, d1, d2) } };
    }

    public ActionResult addEdo(List<tEDO> data)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.AddEdo(data, User.Identity.Name) } };
    }

    public ActionResult updEdo(List<tEDO> data)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.UpdEdo(data, User.Identity.Name) } };
    }

    public ActionResult delEdo(List<tEDO> data)
    {
      return new JsonnResult { Data = new { success = true, data = jurRepository.DelEdo(data) } };
    }

    public ActionResult edoCourriel()
    {
      var q = jurRepository.EdoCourriel();
      if (q != null)
      {
        SmtpClient sc = new SmtpClient();
        foreach (var i in q)
        {
          MailMessage message = new MailMessage();
          message.From = new MailAddress(ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
          if ((HttpContext.Request).Url.Authority.Contains("localhost") || i.Key == null)
          {
            message.To.Add("qbcontrol@qbfin.ru");
          }
          else
          {
            message.To.Add(i.Key);
          }
          ViewBag.email = i.Key;
          ViewBag.host = (HttpContext.Request).Url.Authority;
          message.Body = RenderViewToString(ControllerContext, "EdoCourriel", i);
          message.IsBodyHtml = true;
          message.Priority = MailPriority.High;
          message.Headers.Add("Importance", "High");
          message.IsBodyHtml = true;
          message.Subject = "Напоминание о сроках действия сертификатов";
          sc.Send(message);
        }
        return new JsonnResult { Data = new { success = true } };
      }
      return new JsonnResult { Data = new { success = false } };
    }

    public ActionResult GetFileE(string data)
    {
      return GetFile(data, prefixE);
    }

    public ActionResult FileUploadE(int? id, HttpPostedFileBase fn)
    {
      return FileUpload(id, fn, prefixE);
    }

    public ActionResult edoEmail(int id)
    {
      var q = jurRepository.GetEDO(id);
      if (q != null)
      {
        SmtpClient sc = new SmtpClient();
        MailMessage message = new MailMessage();
        message.From = new MailAddress(ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
        if ((HttpContext.Request).Url.Authority.Contains("localhost") || q.EmailTo == null)
          message.To.Add("qbcontrol@qbfin.ru");
        else
          message.To.Add(q.EmailTo);
        ViewBag.email = q.EmailTo;
        message.Body = RenderViewToString(ControllerContext, "edoEmail", q);
        message.IsBodyHtml = true;
        message.Priority = MailPriority.High;
        message.Headers.Add("Importance", "High");
        message.IsBodyHtml = true;
        message.Subject = "Сертификат";
        sc.Send(message);
        return new JsonnResult { Data = new { success = true } };
      }
      return new JsonnResult { Data = new { success = false } };
    }

    public ActionResult reestrEmail(int id)
    {
      var q = jurRepository.GetReestr(id);
      if (q != null)
      {
        SmtpClient sc = new SmtpClient();
        MailMessage message = new MailMessage();
        message.From = new MailAddress(ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
        if ((HttpContext.Request).Url.Authority.Contains("localhost") || q.EmailTo == null)
          message.To.Add("qbcontrol@qbfin.ru");
        else
          message.To.Add(q.EmailTo);
        ViewBag.email = q.EmailTo;
        ViewBag.host = (HttpContext.Request).Url.Authority;
        message.Body = RenderViewToString(ControllerContext, "reestrEmail", q);
        message.IsBodyHtml = true;
        message.Priority = MailPriority.High;
        message.Headers.Add("Importance", "High");
        message.IsBodyHtml = true;
        message.Subject = "Реестр";
        sc.Send(message);
        return new JsonnResult { Data = new { success = true } };
      }
      return new JsonnResult { Data = new { success = false } };
    }

  }
}

