using MO5.Models;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Dynamic;

namespace MO5.Areas.Code.Models
{
  public interface IRiskRepository
  {
    IEnumerable<dynamic> GetLimitList(int TypeID);
    IEnumerable<dynamic> GetEmitentList(string filter, string sort, string dir);
    IEnumerable<dynamic> GetFinInstGroupList(int FinInstGroupTypeID, string filter, string sort, string dir);
    IEnumerable<dynamic> AddFinInstGroup(int FinInstGroupTypeID, List<tFinInstGroup> data);
    IEnumerable<dynamic> UpdFinInstGroup(int FinInstGroupTypeID, List<tFinInstGroup> data);
    bool DelFinInstGroup(int FinInstGroupTypeID, List<tFinInstGroup> data);
    IEnumerable<dynamic> AddLimitList(List<tLimitList> data);
    IEnumerable<dynamic> UpdLimitList(List<tLimitList> data);
    bool DelLimitList(List<tLimitList> data);
    bool AddFinInstFinInstGroup(int EmitentID, int GroupID, int FinInstGroupTypeID);
    IEnumerable<dynamic> RepLimit1(int TypeID, DateTime? dt);
    DateTime GetPrevWorkDate(DateTime date, int days);
  }

  public class RiskRepository : IRiskRepository
  {
    private MiddleOfficeEntities db = new MiddleOfficeEntities() { };

    public IEnumerable<dynamic> GetLimitList(int TypeID)
    {
      var q = from ll in db.tLimitList
              where ll.TypeID == TypeID
              select new
              {
                ll.ID,
                ll.EmitentID,
                FinGroup = ll.tFinInst.tFinInstGroup.FirstOrDefault(p => p.FinInstGroupTypeID == 10).Name,
                FinName = ll.tFinInst.Name,
                ll.tFinInst.INN,
                TypeName = ll.TypeID == 1 ? "ДУ" : ll.TypeID == 2 ? "CC" : "",
                ll.Value1,
                ll.Value2,
                ll.Value3,
                ll.Value4,
                ll.Value5
              };
      return q.OrderBy(p => p.FinGroup).ThenBy(p => p.FinName);
    }

