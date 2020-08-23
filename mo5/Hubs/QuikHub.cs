using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MO5.Areas.Code.Models;
using Ninject.Modules;

namespace MO5.Hubs
{
  public class QuikHub : Hub
  {
    private readonly IQuikRepository quikRepository;
    public QuikHub()
    {
      quikRepository = new QuikRepository();
    }
    public QuikHub(IQuikRepository _quikRepository)
    {
      quikRepository = _quikRepository;
    }
    public void Send(Message m)
    {
      Clients.All.addNewMessageToPage(m);
    }
    public void addOrderRet(SendOrderRet m)
    {
      quikRepository.AddOrderRet(m);
      //quikRepository.KillOrder(m.Id, m.UserName);
    }

    public void aRet(string m)
    {
      quikRepository.k(m);
      //Clients.All.k("kill");
    }
    public void kRet(string m)
    {
    }
    public void killOrderRet(SendOrderRet m)
    {
      quikRepository.KillOrderRet(m);
    }
    public override Task OnConnected()
    {
      string name = Context.User.Identity.Name;
      Groups.Add(Context.ConnectionId, name);
      return base.OnConnected();
    }

    public override Task OnDisconnected(bool stopCalled)
    {
      //string name = Context.User.Identity.Name;
      return base.OnDisconnected(stopCalled);
    }
  }
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

  public class HubConnectionModule : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      this.Bind(typeof(IHubConnectionContext<dynamic>)).ToMethod(context =>
        Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<QuikHub>().Clients)
        .WhenInjectedInto<IQuikRepository>();
    }
  }

}