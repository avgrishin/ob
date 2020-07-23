public void Main()
    {
      HttpWebRequest wr = null;
      var cookie = new CookieContainer() { MaxCookieSize = 16000, Capacity = 60, PerDomainCapacity = 60 };
      string postData = "";
      string s = "";
      StreamWriter requestWriter = null;

      //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
      //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
      //ServicePointManager.Expect100Continue = true;
      //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
      wr = (HttpWebRequest)WebRequest.Create("https://spbexchange.ru/ru/market-data/totalsArch.aspx?date=2020.02.21");
      wr.Proxy = null;
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
      wr.Headers.Add("Cookie", "ASP.NET1_SessionId=kjkkg3h0sxcbmhea3ul54lbh;");
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

      using (var sw = new StreamWriter("c:\\tmp\\sp.csv"))
      {
        var nmax = 11;
        for (var n = 0; n < nmax; n++)
        {
          var d = 0;
          var pmax = n > 0 ? 6 : 5;
          for (var p = n > 0 ? 2 : 0; p <= pmax; p++)
          {
            wr = (HttpWebRequest)WebRequest.Create("https://spbexchange.ru/ru/market-data/totalsArch.aspx?date=2020.02.21");
            wr.Proxy = null;
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
            //wr.Referer = "https://spbexchange.ru/ru/market-data/totalsArch.aspx?date=2020.02.21";
            wr.Headers.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            wr.Headers.Add("Cookie", "ASP.NET1_SessionId=kjkkg3h0sxcbmhea3ul54lbh;");
            //wr.CookieContainer = cookie;
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
                var td = Regex.Matches(tr[i].Groups[1].Value, "<td.*?>(.+?)</td>", RegexOptions.Singleline);
                for (int j = 0; j < td.Count; j++)
                {
                  sw.Write((j == 0 ? "" : ";") + td[j].Groups[1].Value.Replace("\r\n", "").Trim());
                }
                sw.Write(";" + m.Groups[1].Value);
                sw.WriteLine("");
              }
            }
          }
        }
      }
      Dts.TaskResult = (int)ScriptResults.Success;
    }
