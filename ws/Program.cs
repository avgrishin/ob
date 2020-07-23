using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ws
{
  class Program
  {
    static void Main(string[] args)
    {
      WebClient wc = new WebClient();
      wc.Credentials = CredentialCache.DefaultCredentials;
      wc.DownloadString(args[0]);
    }
  }
}
