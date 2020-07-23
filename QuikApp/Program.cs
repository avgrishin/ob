using Microsoft.AspNet.SignalR.Client;
using QuikApp.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuikApp
{
  public class Message
  {
    public string name { get; set; }
    public string message { get; set; }
  }

  public class SendOrder
  {
    public int Id { get; set; }
    public string PathQuik { get; set; }
    public string TransactionString { get; set; }
    public string UserName { get; set; }
  }

  public class SendOrderRet
  {
    public int Id { get; set; }
    public string UserName { get; set; }
    public Int32 ReturnCode { get; set; }
    public Int32 ReplyCd { get; set; }
    public int TransId { get; set; }
    public UInt64 OrderNum { get; set; }
    public string ResultMessage { get; set; }
    public string EMsg { get; set; }
  }

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
          AddUpdateAppSettings("URL", url);
          AddUpdateAppSettings("User", u);
          AddUpdateAppSettings("Password", EncryptString(ToSecureString(p)));
          return;
        }
      }
      else {
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

    static void AddUpdateAppSettings(string key, string value)
    {
      try
      {
        var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        var settings = configFile.AppSettings.Settings;
        if (settings[key] == null)
        {
          settings.Add(key, value);
        }
        else
        {
          settings[key].Value = value;
        }
        configFile.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
      }
      catch (ConfigurationErrorsException)
      {
        Console.WriteLine("Error writing app settings");
      }
    }
  }
  public class Client
  {
    public Quik quik { get; set; }
    public string URL { get; set; }
    public ICredentials Credential { get; set; }
    public Client(string _URL, ICredentials _Credential)
    {
      quik = new Quik();
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
          IHubProxy quikHubProxy = hubConnection.CreateHubProxy("QuikHub");
          hubConnection.Credentials = Credential;
          quikHubProxy.On<SendOrder>("addOrder", async m =>
          {
            UInt64 OrderNum = 0;
            int TransId = 0, ReturnCode = 0, ReplyCd = 0;
            string ResultMessage = "", EMsg = "";
            try
            {
              var ret = true; // SendOrder(m, ref ReturnCode, ref ReplyCd, ref TransId, ref OrderNum, ref ResultMessage, ref EMsg);
              if (ret)
                await quikHubProxy.Invoke("addOrderRet", new SendOrderRet
                {
                  Id = m.Id,
                  UserName = m.UserName,
                  ReturnCode = ReturnCode,
                  ReplyCd = ReplyCd,
                  TransId = TransId,
                  OrderNum = OrderNum,
                  ResultMessage = ResultMessage,
                  EMsg = EMsg
                });
            }
            catch (Exception ex)
            {
              Console.WriteLine(ex.Message);
            }
          });
          quikHubProxy.On<SendOrder>("killOrder", async m =>
          {
            UInt64 OrderNum = 0;
            int TransId = 0, ReturnCode = 0, ReplyCd = 0;
            string ResultMessage = "", EMsg = "";
            var ret = true;// SendOrder(m, ref ReturnCode, ref ReplyCd, ref TransId, ref OrderNum, ref ResultMessage, ref EMsg);
            if (ret)
              await quikHubProxy.Invoke("killOrderRet", new SendOrderRet
              {
                Id = m.Id,
                UserName = m.UserName,
                ReturnCode = ReturnCode,
                ReplyCd = ReplyCd,
                TransId = TransId,
                OrderNum = OrderNum,
                ResultMessage = ResultMessage,
                EMsg = EMsg
              });
          });
          quikHubProxy.On<string>("a", async m =>
          {
            Console.WriteLine($"func a = {m}");
            await quikHubProxy.Invoke("aRet", m);
          });
          quikHubProxy.On<string>("k", async m =>
          {
            Console.WriteLine($"func k = {m}");
            await quikHubProxy.Invoke("kRet", "return kRet");
          });
          //quikHubProxy.
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

    public bool SendOrder(SendOrder m, ref Int32 ReturnCode, ref Int32 ReplyCd, ref int TransId, ref UInt64 OrderNum, ref string ResultMessage, ref string EMsg)
    {
      quik.connect(m.PathQuik);
      if (!quik.is_dll_connected())
      {
        EMsg = quik.LastEMsg;
        Console.WriteLine(quik.LastEMsg);
        return false;
      }
      if (!quik.is_quik_connected())
      {
        EMsg = quik.LastEMsg;
        Console.WriteLine(quik.LastEMsg);
        return false;
      }

      ReturnCode = quik.send_sync_transaction(m.TransactionString, ref ReplyCd, ref TransId, ref OrderNum, ref ResultMessage);
      Console.WriteLine($"Send {m.TransactionString}");
      Console.WriteLine($"ResultMessage {ResultMessage}");
      return true;
    }
  }
}
