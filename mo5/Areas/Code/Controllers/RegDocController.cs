using MO5.Areas.Code.Models;
using MO5.Controllers;
using MO5.Helpers;
using MO5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MO5.Areas.Code.Controllers
{
  public class RegDocController : BaseController
  {
    public IRegDocRepository regdocRepository;
    private readonly IConfigurationProvider _configProvider;

    public RegDocController(IRegDocRepository _regdocRepository, IConfigurationProvider configProvider)
    {
      regdocRepository = _regdocRepository;
      _configProvider = configProvider;
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult Index()
    {
      return View();
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult getRegDocList(int? OwnerID, string sort, string dir, DateTime? d1, DateTime? d2, int? type, Boolean? sd, bool? Direction)
    {
      return new JsonnResult { Data = new { success = true, data = regdocRepository.getRegDocList(OwnerID, sort ?? "Id", dir ?? "DESC", d1, d2, type, sd, Direction) } };
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult addRegDoc(List<tRegDoc> data)
    {
      return new JsonnResult { Data = new { success = true, data = regdocRepository.addRegDoc(data, User.Identity.Name) } };
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult updRegDoc(List<tRegDoc> data)
    {
      return new JsonnResult { Data = new { success = true, data = regdocRepository.updRegDoc(data, User.Identity.Name) } };
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult delRegDoc(List<tRegDoc> data)
    {
      return new JsonnResult { Data = new { success = true, data = regdocRepository.delRegDoc(data) } };
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult regdocCourriel(int? id)
    {
      var q = regdocRepository.regdocCourriel(id, (HttpContext.Request).Url.Authority);
      if (q != null)
      {
        SmtpClient sc = new SmtpClient();
        foreach (var i in q)
        {
          MailMessage message = new MailMessage();
          message.From = new MailAddress(_configProvider.GetValue<string>("EMailFrom"), "Внутренний контроль");
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
          message.Body = RenderViewToString(ControllerContext, "regdocCourriel", i);
          message.IsBodyHtml = true;
          message.Priority = MailPriority.High;
          message.Headers.Add("Importance", "High");
          message.Subject = "Поручения к исполнению";
          sc.Send(message);
        }
        return new JsonnResult { Data = new { success = true } };

      }
      return new JsonnResult { Data = new { success = false } };
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult getEMailList(string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = regdocRepository.getEMailList(sort, dir) } };
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult GetObjClsByParent(int id)
    {
      return new JsonnResult { Data = new { success = true, data = regdocRepository.GetObjClsByParent(id) } };
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult FileUploadI(int? Id, HttpPostedFileBase FileName)
    {
      if (FileName != null && FileName.ContentLength > 0)
      {
        var prefix = @"c:\data\RegDoc\In";
        var dir = Path.Combine(prefix, DateTime.Today.ToString("yy"));
        if (!Directory.Exists(dir))
          Directory.CreateDirectory(dir);
        var file = Path.Combine(DateTime.Today.ToString("yy"), string.Format("{0}_{1}", Id, Path.GetFileName(FileName.FileName)));
        var path = Path.Combine(prefix, file);
        if (System.IO.File.Exists(path))
          System.IO.File.Delete(path);
        FileName.SaveAs(path);
        return new JsonnResult { Data = new { success = true, message = "Сохранено", file = file }, ContentType = "text/html" };
      }
      return new JsonnResult { Data = new { success = false, message = "Нет файла" }, ContentType = "text/html" };
    }

    [Authorize]
    public ActionResult GetFileI(string data)
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
      var prefix = @"c:\data\RegDoc\In";
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

    [Authorize(Roles = "regdoc")]
    public ActionResult FileUploadO(int? Id, HttpPostedFileBase FileName)
    {
      if (FileName != null && FileName.ContentLength > 0)
      {
        var prefix = @"c:\data\RegDoc\Out";
        var dir = Path.Combine(prefix, DateTime.Today.ToString("yy"));
        if (!Directory.Exists(dir))
          Directory.CreateDirectory(dir);
        var file = Path.Combine(DateTime.Today.ToString("yy"), string.Format("{0}_{1}", Id, Path.GetFileName(FileName.FileName)));
        var path = Path.Combine(prefix, file);
        if (System.IO.File.Exists(path))
          System.IO.File.Delete(path);
        FileName.SaveAs(path);
        return new JsonnResult { Data = new { success = true, message = "Сохранено", file = file }, ContentType = "text/html" };
      }
      return new JsonnResult { Data = new { success = false, message = "Нет файла" }, ContentType = "text/html" };
    }

    [Authorize]
    public ActionResult GetFileO(string data)
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
      var prefix = @"c:\data\RegDoc\Out";
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

    [Authorize(Roles = "regdoc")]
    public ActionResult getNextRegNum()
    {
      return Content(regdocRepository.getNextRegNum1());
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult getRegDocContrList(string filter, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = regdocRepository.getRegDocContrList(filter, sort, dir) } };
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult addRegDocContr(List<tRegDocContr> data)
    {
      return new JsonnResult { Data = new { success = true, data = regdocRepository.addRegDocContr(data) } };
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult updRegDocContr(List<tRegDocContr> data)
    {
      return new JsonnResult { Data = new { success = true, data = regdocRepository.updRegDocContr(data) } };
    }

    [Authorize(Roles = "regdoc")]
    public ActionResult delRegDocContr(List<tRegDocContr> data)
    {
      return new JsonnResult { Data = new { success = true, data = regdocRepository.delRegDocContr(data) } };
    }

  }
}