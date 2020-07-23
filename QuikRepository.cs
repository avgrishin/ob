using Microsoft.AspNet.SignalR.Hubs;
using MO5.Models;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MO5.Areas.Code.Models
{
  public interface IQuikRepository
  {
    void AddOrder(string UserName);
    void AddOrderRet(Hubs.AddOrderRet m);
    void KillOrder(string UserName, UInt64 OrderNum);
    IEnumerable<dynamic> getQuikList(DateTime d1, DateTime d2, string sort, string dir);
  }

  public class QuikRepository : IQuikRepository
  {
    private readonly MiddleOfficeEntities db = new MiddleOfficeEntities() { };
    private readonly IHubConnectionContext<dynamic> clients;

    public QuikRepository(IHubConnectionContext<dynamic> _clients)
    {
      clients = _clients;
    }
    public void AddOrder(string UserName)
    {
      clients.Group(UserName).addOrder(new Hubs.AddOrder
      {
        Id = 1,
        PathQuik = @"C:\Uralsib\QUIK1\",
        TransactionString = "ACTION=NEW_ORDER; TRANS_ID=888; CLASSCODE=TQBR; SECCODE=SBER; ACCOUNT=Y02+00000B01; CLIENT_CODE=01993; TYPE=L; OPERATION=B; QUANTITY=1; PRICE=235",
        UserName = UserName
      });
    }

    public void AddOrderRet(Hubs.AddOrderRet m)
    {
      if (m.ReturnCode == 0)
        KillOrder(m.UserName, m.OrderNum);
    }
    public void KillOrder(string UserName, UInt64 OrderNum)
    {
      clients.Group(UserName).cancelOrder(new Hubs.AddOrder
      {
        Id = 1,
        PathQuik = @"C:\Uralsib\QUIK1\",
        TransactionString = $"ACTION=KILL_ORDER; TRANS_ID=888; CLASSCODE=TQBR; SECCODE=SBER; ORDER_KEY={OrderNum}",
        UserName = UserName
      });
    }
    public IEnumerable<dynamic> getQuikList(DateTime d1, DateTime d2, string sort, string dir)
    {
      var q = from qk in db.tQuikDeal.Where(p => p.DateDeal >= d1 && p.DateDeal < d2.AddDays(1))
              select qk;
      return q;
    }
  }
  public class QuikModule : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      this.Bind<IQuikRepository>().To<QuikRepository>().InSingletonScope();
    }
  }

}