    public IEnumerable<dynamic> GetEmitentList(string filter, string sort, string dir)
    {
      var q1 = db.tFinInst.Where(p => db.tSecurity.Any(s => s.IssuerID == p.FinInstID));
      if (!string.IsNullOrEmpty(filter))
        q1 = q1.Where(p => p.Name.Contains(filter));
      var q = from fi in q1
              select new
              {
                fi.FinInstID,
                fi.Name,
                fi.INN,
                fi.KPP,
                fi.Name1,
                FinGroup = fi.tFinInstGroup.FirstOrDefault(p => p.FinInstGroupTypeID == 10).Name,
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> GetFinInstGroupList(int FinInstGroupTypeID, string filter, string sort, string dir)
    {
      var q1 = db.tFinInstGroup.AsQueryable();
      if (!string.IsNullOrEmpty(filter))
        q1 = q1.Where(p => p.Name.Contains(filter));
      var q = from i in q1
              where i.FinInstGroupTypeID == FinInstGroupTypeID
              select new
              {
                i.FinInstGroupID,
                i.Name
              };
      return q;
    }

    public IEnumerable<dynamic> AddFinInstGroup(int FinInstGroupTypeID, List<tFinInstGroup> data)
    {
      foreach (var e in data)
      {
        e.FinInstGroupTypeID = FinInstGroupTypeID;
      }
      db.tFinInstGroup.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.FinInstGroupID);
      var q = from i in db.tFinInstGroup.Where(p => ids.Contains(p.FinInstGroupID))
              select new
              {
                i.FinInstGroupID,
                i.Name
              };
      return q;
    }

    public IEnumerable<dynamic> UpdFinInstGroup(int FinInstGroupTypeID, List<tFinInstGroup> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tFinInstGroup.FirstOrDefault(p => p.FinInstGroupID == e.FinInstGroupID && p.FinInstGroupTypeID == FinInstGroupTypeID);
        if (q1 != null)
        {
          q1.Name = e.Name;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.FinInstGroupID);
      var q = from i in db.tFinInstGroup.Where(p => ids.Contains(p.FinInstGroupID))
              select new
              {
                i.FinInstGroupID,
                i.Name
              };
      return q;
    }


    public bool DelFinInstGroup(int FinInstGroupTypeID, List<tFinInstGroup> data)
    {
      try
      {
        var ids = data.Select(p => p.FinInstGroupID);
        var e = db.tFinInstGroup.Where(p => ids.Contains(p.FinInstGroupID) && p.FinInstGroupTypeID == FinInstGroupTypeID);
        db.tFinInstGroup.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (DbUpdateException ex)
      {
        throw new Exception($"Db update Exception {ex.Message}");
      }
      catch (Exception ex)
      {
        throw ex;
      }
      //return false;
    }

    public IEnumerable<dynamic> AddLimitList(List<tLimitList> data)
    {
      foreach (var e in data)
      {
        e.InDateTime = DateTime.Now;
      }
      db.tLimitList.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.ID);
      var q = from ll in db.tLimitList
              where ids.Contains(ll.ID)
              select new
              {
                ll.ID,
                ll.EmitentID,
                FinGroup = ll.tFinInst.tFinInstGroup.FirstOrDefault(p => p.FinInstGroupTypeID == 10).Name,
                FinName = ll.tFinInst.Name,
                ll.tFinInst.INN,
                TypeName = ll.TypeID == 1 ? "ДУ" : ll.TypeID == 2 ? "CC" : "",
                ll.Value1,
                ll.Value2,
                ll.Value3,
                ll.Value4,
                ll.Value5
              };
      return q;
    }

    public IEnumerable<dynamic> UpdLimitList(List<tLimitList> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tLimitList.Find(e.ID);
        if (q1 != null)
        {
          q1.EmitentID = e.EmitentID;
          q1.InDateTime = DateTime.Now;
          q1.Value1 = e.Value1;
          q1.Value2 = e.Value2;
          q1.Value3 = e.Value3;
          q1.Value4 = e.Value4;
          q1.Value5 = e.Value5;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from ll in db.tLimitList
              where ids.Contains(ll.ID)
              select new
              {
                ll.ID,
                ll.EmitentID,
                FinGroup = ll.tFinInst.tFinInstGroup.FirstOrDefault(p => p.FinInstGroupTypeID == 10).Name,
                FinName = ll.tFinInst.Name,
                ll.tFinInst.INN,
                TypeName = ll.TypeID == 1 ? "ДУ" : ll.TypeID == 2 ? "CC" : "",
                ll.Value1,
                ll.Value2,
                ll.Value3,
                ll.Value4,
                ll.Value5
              };
      return q;
    }

    public bool DelLimitList(List<tLimitList> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.tLimitList.Where(p => ids.Contains(p.ID));
        db.tLimitList.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (DbUpdateException ex)
      {
        throw new Exception($"Db update Exception {ex.Message}");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool AddFinInstFinInstGroup(int EmitentID, int GroupID, int FinInstGroupTypeID)
    {
      try
      {
        var f = db.tFinInst.Find(EmitentID);
        if (f != null)
        {
          f.tFinInstGroup.Where(p => p.FinInstGroupTypeID == FinInstGroupTypeID).ToList().ForEach(p => f.tFinInstGroup.Remove(p));
          //foreach (var e in f.tFinInstGroup.Where(p => p.FinInstGroupTypeID == FinInstGroupTypeID).ToList())
          //{
          //  f.tFinInstGroup.Remove(e);
          //}
          f.tFinInstGroup.Add(db.tFinInstGroup.Find(GroupID));
          db.SaveChanges();
          return true;
        }

      }
      catch (DbUpdateException ex)
      {
        throw new Exception($"Db update Exception {ex.Message}");
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return false;
    }

    public IEnumerable<dynamic> RepLimit1(int TypeID, DateTime? dt)
    {
      //var q1 = db.tTreaty.AsEnumerable();
      //if (TypeID == 1)
      //  q1 = q1.Where(p => p.tPortfolioTreaty.Any(t => t.DateStart <= dt & t.DateFinish > dt && t.tPortfolio.Name.StartsWith("ДУ") && p.TreatyID != 13788));
      //else if (TypeID == 2)
      //  q1 = q1.Where(p => p.TreatyID == 13788);

      dt = dt ?? DateTime.Today;
      var q =
        from t in (
        from t in (
          from t in (
            from r in db.tODRests
            where r.BalAccID != 2836 /* РЕПО */
            join s in db.tSecurity.Where(p => p.Class == 2) on r.ValueID equals s.SecurityID
            join t in db.tODTurns.Where(p => p.TDate > dt) on r.ID equals t.RestID
            join tr in db.tTreaty.Where(p => (p.TreatyID == 13788 ? 2 : p.Name.StartsWith("ДУ") ? 1 : 3) == TypeID) on r.Reg3ID equals tr.TreatyID
            group new { r, t } by r.ValueID into grp
            let s = grp.Sum(p => p.t.Type * p.t.Value)
            where s != 0
            select new
            {
              SecurityID = grp.Key,
              Num = grp.Sum(p => p.t.Type * p.t.Value)
            })
          join s in db.tSecurity on t.SecurityID equals s.SecurityID
          join sr in db.tSecurityRate.Where(p => p.RateDate == dt) on t.SecurityID equals sr.SecurityID into _sr
          from sr in _sr.DefaultIfEmpty()
          select new
          {
            s.IssuerID,
            Qty = (s.Class == 0 ? 1 * -1 : 1) * t.Num * sr.Course
          })
        group t by t.IssuerID into grp
        select new
        {
          IssuerID = grp.Key,
          Qty = grp.Sum(p => p.Qty)
        })
        join l in db.tLimitList.Where(p => p.TypeID == TypeID) on t.IssuerID equals l.EmitentID into _l
        from l in _l.DefaultIfEmpty()
        join f in db.tFinInst on t.IssuerID equals f.FinInstID
        select new
        {
          f.Name,
          Value2 = (double?)l.Value2,
          Qty = t.Qty
        };
      return q;
    }

    public DateTime GetPrevWorkDate(DateTime date, int days)
    {
      return db.tWorkDate.Where(p=>p.WorkDate < date).OrderByDescending(p=>p.WorkDate).Take(2).Min(p=>p.WorkDate);
    }
  }

  public class RiskModule : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      this.Bind<IRiskRepository>().To<RiskRepository>().InRequestScope();
    }
  }

}
