using ClosedXML.Excel;
using Microsoft.AspNet.SignalR.Hubs;
using MO5.Helpers;
using MO5.Hubs;
using MO5.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Mail;
using System.Reflection;
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

    [HttpPost]
    public ActionResult getWorkDateT(DateTime dt, int t)
    {
      return new JsonnResult { Data = new { wdt = homeRepository.GetWorkDateT(dt, t) } };
    }

    [HttpPost]
    public ActionResult getTBetweenWorkDates(DateTime dt1, DateTime dt2)
    {
      return new JsonnResult { Data = new { t = homeRepository.GetTBetweenWorkDates(dt1, dt2) } };
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

    ZipArchive OpenRead(string filename)
    {
      return new ZipArchive(System.IO.File.OpenRead(filename), ZipArchiveMode.Read);
    }

    public ActionResult GetRateMoex(DateTime? dt, string security = "US4581401001")
    {
      //https://iss.moex.com/iss/securities.json?q=GAZP&iss.meta=on&securities.columns=shortname,name,emitent_inn,isin,secid,primary_boardid
      var wc = new WebClient { Encoding = Encoding.GetEncoding(1251) };
      wc.Proxy = null;
      wc.Headers.Add("Content-type", "application/x-www-form-urlencoded; charset=UTF-8");
      var response = wc.UploadString("https://www.moex.com/Search.asmx/IssuesListSearch", $"searchString={security}&langID=1");
      var r = new Regex("<string.+?>(.+?)</string>", RegexOptions.Singleline);
      var m = r.Match(response);
      if (m.Success)
      {
        var r1 = new Regex("\\[.*?\\[(.+?)\\]", RegexOptions.Singleline);
        var m1 = r1.Match(m.Groups[1].Value);
        if (m1.Success)
        {
          var s = m1.Groups[1].Value.Split(new char[] { ',' });
          if (s.Length > 0)
          {
            var r2 = new Regex("\\\"(.+?)\\\"", RegexOptions.Singleline);
            var m2 = r2.Match(s[s.Length - 1]);
            if (m2.Success)
            {
              var m3 = r2.Match(s[s.Length - 3]);
              if (m3.Success)
              {
                var m4 = r2.Match(s[1]);
                if (m4.Success)
                {
                  response = wc.DownloadString($"https://iss.moex.com/iss/history/engines/stock/markets/{ (m3.Groups[1].Value == "stock_dr" || m3.Groups[1].Value == "stock_etf" ? "shares" : m2.Groups[1].Value == "FQBR" ? "foreignshares" : m3.Groups[1].Value.Substring(6, m3.Groups[1].Value.Length - 6)) }/boards/{m2.Groups[1].Value}/securities/{m4.Groups[1].Value}.csv?from={dt:yyyy-MM-dd}&till={dt:yyyy-MM-dd}");
                  return File(Encoding.GetEncoding(1251).GetBytes(response.Replace("\n\n\n", "")), "application/text", "moex_b.csv");
                }
              }
            }
          }
        }
      }
      return HttpNotFound();
    }

    public ActionResult GetRatesSpbExch(DateTime? dt)
    {
      ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
      ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
      HttpWebRequest wr = null;
      var cookie = new CookieContainer() { MaxCookieSize = 16000, Capacity = 60, PerDomainCapacity = 60 };
      string postData = "";
      string s = "";
      StreamWriter requestWriter = null;
      wr = (HttpWebRequest)WebRequest.Create(string.Format("https://spbexchange.ru/ru/market-data/totalsArch.aspx?date={0:yyyy.MM.dd}", dt));
      wr.Credentials = CredentialCache.DefaultCredentials;

      //wr.Proxy = null;
      wr.CookieContainer = cookie;
      wr.Method = WebRequestMethods.Http.Get;
      wr.KeepAlive = true;
      wr.Headers.Add("Cache-Control", "max-age=0");
      wr.Headers.Add("Upgrade-Insecure-Requests", "1");
      wr.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";
      wr.Headers.Add("Sec-Fetch-Dest", "document");
      wr.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
      wr.Headers.Add("Sec-Fetch-Site", "same-origin");
      wr.Headers.Add("Sec-Fetch-Mode", "navigate");
      wr.Headers.Add("Sec-Fetch-User", "?1");
      wr.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
      using (HttpWebResponse hwr = (HttpWebResponse)wr.GetResponse())
      {
        using (Stream receiveStream = hwr.GetResponseStream())
        {
          using (StreamReader sr = new StreamReader(receiveStream, Encoding.UTF8))
          {
            s = sr.ReadToEnd();
          }
        }
      }
      var m = Regex.Match(s, "<input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\" value=\"(.+?)\"");
      var ViewState = System.Web.HttpUtility.UrlEncode(m.Groups[1].Value);
      var e = Regex.Match(s, "<input type=\"hidden\" name=\"__EVENTVALIDATION\" id=\"__EVENTVALIDATION\" value=\"(.+?)\"");
      var EventValidation = System.Web.HttpUtility.UrlEncode(e.Groups[1].Value);
      m = Regex.Match(s, "<input type=\"hidden\" name=\"bxValidationToken\" value=\"(.+?)\"");
      var bxValidationToken = System.Web.HttpUtility.UrlEncode(m.Groups[1].Value);
      var rdt = new Regex("(\\d+),(\\d+)");
      var rmn = new Regex("^-$");
      var sw = new StringBuilder();

      var nmax = 11;
      for (var n = 0; n < nmax; n++)
      {
        var d = 0;
        var pmax = n > 0 ? 6 : 5;
        for (var p = n > 0 ? 2 : 0; p <= pmax; p++)
        {
          wr = (HttpWebRequest)WebRequest.Create(string.Format("https://spbexchange.ru/ru/market-data/totalsArch.aspx?date={0:yyyy.MM.dd}", dt));
          wr.Method = WebRequestMethods.Http.Post;
          wr.ProtocolVersion = HttpVersion.Version11;
          wr.Timeout = 600000;
          wr.ReadWriteTimeout = 600000;
          wr.KeepAlive = true;
          wr.Headers.Add("Cache-Control", "no-cache");
          wr.Headers.Add("Sec-Fetch-Dest", "empty");
          wr.Headers.Add("X-Requested-With", "XMLHttpRequest");
          wr.Headers.Add("X-MicrosoftAjax", "Delta=true");
          wr.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
          wr.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";
          wr.Accept = "*/*";
          wr.Headers.Add("Origin", "https://spbexchange.ru");
          wr.Headers.Add("Sec-Fetch-Site", "same-origin");
          wr.Headers.Add("Sec-Fetch-Mode", "cors");
          wr.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
          wr.CookieContainer = cookie;
          postData = string.Format("ctl00%24ScriptManager1=ctl00%24BXContent%24up%7Cctl00%24BXContent%24pager%24ctl00%24ctl0{0}" +
            "&bitrix_include_areas=N" +
            "&__EVENTTARGET=ctl00%24BXContent%24pager%24ctl00%24ctl0{0}" +
            "&__EVENTARGUMENT=" +
            "&__VIEWSTATE={1}" +
            "&bxValidationToken={3}" +
            "&ctl00%24searchform1%24searchform1%24searchform1%24query=%D0%9F%D0%BE%D0%B8%D1%81%D0%BA..." +
            "&__VIEWSTATEGENERATOR=1E76840D" +
            "&__EVENTVALIDATION={2}" +
            "&__ASYNCPOST=true" +
            "&", p, ViewState, EventValidation, bxValidationToken);

          wr.ContentLength = postData.Length;
          using (requestWriter = new StreamWriter(wr.GetRequestStream()))
          {
            requestWriter.Write(postData);
            requestWriter.Close();
          }

          s = "";
          using (HttpWebResponse hwr = (HttpWebResponse)wr.GetResponse())
          {
            using (Stream receiveStream = hwr.GetResponseStream())
            {
              using (StreamReader sr = new StreamReader(receiveStream, Encoding.UTF8))
              {
                s = sr.ReadToEnd();
              }
            }
          }
          m = Regex.Match(s, "hiddenField\\|__VIEWSTATE\\|(.+?)\\|");
          ViewState = System.Web.HttpUtility.UrlEncode(m.Groups[1].Value);
          m = Regex.Match(s, "hiddenField\\|__EVENTVALIDATION\\|(.+?)\\|");
          EventValidation = System.Web.HttpUtility.UrlEncode(m.Groups[1].Value);
          m = Regex.Match(s, "<span>(\\d+)</span>");
          if (d == 0)
          {
            d++;
            var g = Regex.Matches(s, "<a.+?href=\"javascript:__doPostBack\\(&#39;ctl00\\$BXContent\\$pager\\$ctl00\\$ctl0(\\d)&#39;,&#39;&#39;\\)\">");
            var p1 = int.Parse(g[g.Count - 1].Groups[1].Value);
            if (p1 < pmax)
            {
              pmax = p1;
              nmax = 0;
            }
          }

          var t = Regex.Match(s, "<table border=\"1\">(.+?)</table>", RegexOptions.Singleline);
          if (t.Success)
          {
            var tr = Regex.Matches(t.Groups[1].Value, "<tr.*?>(.+?)</tr>", RegexOptions.Singleline);
            for (int i = 2; i < tr.Count; i++)
            {
              var td = Regex.Matches(tr[i].Groups[1].Value, "<td.*?>(.*?)</td>", RegexOptions.Singleline);
              for (int j = 0; j < td.Count; j++)
              {
                sw.Append((j == 0 ? string.Format("{0:yyyy.MM.dd}", dt) : "") + ";" + rmn.Replace(rdt.Replace(td[j].Groups[1].Value.Replace("\r\n", "").Trim(), "$1.$2"), ""));
              }
              sw.Append(";" + m.Groups[1].Value);
              sw.AppendLine("");
            }
          }
        }
      }
      return File(Encoding.GetEncoding(1251).GetBytes(sw.ToString().Replace("\n\n\n", "")), "application/text", "moex_b.csv");
    }
    public ActionResult GetRatesMoex(DateTime? dt, string type = "shares")
    {
      //dt = dt ?? homeRepository.GetLastWorkDate();
      //ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
      //CookieContainer cookie = new CookieContainer();
      //HttpWebRequest wr;
      //wr = (HttpWebRequest)WebRequest.Create("https://passport.moex.com/authenticate");
      ////wr.Proxy = null;
      //wr.CookieContainer = cookie;
      //wr.Headers.Add("Authorization", string.Format("Basic {0}", System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("GrishinAV@uralsib.ru:uralsib2012"))));
      //wr.Timeout = 600000;
      //wr.ReadWriteTimeout = 600000;
      //HttpWebResponse hwr = (HttpWebResponse)wr.GetResponse();
      //hwr.Close();

      //var site = "iss.moex.com";
      //var pathv = "c:\\data\\rate";
      //MyWebClient wc = new MyWebClient(cookie);
      ////wc.Proxy = null;
      //wc.Headers.Add("Pragma: no-cache");

      //var filezip = $"{pathv}\\securities_micex_stock_{type}_{dt:yyyy}_{dt:MM}_{dt:dd}.csv.zip";
      //wc.MyDownloadFile($"https://{site}/iss/downloads/engines/stock/markets/{type}/years/{dt:yyyy}/months/{dt:MM}/days/{dt:dd}/securities_micex_stock_{type}_{dt:yyyy}_{dt:MM}_{dt:dd}.csv.zip", filezip);
      //using (ZipArchive archive = new ZipArchive(System.IO.File.OpenRead(filezip), ZipArchiveMode.Read))
      ////using (ZipArchive archive = ZipFile.OpenRead(filezip))
      //{
      //  var e = archive.Entries.Where(p => p.Name.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
      //  if (e != null)
      //  {
      //    var dir = pathv + "\\temp";
      //    if (!Directory.Exists(dir))
      //      Directory.CreateDirectory(dir);

      //    var fn = Path.Combine(dir, e.Name);
      //    e.ExtractToFile(fn, true);

      //    using (var sr = new StreamReader(fn, Encoding.GetEncoding(1251)))
      //    {
      //      string t = sr.ReadToEnd();
      //      Regex rgx = new Regex("(;-?\\d+),(\\d+)");
      //      t = rgx.Replace(t, "$1.$2").Replace(";0000-00-00;", ";;");
      //      using (var sw = new StreamWriter(fn + "_", false, Encoding.GetEncoding(1251)))
      //      {
      //        sw.Write(t);
      //      }
      //    }
      //    System.IO.File.Delete(fn);
      //    Byte[] bytes;
      //    using (FileStream fs = new FileStream(fn + "_", FileMode.Open, FileAccess.Read))
      //    {
      //      BinaryReader reader = new BinaryReader(fs);
      //      bytes = reader.ReadBytes(Convert.ToInt32(fs.Length));
      //    }
      //    System.IO.File.Delete(fn + "_");
      //    return File(bytes, "application/vnd.ms-excel", Path.GetFileName(fn));
      //  }
      //}
      return new HttpNotFoundResult("File not found");
    }

    public ActionResult GetRatesCBonds(DateTime? dt, string type = "shares")
    {
      DateTime d = (dt ?? homeRepository.GetLastWorkDate()).Value;
      if (type == "shares")
      {
        return CBonds<get_tradings_stocks>("trading_date", d);
      }
      else if (type == "bonds")
      {
        return CBonds<get_tradings>("date", d);
      }
      return new HttpNotFoundResult("File not found");
    }

    private ActionResult CBonds<T>(string d, DateTime dt)
    {
      CookieContainer cookie = new CookieContainer();
      MyWebClient wc = new MyWebClient(cookie) { Encoding = Encoding.UTF8 };
      wc.Proxy.Credentials = CredentialCache.DefaultCredentials;
      string s;
      int offset = 0;
      using (var ms = new MemoryStream())
      {
        using (var sw = new StreamWriter(ms, Encoding.GetEncoding(1251)))
        {
          var pi = new List<PropertyInfo>();
          foreach (PropertyInfo prop in typeof(T).GetProperties())
          {
            pi.Add(prop);
            sw.Write(prop.Name);
            sw.Write("\t");
          }
          sw.WriteLine();
          while (true)
          {
            s = wc.UploadString("https://ws.cbonds.info/services/json/" + typeof(T).Name + "/?lang=rus&cache_all_revalidate=0&nocache_all=0", "{\"auth\":{\"login\":\"yury.orlov@qbf.world\",\"password\":\"cbonds2019\"},\"filters\":[{\"field\":\"" + d + "\",\"operator\":\"eq\",\"value\":\"" + string.Format("{0:yyyy-MM-dd}", dt) + "\"}],\"quantity\":{\"limit\":100,\"offset\":" + offset.ToString() + "},\"sorting\":[{\"field\":\"\",\"order\":\"asc\"}],\"paging\": 1}");
            //s = wc.UploadString("https://ws.cbonds.info/services/json/" + typeof(T).Name + "/?lang=rus&cache_all_revalidate=0&nocache_all=0", "{\"auth\":{\"login\":\"it@qbfin.ru\",\"password\":\"qbf2310\"},\"filters\":[{\"field\":\"" + d + "\",\"operator\":\"eq\",\"value\":\"" + string.Format("{0:yyyy-MM-dd}", dt) + "\"}],\"quantity\":{\"limit\":100,\"offset\":" + offset.ToString() + "},\"sorting\":[{\"field\":\"\",\"order\":\"asc\"}],\"paging\": 1}");
            var j1 = JsonConvert.DeserializeObject<RootObject<T>>(s);

            foreach (var ra in j1.items)
            {
              foreach (PropertyInfo prop in pi)
              {
                sw.Write((prop.GetValue(ra, null) ?? "").ToString().Replace("\r\n", "").Replace("\t", ""));
                sw.Write("\t");
              }
              sw.WriteLine();
            }
            if (j1.offset + j1.count == j1.total) break;
            offset += j1.count;
          }
          sw.Flush();
          ms.Position = 0;
          return File(ms.ToArray(), "application/text", typeof(T).Name + ".txt");
        }
      }
    }
    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";
      var s = RenderViewToString(ControllerContext, "~/views/home/contact.cshtml");
      IHubConnectionContext<dynamic> Clients = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<QuikHub>().Clients;
      Clients.Group(User.Identity.Name).a(User.Identity.Name);

      //Clients.Group(User.Identity.Name).addOrder(new SendOrder
      //{
      //  Id = 1,
      //  PathQuik = @"C:\Uralsib\QUIK1\",
      //  TransactionString = "ACTION=NEW_ORDER; TRANS_ID=888; CLASSCODE=TQBR; SECCODE=SBER; ACCOUNT=Y02+00000B01; CLIENT_CODE=01993; TYPE=L; OPERATION=B; QUANTITY=1; PRICE=235",
      //  UserName = User.Identity.Name
      //});

      return Content(s);
    }

    public ActionResult Chat()
    {
      return View();
    }
  }
}