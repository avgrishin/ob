using ClosedXML.Excel;
using MO5.Helpers;
using MO5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace MO5.Controllers
{
  public class HomeController : BaseController
  {
    public IHomeRepository homeRepository;

    public HomeController(IHomeRepository _homeRepository)
    {
      homeRepository = _homeRepository;
    }

    public ActionResult Index()
    {
      //Membership.CreateUser("FC\\GrishinAV", "1", "");
      //Roles.AddUserToRole("FC\\GrishinAV", "envoi");

      //var f = homeRepository.getWorkDatesList(DateTime.Today.Year).ToList();
      return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";
      var s = RenderViewToString(ControllerContext, "~/views/home/about.cshtml");
      return Content(s);
    }

    [Authorize(Roles = "admin")]
    public ActionResult WorkDate()
    {
      return View();
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public ActionResult getWorkDatesList(int? year)
    {
      return new JsonnResult { Data = new { success = true, data = homeRepository.getWorkDatesList(year ?? DateTime.Today.Year) } };
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public ActionResult updWorkDate(List<WorkDateList> data)
    {
      return new JsonnResult { Data = new { success = true, data = homeRepository.updWorkDate(data) } };
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public ActionResult setupWorkDates(int? year)
    {
      if (!year.HasValue)
        return new JsonnResult { Data = new { success = false, message = "Не указан год" } };
      return new JsonnResult { Data = new { success = homeRepository.setupWorkDates(year.Value) } };
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    [ValidateInput(false)]
    public ActionResult ExportExcel(string f, string d)
    {
      string mimeType = "application/octet-stream";
      string ext = Path.GetExtension(f).ToLower();
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
      if (ext == ".csv")
        return File(System.Text.Encoding.GetEncoding(1251).GetBytes(d), mimeType, f);
      else
        return File(System.Text.Encoding.UTF8.GetBytes(d), mimeType, f);
    }

    public ActionResult EMail()
    {
      SmtpClient sc = new SmtpClient();
      MailMessage message = new MailMessage();
      message.From = new MailAddress(ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
      message.To.Add("qbcontrol@qbfin.ru");
      message.Body = "Test";
      message.IsBodyHtml = false;
      message.Priority = MailPriority.High;
      message.Headers.Add("Importance", "High");
      message.IsBodyHtml = true;
      message.Subject = "Test";
      sc.Send(message);

      return View("About");
    }

    public ActionResult Report()
    {
      var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add("КМ");
      worksheet.Cell(1, 1).Value = "Период";
      MemoryStream ms = new MemoryStream();
      workbook.SaveAs(ms);
      return File(ms.ToArray(), "application/vnd.ms-excel", string.Format("rf{0}.xlsx", DateTime.Now.Second));
    }

    public ActionResult GetRatesMoex(DateTime? dt, string type = "shares")
    {
      dt = dt ?? homeRepository.GetLastWorkDate();
      ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
      CookieContainer cookie = new CookieContainer();
      HttpWebRequest wr;
      wr = (HttpWebRequest)WebRequest.Create("https://passport.moex.com/authenticate");
      //wr.Proxy = null;
      wr.CookieContainer = cookie;
      wr.Headers.Add("Authorization", string.Format("Basic {0}", System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("GrishinAV@uralsib.ru:uralsib2012"))));
      wr.Timeout = 600000;
      wr.ReadWriteTimeout = 600000;
      HttpWebResponse hwr = (HttpWebResponse)wr.GetResponse();
      hwr.Close();

      var site = "iss.moex.com";
      var pathv = "c:\\data\\rate";
      MyWebClient wc = new MyWebClient(cookie);
      //wc.Proxy = null;
      wc.Headers.Add("Pragma: no-cache");

      var filezip = $"{pathv}\\securities_micex_stock_{type}_{dt:yyyy}_{dt:MM}_{dt:dd}.csv.zip";
      wc.MyDownloadFile($"https://{site}/iss/downloads/engines/stock/markets/{type}/years/{dt:yyyy}/months/{dt:MM}/days/{dt:dd}/securities_micex_stock_{type}_{dt:yyyy}_{dt:MM}_{dt:dd}.csv.zip", filezip);
      using (ZipArchive archive = ZipFile.OpenRead(filezip))
      {
        var e = archive.Entries.Where(p => p.Name.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        if (e != null)
        {
          var dir = pathv + "\\temp";
          if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

          var fn = Path.Combine(dir, e.Name);
          e.ExtractToFile(fn, true);

          using (var sr = new StreamReader(fn, Encoding.GetEncoding(1251)))
          {
            string t = sr.ReadToEnd();
            Regex rgx = new Regex("(;-?\\d+),(\\d+)");
            t = rgx.Replace(t, "$1.$2").Replace(";0000-00-00;", ";;");
            using (var sw = new StreamWriter(fn + "_", false, Encoding.GetEncoding(1251)))
            {
              sw.Write(t);
            }
          }
          System.IO.File.Delete(fn);
          Byte[] bytes;
          using (FileStream fs = new FileStream(fn + "_", FileMode.Open, FileAccess.Read))
          {
            BinaryReader reader = new BinaryReader(fs);
            bytes = reader.ReadBytes(Convert.ToInt32(fs.Length));
          }
          System.IO.File.Delete(fn + "_");
          return File(bytes, "application/vnd.ms-excel", Path.GetFileName(fn));
        }
      }
      return new HttpNotFoundResult("File not found");
    }

  }

}