using Microsoft.AspNet.SignalR.Hubs;
using MO5.Models;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MO5.Areas.Code.Models
{
  public interface IQuikRepository
  {
    void AddOrderRet(Hubs.SendOrderRet m);
    void KillOrderRet(Hubs.SendOrderRet m);
    void KillOrder(int Id, string UserName);
    IEnumerable<dynamic> getQuikDealList(DateTime d1, DateTime d2, Guid UserId, string sort, string dir);
    IEnumerable<dynamic> getQuikDealList(DateTime cd, Guid UserId);
    bool addQuikDeal(List<QuikDeal> data, Guid UserId);
    bool updQuikDeal(List<QuikDeal> data, Guid UserId);
    bool delQuikDeal(List<QuikDeal> data, Guid UserId);
    bool setQuikUserPath(int QuikID, string Path, Guid UserId);
    IEnumerable<dynamic> getQuikUserList(Guid UserId);
    void updQuikUser(int QuikID, Guid UserID, string Path);
    IEnumerable<dynamic> getQuikDealTypeList();
    IEnumerable<dynamic> getQuikSec(string q);
    IEnumerable<dynamic> getQuikCln(int QuikID);
    IEnumerable<dynamic> getQuikCln(string q, int QuikID);
    bool sendQuikOrder(DateTime cd, string UserName);
    bool addQuikModDeal(DateTime cd, string UserName);
    (bool, string) checkQuikDeal(DateTime cd, string UserName);
    void k(string Text);
    IEnumerable<dynamic> getMyTreatyCode(int QuikID, string UserName);
    IEnumerable<dynamic> addMyTreatyCode(List<tMyTreatyCode> data, string UserName);
    bool delMyTreatyCode(List<tMyTreatyCode> data, string UserName);
  }

  public class QuikRepository : IQuikRepository
  {
    private readonly MiddleOfficeEntities db = new MiddleOfficeEntities() { };
    private readonly IHubConnectionContext<dynamic> clients = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Hubs.QuikHub>().Clients;

    public void k(string Text)
    {
      clients.Group(Text).k(Text);
    }
    public IEnumerable<dynamic> getQuikDealList(DateTime d1, DateTime d2, Guid UserId, string sort, string dir)
    {
      d2 = d2.AddDays(1);
      var q = from qk in db.tQuikDeal.Where(p => p.DateDeal >= d1 && p.DateDeal < d2)
              join us in db.aspnet_Users on qk.UserID equals us.UserId
              where qk.UserID == UserId
              select new
              {
                qk.Brief,
                qk.ClassCode,
                qk.ClientAccount,
                qk.ClientCode,
                qk.Comment,
                qk.CreateDate,
                qk.DateDeal,
                qk.DealPrice,
                qk.Direction,
                qk.ID,
                qk.ISIN,
                qk.Lot,
                qk.Name,
                qk.Num,
                qk.OrderNum,
                qk.QuikID,
                QuikName = qk.tQuik.Name,
                db.tQuikUser.FirstOrDefault(p => p.UserID == qk.UserID && p.QuikID == qk.QuikID).Path,
                qk.RegNumber,
                qk.ReplayCode,
                qk.SecurityID,
                qk.StatusID,
                Status = qk.tQuikDealStatus.Name,
                qk.Trans_ID,
                qk.TreatyID,
                qk.Type,
                qk.TypeO,
                TypeOName = qk.tQuikDealType.Name,
                qk.UserID,
                us.UserName
              };
      return q;
    }
    public IEnumerable<dynamic> getQuikUserList(Guid UserId)
    {
      var q = from qk in db.tQuik
              join qu in db.tQuikUser.Where(p => p.UserID == UserId) on qk.ID equals qu.QuikID into _qu
              from qu in _qu.DefaultIfEmpty()
              where qk.IsDeleted == false
              select new
              {
                qk.ID,
                qk.Name,
                qk.ExternalID,
                qu.Path
              };
      return q;
    }

    public void updQuikUser(int QuikID, Guid UserID, string Path)
    {
      if (db.aspnet_Users.Where(p => p.UserId == UserID) != null)
      {
        var qu = db.tQuikUser.Find(new object[] { QuikID, UserID });
        if (qu == null)
        {
          db.tQuikUser.Add(new tQuikUser { UserID = UserID, QuikID = QuikID, Path = Path });
        }
        else
        {
          qu.Path = Path;
        }
        db.SaveChanges();
      }
    }
    public IEnumerable<dynamic> getQuikDealTypeList()
    {
      var q = db.tQuikDealType.OrderBy(p => p.ID).Select(p => new { p.ID, p.Name });
      return q;
    }

    public IEnumerable<dynamic> getQuikSec(string q) =>
      db.tQuikRate
      .Where(p => p.Name.StartsWith(q) || p.Brief.StartsWith(q) || p.ISIN.Contains(q))
      .Select(p => new
      {
        p.ID,
        p.Instrument,
        p.Brief,
        p.Name,
        p.ISIN,
        p.RegNumber,
        p.ClassCode,
        p.Lot
      });

    public IEnumerable<dynamic> getQuikCln(string q, int QuikID) =>
      db.tTreatyCode
      .Where(p => p.Code.StartsWith(q))
      .OrderBy(p => p.Code)
      .Take(10)
      .Select(p => new
      {
        p.ID,
        ClientCode = p.Code,
        ClientAccount = p.Razdel,
        p.TreatyID
      });

    public IEnumerable<dynamic> getMyTreatyCode(int QuikID, string UserName) =>
      db.tMyTreatyCode
      .Where(p => p.UserName == UserName)
      .Select(p => new
      {
        p.ID,
        p.TreatyCodeID,
        ClientCode = p.tTreatyCode.Code,
        ClientAccount = p.tTreatyCode.Razdel,
        p.tTreatyCode.TreatyID
      });

    public IEnumerable<dynamic> addMyTreatyCode(List<tMyTreatyCode> data, string UserName)
    {
      foreach (var e in data)
      {
        e.UserName = UserName;
      }
      db.tMyTreatyCode.AddRange(data);
      db.SaveChanges();

      var ids = data.Select(p => p.ID);
      var q = from p in db.tMyTreatyCode.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                p.ID,
                p.TreatyCodeID,
                ClientCode = p.tTreatyCode.Code,
                ClientAccount = p.tTreatyCode.Razdel,
                p.tTreatyCode.TreatyID
              };
      return q;
    }
    public bool delMyTreatyCode(List<tMyTreatyCode> data, string UserName)
    {
      try
      {
        foreach (var e in data)
        {
          var q1 = db.tMyTreatyCode.Find(e.ID);
          if (q1 != null)
          {
            if (q1.UserName == UserName)
            {
              db.tMyTreatyCode.Remove(q1);
            }
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

    public IEnumerable<dynamic> getQuikCln(int QuikID) =>
      db.tTreatyCode
      .OrderBy(p => p.Code)
      .Select(p => new
      {
        p.ID,
        ClientCode = p.Code,
        ClientAccount = p.Razdel,
        p.TreatyID
      });

    public IEnumerable<dynamic> getQuikDealList(DateTime cd, Guid UserId)
    {
      var q = from qk in db.tQuikDeal
              where qk.CreateDate == cd && qk.UserID == UserId
              select new
              {
                qk.Brief,
                qk.ClassCode,
                qk.ClientAccount,
                qk.ClientCode,
                qk.Comment,
                qk.CreateDate,
                qk.DateDeal,
                qk.DealPrice,
                qk.Direction,
                qk.ID,
                qk.ISIN,
                qk.Lot,
                qk.Name,
                qk.Num,
                qk.OrderNum,
                qk.QuikID,
                db.tQuikUser.FirstOrDefault(p => p.UserID == qk.UserID && p.QuikID == qk.QuikID).Path,
                qk.RegNumber,
                qk.ReplayCode,
                qk.SecurityID,
                qk.StatusID,
                Status = qk.tQuikDealStatus.Name,
                qk.Trans_ID,
                qk.TreatyID,
                qk.Type,
                qk.TypeO,
                TypeOName = qk.tQuikDealType.Name,
                qk.UserID
              };
      return q;
    }

    public bool addQuikDeal(List<QuikDeal> data, Guid UserId)
    {
      if (UserId == null)
        return false;
      var qd = new List<tQuikDeal>();
      foreach (var e in data)
      {
        var d = new tQuikDeal()
        {
          Brief = e.Brief,
          ClassCode = e.ClassCode,
          ClientAccount = e.ClientAccount,
          ClientCode = e.ClientCode,
          CreateDate = e.CreateDate,
          DateDeal = DateTime.Today,
          DealPrice = e.DealPrice,
          Direction = e.Direction,
          Form = e.Form,
          ISIN = e.ISIN,
          Lot = e.Lot,
          Name = e.Name,
          Num = e.Num,
          RegNumber = e.RegNumber,
          QuikID = e.QuikID,
          SecurityID = db.tSecurity.FirstOrDefault(s => s.ISIN == e.ISIN)?.SecurityID,
          StatusID = e.StatusID,
          Trans_ID = e.Trans_ID,
          TreatyID = e.TreatyID,
          Type = e.Type,
          TypeO = e.TypeO,
          UserID = UserId
        };
        qd.Add(d);
      }
      db.tQuikDeal.AddRange(qd);
      db.SaveChanges();
      return true;
    }
    public bool updQuikDeal(List<QuikDeal> data, Guid UserId)
    {
      if (UserId == null)
        return false;
      foreach (var e in data)
      {
        var q1 = db.tQuikDeal.Find(e.ID);
        if (q1 != null)
        {
          q1.Brief = e.Brief;
          q1.ClassCode = e.ClassCode;
          q1.ClientAccount = e.ClientAccount;
          q1.ClientCode = e.ClientCode;
          q1.CreateDate = e.CreateDate;
          q1.DealPrice = e.DealPrice;
          q1.Direction = e.Direction;
          q1.ISIN = e.ISIN;
          q1.Lot = e.Lot;
          q1.Name = e.Name;
          q1.Num = e.Num;
          q1.RegNumber = e.RegNumber;
          q1.QuikID = e.QuikID;
          q1.SecurityID = db.tSecurity.FirstOrDefault(s => s.ISIN == e.ISIN)?.SecurityID;
          q1.Trans_ID = e.Trans_ID;
          q1.TreatyID = e.TreatyID;
          q1.Type = e.Type;
          q1.TypeO = e.TypeO;
          q1.UserID = UserId;
          db.SaveChanges();
        }
      }
      return true;
    }
    public bool delQuikDeal(List<QuikDeal> data, Guid UserId)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.tQuikDeal.Where(p => ids.Contains(p.ID) && p.UserID == UserId);
        db.tQuikDeal.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (DbUpdateException ex)
      {
        throw new Exception("Db update Exception");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public bool setQuikUserPath(int QuikID, string Path, Guid UserId)
    {
      if (UserId == null)
        return false;
      var quik = db.tQuik.Find(QuikID);
      if (quik != null)
      {
        var qu = db.tQuikUser.Find(quik.ID, UserId);
        if (qu == null)
        {
          db.tQuikUser.Add(new tQuikUser { QuikID = quik.ID, UserID = UserId, Path = Path });
          db.SaveChanges();
        }
        else if (qu.Path != Path)
        {
          qu.Path = Path;
          db.SaveChanges();
        }
        return true;
      }
      return false;
    }
    public (bool, string) checkQuikDeal(DateTime cd, string UserName)
    {
      var u = db.aspnet_Users.FirstOrDefault(p => p.UserName == UserName).UserId;
      if (db.tQuikDeal.Any(qk => qk.CreateDate == cd && qk.UserID == u && qk.StatusID == 0 && qk.SecurityID == null))
        return (false, "Бумаги нет в системе");
      if (db.tQuikDeal.Any(qk => qk.CreateDate == cd && qk.UserID == u && qk.StatusID == 0 && qk.TreatyID == null))
        return (false, "Договора нет в системе");
      return (true, "");
    }
    public bool addQuikModDeal(DateTime cd, string UserName)
    {
      var u = db.aspnet_Users.FirstOrDefault(p => p.UserName == UserName).UserId;
      db.tModDeal.RemoveRange(db.tModDeal.Where(p => p.UserId == u && p.CreateDate == cd));
      db.SaveChanges();
      foreach (var r in from qk in db.tQuikDeal
                        where qk.CreateDate == cd && qk.UserID == u && qk.StatusID == 0
                        select new
                        {
                          qk.Brief,
                          qk.Name,
                          qk.ISIN,
                          qk.ClassCode,
                          qk.DealPrice,
                          qk.Direction,
                          qk.Num,
                          qk.Lot,
                          qk.SecurityID,
                          qk.TreatyID,
                          qk.DateDeal
                        })
      {
        var q = new tModDeal
        {
          CreateDate = cd,
          SecurityID = r.SecurityID,
          TreatyID = r.TreatyID,
          DealDate = r.DateDeal,
          DealPrice = r.DealPrice,
          DealTypeID = 1,
          Direction = r.Direction,
          FundID = 1,
          Num = r.Num * r.Lot,
          SupplyDate = r.DateDeal,
          ValueDate = r.DateDeal,
          UserId = u
        };
        var tr = db.tTreaty.Find(r.TreatyID);
        q.FinInstID = tr.FinInstID;
        var sec = db.tSecurity.Find(r.SecurityID);
        var qr = db.tQuikRate.FirstOrDefault(p => p.ISIN == r.ISIN && p.ClassCode == r.ClassCode && p.Brief == r.Brief);
        double? Qty = (double?)q.Num * q.DealPrice * (sec.Class == 2 ? qr.Nominal / 100 : 1) + (sec.Class == 2 ? qr?.NKD * (double?)q.Num : 0);
        q.Qty = Qty;
        q.AccInt = qr?.NKD;
        db.tModDeal.Add(q);
      }
      db.SaveChanges();
      return true;
    }
    public bool sendQuikOrder(DateTime cd, string UserName)
    {
      var u = db.aspnet_Users.FirstOrDefault(p => p.UserName == UserName).UserId;
      foreach (var r in from qk in db.tQuikDeal
                        where qk.CreateDate == cd && qk.UserID == u && qk.StatusID == 0
                        select new
                        {
                          qk.ClassCode,
                          qk.ClientAccount,
                          qk.ClientCode,
                          qk.DealPrice,
                          qk.Direction,
                          qk.ID,
                          qk.Name,
                          qk.Num,
                          db.tQuikUser.FirstOrDefault(p => p.UserID == u && p.QuikID == qk.QuikID).Path,
                          qk.Trans_ID,
                          qk.Type,
                          qk.TypeO
                        })
      {
        if (!string.IsNullOrWhiteSpace(r.Path) && r.Num > 0)
        {
          if (r.TypeO == 0)
          {
            var q = clients.Group(UserName).addOrder(new Hubs.SendOrder
            {
              Id = r.ID,
              PathQuik = r.Path,
              TransactionString = $"ACTION=NEW_ORDER; TRANS_ID={r.Trans_ID}; CLASSCODE={r.ClassCode}; SECCODE={r.Name}; ACCOUNT={r.ClientAccount}; CLIENT_CODE={r.ClientCode}; TYPE={(r.Type == true ? "M" : "L")}; OPERATION={(r.Direction == 0 ? "B" : "S")}; QUANTITY={(int?)r.Num}; PRICE={r.DealPrice}",
              UserName = UserName
            });
          }
        }
      }
      return true;
    }
    public void AddOrderRet(Hubs.SendOrderRet m)
    {
      if (m.ReturnCode == 0)
      {
        var q = db.tQuikDeal.Find(m.Id);
        if (q != null)
        {
          q.StatusID = 1;
          q.Comment = m.ResultMessage;
          q.OrderNum = (long?)m.OrderNum;
          q.ReplayCode = m.ReplyCd;
          db.SaveChanges();
        }
      }
      ;
    }
    public void KillOrder(int Id, string UserName)
    {
      var u = db.aspnet_Users.FirstOrDefault(p => p.UserName == UserName).UserId;
      foreach (var r in from qk in db.tQuikDeal
                        where qk.ID == Id && qk.UserID == u
                        select new
                        {
                          qk.ClassCode,
                          qk.ID,
                          qk.Name,
                          qk.OrderNum,
                          db.tQuikUser.FirstOrDefault(p => p.UserID == u && p.QuikID == qk.QuikID).Path,
                          qk.Trans_ID
                        })
      {
        var kill = new Hubs.SendOrder
        {
          Id = r.ID,
          PathQuik = r.Path,
          TransactionString = $"ACTION=KILL_ORDER; TRANS_ID={r.Trans_ID}; CLASSCODE={r.ClassCode}; SECCODE={r.Name}; ORDER_KEY={r.OrderNum}",
          UserName = UserName
        };
        var cg = clients.Group(UserName);
        cg.killOrder(kill);
      }
    }
    public void KillOrderRet(Hubs.SendOrderRet m)
    {
      if (m.ReturnCode == 0)
      {
        var q = db.tQuikDeal.Find(m.Id);
        if (q != null)
        {
          q.StatusID = 2;
          db.SaveChanges();
        }
      }
    }
  }
  public class QuikModule : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      this.Bind<IQuikRepository>().To<QuikRepository>().InRequestScope();
    }
  }

}