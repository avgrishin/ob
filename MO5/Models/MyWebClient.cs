using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MO5.Models
{
  public class MyWebClient : WebClient
  {
    private int timeout;
    private CookieContainer cookie;

    public MyWebClient(CookieContainer _cookie)
    {
      cookie = _cookie;
      timeout = 600000;
    }

    protected override WebRequest GetWebRequest(Uri address)
    {
      WebRequest request = base.GetWebRequest(address);

      request.Timeout = timeout;
      //request.Headers.Add("Authorization", string.Format("Basic {0}", System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("GrishinAV@uralsib.ru:uralsib2012"))));
      if (request.GetType() == typeof(HttpWebRequest))
      {
        (request as HttpWebRequest).CookieContainer = cookie;
        (request as HttpWebRequest).ReadWriteTimeout = timeout;
      }
      return request;
    }

    public void MyDownloadFile(string address, string fileName)
    {
      int cycle = 10;
      while (cycle != 0)
      {
        try
        {
          DownloadFile(address, fileName);
          cycle = 0;
        }
        catch
        {
          if (cycle-- > 0)
          {
            System.Threading.Thread.Sleep(2000);
          }
          else
          {
            cycle = 0;
            throw;
          }
        }
      }
    }


  }

}
