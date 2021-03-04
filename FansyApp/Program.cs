using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FansyApp
{
  class Program
  {
    static async Task Main(string[] args)
    {
      if (args.Length > 0)
      {
        if (args.Where(p => p == "-config").Any())
        {
          Console.Write("URL(http://ob.lan.qbfin.ru):");
          string url = Console.ReadLine();
          if (string.IsNullOrEmpty(url)) url = "http://ob.lan.qbfin.ru";
          Console.Write("User:");
          var u = Console.ReadLine();
          Console.Write("Password:");
          var p = Console.ReadLine();
          ConfigurationManager.AppSettings["URL"] = url;
          ConfigurationManager.AppSettings["User"] = u;
          ConfigurationManager.AppSettings["Password"] = EncryptString(ToSecureString(p));
          return;
        }
      }
      else
      {
        var URL = ConfigurationManager.AppSettings["URL"];
        var User = ConfigurationManager.AppSettings["User"];
        var Password = ConfigurationManager.AppSettings["Password"];
        var credential = CredentialCache.DefaultCredentials;
        if (!string.IsNullOrEmpty(User))
        {
          credential = new NetworkCredential(User, DecryptString(Password));
        }
        var client = new Client(URL, credential);
        await client.Run();
      }
    }

    static byte[] entropy = Encoding.Unicode.GetBytes("Salt Is Not A Password");
    public static string EncryptString(SecureString input)
    {
      byte[] encryptedData = ProtectedData.Protect(
          Encoding.Unicode.GetBytes(ToInsecureString(input)),
          entropy,
          DataProtectionScope.CurrentUser);
      return Convert.ToBase64String(encryptedData);
    }

    public static SecureString DecryptString(string encryptedData)
    {
      try
      {
        byte[] decryptedData = ProtectedData.Unprotect(
            Convert.FromBase64String(encryptedData),
            entropy,
            DataProtectionScope.CurrentUser);
        return ToSecureString(Encoding.Unicode.GetString(decryptedData));
      }
      catch
      {
        return new SecureString();
      }
    }

    public static SecureString ToSecureString(string input)
    {
      SecureString secure = new SecureString();
      foreach (char c in input)
      {
        secure.AppendChar(c);
      }
      secure.MakeReadOnly();
      return secure;
    }

    public static string ToInsecureString(SecureString input)
    {
      string returnValue = string.Empty;
      IntPtr ptr = Marshal.SecureStringToBSTR(input);
      try
      {
        returnValue = Marshal.PtrToStringBSTR(ptr);
      }
      finally
      {
        Marshal.ZeroFreeBSTR(ptr);
      }
      return returnValue;
    }

  }

  public class Client
  {
    public string URL { get; set; }
    public ICredentials Credential { get; set; }
    public Client(string _URL, ICredentials _Credential)
    {
      URL = _URL;
      Credential = _Credential;
    }

    public async Task Run()
    {
      try
      {
        //"http://localhost:61740/"
        using (var hubConnection = new HubConnection(ConfigurationManager.AppSettings["URL"] ?? "http://ob.lan.qbfin.ru"))
        {
          IHubProxy restHubProxy = hubConnection.CreateHubProxy("RestHub");
          hubConnection.Credentials = Credential;
          restHubProxy.On<SendRequest>("getRest", async m =>
          {
            try
            {
              var ret = true; // SendOrder(m, ref ReturnCode, ref ReplyCd, ref TransId, ref OrderNum, ref ResultMessage, ref EMsg);
              var data = new ObData(new SqlDataAccess());
              var amt = (await data.GetDURest(m.Id)).Sum();
              if (ret)
                await restHubProxy.Invoke("getRestRet", new SendRequestRet
                {
                  Id = m.Id,
                  Amount = amt
                });
            }
            catch (Exception ex)
            {
              Console.WriteLine(ex.Message);
            }
          });

          hubConnection.ConnectionSlow += () => { Console.WriteLine("ConnectionSlow"); };
          hubConnection.Reconnecting += () => { Console.WriteLine("Reconnecting"); };
          hubConnection.Reconnected += () => { Console.WriteLine("Reconnected"); };

          await hubConnection.Start();
          Console.WriteLine("HubConnection started");
          Console.WriteLine("Press Esc to exit");
          while (true)
          {
            var result = Console.ReadKey(intercept: true);
            if (result.Key == ConsoleKey.Escape)
            {
              break;
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        if (ex.InnerException != null)
        {
          Console.WriteLine(ex.InnerException.Message);
          if (ex.InnerException.InnerException != null)
          {
            Console.WriteLine(ex.InnerException.InnerException.Message);
          }
        }
        Console.ReadKey();
      }
      Console.WriteLine("Exit");
    }
  }

  public class SendRequest
  {
    public int Id { get; set; }
    public int PaymId { get; set; }
  }
  public class SendRequestRet
  {
    public int Id { get; set; }
    public int PaymId { get; set; }
    public decimal Amount { get; set; }
  }

}
