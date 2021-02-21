using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MO5.Models;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace MO5.Areas.Code.Models
{
  public interface IPaymentRepository
  {
    IEnumerable<dynamic> GetPaymentList(DateTime? d1, DateTime? d2, string UserName, string sort, string dir);
    IEnumerable<dynamic> AddPayment(List<tPayment> data, string UserName);
    IEnumerable<dynamic> UpdPayment(List<tPayment> data, string UserName);
    bool DelPayment(List<tPayment> data, string UserName);
    tBank GetBankByBIC(string BIC);
    string[] Unload(List<int> data);
    PaymentDoc GetPayment(int ID);
    IEnumerable<dynamic> GetReceiver(int id);
    IEnumerable<dynamic> GetReceiver(int? id, int? TreatyID);
    bool GetRest(int PaymID);
  }

  public class PaymentRepository : IPaymentRepository
  {
    private MiddleOfficeEntities db = new MiddleOfficeEntities() { };
    private readonly IHubConnectionContext<dynamic> clients = GlobalHost.ConnectionManager.GetHubContext<Hubs.RestHub>().Clients;

    public IEnumerable<dynamic> GetPaymentList(DateTime? d1, DateTime? d2, string UserName, string sort, string dir)
    {
      var pmt = db.tPayment.AsQueryable();
      if (d1.HasValue)
        pmt = pmt.Where(p => p.PayDate >= d1);
      if (d2.HasValue)
        pmt = pmt.Where(p => p.PayDate <= d2);
      var q = from pm in pmt
              select new
              {
                pm.Amount,
                pm.BankI,
                pm.BankO,
                pm.BICI,
                pm.BICO,
                pm.ID,
                pm.INNI,
                pm.INNO,
                pm.KAccI,
                pm.KAccO,
                pm.KPPI,
                pm.KPPO,
                pm.NameI,
                pm.NameO,
                pm.Number,
                pm.PayDate,
                pm.Queue,
                pm.RAccI,
                pm.RAccO,
                pm.Reference,
                pm.EnregID,
                pm.tEnregistrement.RecuDate,
                pm.tEnregistrement.Numero,
                trName = pm.tEnregistrement.tTreaty.Name,
                ClnName = pm.tEnregistrement.tTreaty.tFinInst.Name,
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q.ToArray();
    }

    public IEnumerable<dynamic> AddPayment(List<tPayment> data, string UserName)
    {
      foreach (var e in data)
      {
        e.InDateTime = DateTime.Now;
      }
      db.tPayment.AddRange(data);
      db.SaveChanges();

      var ids = data.Select(p => p.ID);
      var q = from pm in db.tPayment.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                pm.Amount,
                pm.BankI,
                pm.BankO,
                pm.BICI,
                pm.BICO,
                pm.ID,
                pm.INNI,
                pm.INNO,
                pm.KAccI,
                pm.KAccO,
                pm.KPPI,
                pm.KPPO,
                pm.NameI,
                pm.NameO,
                pm.Number,
                pm.PayDate,
                pm.Queue,
                pm.RAccI,
                pm.RAccO,
                pm.Reference,
                pm.EnregID,
                pm.tEnregistrement.RecuDate,
                pm.tEnregistrement.Numero,
                trName = pm.tEnregistrement.tTreaty.Name,
                ClnName = pm.tEnregistrement.tTreaty.tFinInst.Name,
              };
      return q;
    }

    public IEnumerable<dynamic> UpdPayment(List<tPayment> data, string UserName)
    {
      foreach (var e in data)
      {
        var q1 = db.tPayment.Find(e.ID);
        if (q1 != null)
        {
          q1.Amount = e.Amount;
          q1.BankI = e.BankI;
          q1.BankO = e.BankO;
          q1.BICI = e.BICI;
          q1.BICO = e.BICO;
          q1.InDateTime = DateTime.Now;
          q1.INNI = e.INNI;
          q1.INNO = e.INNO;
          q1.KAccI = e.KAccI;
          q1.KAccO = e.KAccO;
          q1.KPPI = e.KPPI;
          q1.KPPO = e.KPPO;
          q1.NameI = e.NameI;
          q1.NameO = e.NameO;
          q1.Number = e.Number;
          q1.PayDate = e.PayDate;
          q1.Queue = e.Queue;
          q1.RAccI = e.RAccI;
          q1.RAccO = e.RAccO;
          q1.Reference = e.Reference;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from pm in db.tPayment.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                pm.Amount,
                pm.BankI,
                pm.BankO,
                pm.BICI,
                pm.BICO,
                pm.ID,
                pm.INNI,
                pm.INNO,
                pm.KAccI,
                pm.KAccO,
                pm.KPPI,
                pm.KPPO,
                pm.NameI,
                pm.NameO,
                pm.Number,
                pm.PayDate,
                pm.Queue,
                pm.RAccI,
                pm.RAccO,
                pm.Reference,
                pm.EnregID,
                pm.tEnregistrement.RecuDate,
                pm.tEnregistrement.Numero,
                trName = pm.tEnregistrement.tTreaty.Name,
                ClnName = pm.tEnregistrement.tTreaty.tFinInst.Name,
              };
      return q;
    }

    public bool DelPayment(List<tPayment> data, string UserName)
    {
      try
      {
        foreach (var e in data)
        {
          var q1 = db.tPayment.Find(e.ID);
          if (q1 != null)
          {
            db.tPayment.Remove(q1);
          }
        }
        db.SaveChanges();
        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }

    public tBank GetBankByBIC(string BIC)
    {
      var q =
        from b in db.tBank
        where b.BIC == BIC
        select b;

      return q.FirstOrDefault();
    }

    public string[] Unload(List<int> data)
    {
      var q = db.tPayment.AsNoTracking().Where(p => data.Contains(p.ID)).ToArray().Select(pm => $"R000000                  {pm.BICI}{pm.RAccI}{pm.KAccI,20}{pm.Number.PadLeft(6, '0')}{pm.PayDate:ddMMyyyy}01{pm.BICO}{pm.RAccO}{pm.KAccO}{string.Format("{0:F0}", (int)(pm.Amount * 100)).PadLeft(18, '0')}{pm.Queue % 10:f0}{pm.INNI,12}{pm.KPPI,9}{pm.NameI,-160}{pm.INNO,12}{pm.KPPO,9}{pm.NameO,-160}{pm.Reference,-210}{"",8}{DateTime.Today:ddMMyyyy}{"",8}{"",8}{"",2}{"",20}{"",11}{"",2}{"",10}{"",15}{"",10}{"",2}{"",3}{"",2}{"",3}{"",8}{"",18}10 100{"",3}{"",2}0{"",3}{"",8}IN{"",25}{"",35}");
      return q.ToArray();
    }

    public PaymentDoc GetPayment(int ID)
    {
      var q =
        from pm in db.tPayment.AsNoTracking()
        where pm.ID == ID
        select new PaymentDoc
        {
          ID = pm.ID,
          Amount = pm.Amount,
          BankO = pm.BankO,
          BICO = pm.BICO,
          INNO = pm.INNO,
          KAccO = pm.KAccO,
          KPPO = pm.KPPO,
          NameO = pm.NameO,
          RAccO = pm.RAccO,
          Reference = pm.Reference,
          Treaty = pm.tEnregistrement.tTreaty.Name,
          Client = pm.tEnregistrement.tTreaty.tFinInst.Name,
        };
      return q.FirstOrDefault();
    }
    public IEnumerable<dynamic> GetReceiver(int id)
    {
      var q = from en in db.tEnregistrement
              where en.ID == id
              from ena in db.tEnregistrement
              where ena.TreatyID == en.TreatyID && ena.ID != en.ID
              from pm in db.tPayment
              where pm.EnregID == ena.ID
              select new { pm.NameO, pm.INNO, pm.BankO, pm.BICO, pm.KAccO, pm.KPPO, pm.RAccO };
      return q.Distinct();
    }

    public IEnumerable<dynamic> GetReceiver(int? id, int? TreatyID)
    {
      var eq = db.tEnregistrement.AsQueryable();
      if (id.HasValue)
        eq = eq.Where(p => p.ID == id);
      else
        eq = eq.Where(p => p.TreatyID == TreatyID);
      var q = from en in eq
              from ena in db.tEnregistrement
              where ena.TreatyID == en.TreatyID && ena.ID != (id ?? -1)
              from pm in db.tPayment
              where pm.EnregID == ena.ID && pm.BICO != null
              select new { pm.NameO, pm.INNO, pm.BankO, pm.BICO, pm.KAccO, pm.KPPO, pm.RAccO };
      return q.Distinct();
    }

    public bool GetRest(int PaymID)
    {
      var TreatyID = db.tPayment.Where(p => p.ID == PaymID).Select(p => p.tEnregistrement.TreatyID).FirstOrDefault();
      if (TreatyID != null)
      {
        var q = clients.All.getRest(new Hubs.SendRequest { Id = TreatyID.Value, PaymId = PaymID });
        return true;
      }
      return false;
    }
  }
  public class PaymentModule : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      this.Bind<IPaymentRepository>().To<PaymentRepository>().InRequestScope();
    }
  }

}
