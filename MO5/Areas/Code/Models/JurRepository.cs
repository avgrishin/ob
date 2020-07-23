using MO5.Helpers;
using MO5.Models;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Dynamic;

namespace MO5.Areas.Code.Models
{
  public interface IJurRepository
  {
    IEnumerable<dynamic> getWarrantList(string sort, string dir, DateTime? d1, DateTime? d2, int? type, bool? all);
    IEnumerable<dynamic> addWarrant(List<tWarrant> data, string UserName);
    IEnumerable<dynamic> updWarrant(List<tWarrant> data, string UserName);
    bool delWarrant(List<tWarrant> data);
    List<tWarrant> warrantCourriel();
    tWarrant getWarrant(int? ID);
    bool closeWarrant(int? ID, string UserName);
    List<string> getJuristList();

    IEnumerable<dynamic> GetReestrList(string sort, string dir, DateTime? d1, DateTime? d2, int? InstOwnerID);
    IEnumerable<dynamic> AddReestr(List<tReestr> data);
    IEnumerable<dynamic> UpdReestr(List<tReestr> data);
    bool DelReestr(List<tReestr> data);
    tReestr GetReestr(int id);
    IEnumerable<dynamic> GetObjClsByParent(int id);
    IEnumerable<dynamic> getEMailList(string sort, string dir);

    IEnumerable<dynamic> GetEdoList(string sort, string dir, DateTime? d1, DateTime? d2);
    IEnumerable<dynamic> AddEdo(List<tEDO> data, string UserName);
    IEnumerable<dynamic> UpdEdo(List<tEDO> data, string UserName);
    bool DelEdo(List<tEDO> data);
    IEnumerable<IGrouping<string, tEDO>> EdoCourriel();
    tEDO GetEDO(int id);
  }

  public class JurRepository : IJurRepository
  {
    private MiddleOfficeEntities db = new MiddleOfficeEntities() { };

    public IEnumerable<dynamic> getWarrantList(string sort, string dir, DateTime? d1, DateTime? d2, int? type, bool? all)
    {
      var q = from i in db.tWarrant
              select new
              {
                i.ID,
                i.Nomer,
                i.Principal,
                i.Confidant,
                i.Place,
                i.DateB,
                i.DateE,
                i.Functions,
                i.Initiator,
                i.DateCancel,
                i.UserName,
                i.FileName,
                i.FileName2,
                i.Comment
              };
      if (d1.HasValue && type == 0)
      {
        q = q.Where(a => a.DateB >= d1);
      }
      if (d2.HasValue && type == 0)
      {
        q = q.Where(a => a.DateB <= d2);
      }
      if (d1.HasValue && type == 1)
      {
        q = q.Where(a => a.DateE >= d1);
      }
      if (d2.HasValue && type == 1)
      {
        q = q.Where(a => a.DateE <= d2);
      }
      if (all == false)
      {
        q = q.Where(a => a.DateCancel == null);
      }
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> addWarrant(List<tWarrant> data, string UserName)
    {
      foreach (var e in data)
      {
        e.InDateTime = DateTime.Now;
        e.UserName = UserName.Left(60);
      }
      db.tWarrant.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.ID);
      var q = from i in db.tWarrant.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                i.ID,
                i.Nomer,
                i.Principal,
                i.Confidant,
                i.Place,
                i.DateB,
                i.DateE,
                i.Functions,
                i.Initiator,
                i.DateCancel,
                i.UserName,
                i.Comment
              };
      return q;
    }

    public IEnumerable<dynamic> updWarrant(List<tWarrant> data, string UserName)
    {
      foreach (var e in data.Where(p => p.ID > 0))
      {
        var q1 = db.tWarrant.Find(e.ID);
        if (q1 != null)
        {
          q1.Confidant = e.Confidant;
          q1.DateB = e.DateB;
          q1.DateCancel = e.DateCancel;
          q1.DateE = e.DateE;
          q1.Functions = e.Functions;
          q1.InDateTime = DateTime.Now;
          q1.Initiator = e.Initiator;
          q1.Nomer = e.Nomer;
          q1.Place = e.Place;
          q1.Principal = e.Principal;
          q1.UserName = UserName.Left(60);
          q1.FileName = e.FileName;
          q1.FileName2 = e.FileName2;
          q1.Comment = e.Comment;
          db.SaveChanges();
        }
      }

      var ids = data.Select(p => p.ID);
      var q = from i in db.tWarrant.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                i.ID,
                i.Nomer,
                i.Principal,
                i.Confidant,
                i.Place,
                i.DateB,
                i.DateE,
                i.Functions,
                i.Initiator,
                i.DateCancel,
                i.UserName,
                i.FileName,
                i.FileName2,
                i.Comment
              };
      return q;
    }

    public bool delWarrant(List<tWarrant> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.tWarrant.Where(p => ids.Contains(p.ID));
        db.tWarrant.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }

    public List<tWarrant> warrantCourriel()
    {
      //var q = (from i in db.tWarrant.Where(p => p.DateCancel == null && p.DateE <= DateTime.Today.AddDays(7))
      //         select new
      //         {
      //           i.ID,
      //           i.Nomer,
      //           i.Principal,
      //           i.Confidant,
      //           i.Place,
      //           i.DateB,
      //           i.DateE,
      //           i.Functions,
      //           i.Initiator,
      //           i.DateCancel
      //         }).ToList();
      var d = DateTime.Today.AddDays(7);
      return db.tWarrant.Where(p => p.DateCancel == null && p.DateE <= d /*SqlFunctions.DateAdd("d", 7, DateTime.Today)*/).OrderBy(p => p.DateE).ToList();
    }

    public tWarrant getWarrant(int? ID)
    {
      var q = (from i in db.tWarrant
               where i.ID == ID
               select i
               ).FirstOrDefault();
      return q;
    }

    public bool closeWarrant(int? ID, string UserName)
    {
      if (ID.HasValue)
      {
        try
        {
          var q1 = db.tWarrant.Where(p => p.ID == ID).First();
          if (q1 != null)
          {
            if (q1.DateCancel == null)
            {
              q1.DateCancel = DateTime.Today;
              q1.InDateTime = DateTime.Now;
              q1.UserName = UserName;
              db.SaveChanges();
            }
          }
          return true;
        }
        catch (Exception /*ex*/)
        {
          return false;
        }
      }
      return false;
    }

    public List<string> getJuristList()
    {
      return db.tObjClassifier.Where(p => p.ParentID == 26189).Select(p => p.Comment).ToList();
    }

    public IEnumerable<dynamic> GetReestrList(string sort, string dir, DateTime? d1, DateTime? d2, int? InstOwnerID)
    {
      var q1 = db.tReestr.Where(p => 1 == 1);
      if (InstOwnerID.HasValue)
        q1 = q1.Where(p => p.InstOwnerID == InstOwnerID);
      var q = from i in q1
              select new
              {
                i.ID,
                i.Nomer,
                i.DateR,
                i.EmailTo,
                EmailToName = UserDbFunction.ufEmailToStr(i.EmailTo),
                i.FileName,
                i.InstOwnerID,
                i.StatusID,
                i.Theme,
                InstOwner = i.tObjClassifier.NameBrief
              };
      if (d1.HasValue) q = q.Where(a => a.DateR >= d1);
      if (d2.HasValue) q = q.Where(a => a.DateR <= d2);

      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> AddReestr(List<tReestr> data)
    {
      foreach (var e in data)
      {
        e.InDateTime = DateTime.Now;
      }
      db.tReestr.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.ID);
      var q = from i in db.tReestr.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                i.ID,
                i.Nomer,
                i.DateR,
                i.EmailTo,
                EmailToName = UserDbFunction.ufEmailToStr(i.EmailTo),
                i.FileName,
                i.InstOwnerID,
                i.StatusID,
                i.Theme,
                InstOwner = i.tObjClassifier.NameBrief
              };
      return q;
    }

    public IEnumerable<dynamic> UpdReestr(List<tReestr> data)
    {
      foreach (var e in data.Where(p => p.ID > 0))
      {
        var q1 = db.tReestr.Find(e.ID);
        if (q1 != null)
        {
          q1.DateR = e.DateR;
          q1.EmailTo = e.EmailTo;
          q1.FileName = e.FileName;
          q1.InDateTime = DateTime.Now;
          q1.InstOwnerID = e.InstOwnerID;
          q1.Nomer = e.Nomer;
          q1.StatusID = e.StatusID;
          q1.Theme = e.Theme;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from i in db.tReestr.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                i.ID,
                i.Nomer,
                i.DateR,
                i.EmailTo,
                EmailToName = UserDbFunction.ufEmailToStr(i.EmailTo),
                i.FileName,
                i.InstOwnerID,
                i.StatusID,
                i.Theme,
                InstOwner = i.tObjClassifier.NameBrief
              };
      return q;
    }

    public bool DelReestr(List<tReestr> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.tReestr.Where(p => ids.Contains(p.ID));
        db.tReestr.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }

    public IEnumerable<dynamic> GetObjClsByParent(int id)
    {
      return (from oc in db.tObjClassifier
              where oc.ParentID == id
              select new
              {
                id = oc.ObjClassifierID,
                name = oc.Name
              });
    }

    public IEnumerable<dynamic> getEMailList(string sort, string dir)
    {
      var q = from l in db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622)
              select new { id = l.LID, name = l.LName, email = l.LName1 };

      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> GetEdoList(string sort, string dir, DateTime? d1, DateTime? d2)
    {
      var q1 = db.tEDO.Where(p => 1 == 1);
      if (d1.HasValue) q1 = q1.Where(a => a.Srok >= d1);
      if (d2.HasValue) q1 = q1.Where(a => a.Srok <= d2);
      var q = from i in q1
              select new
              {
                i.ID,
                i.Crypto,
                i.DateCancel,
                i.EmailTo,
                EmailToName = UserDbFunction.ufEmailToStr(i.EmailTo),
                i.Department,
                i.EDO,
                i.FIO_owner,
                i.FIO_resp,
                i.InDateTime,
                i.IsCancel,
                i.Publisher,
                i.Srok,
                i.Storage,
                i.FileName,
                i.UserName
              };

      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> AddEdo(List<tEDO> data, string UserName)
    {
      foreach (var e in data)
      {
        e.InDateTime = DateTime.Now;
        e.UserName = UserName.Left(60);
      }
      db.tEDO.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.ID);
      var q = from i in db.tEDO.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                i.ID,
                i.Crypto,
                i.DateCancel,
                i.EmailTo,
                EmailToName = UserDbFunction.ufEmailToStr(i.EmailTo),
                i.Department,
                i.EDO,
                i.FIO_owner,
                i.FIO_resp,
                i.InDateTime,
                i.IsCancel,
                i.Publisher,
                i.Srok,
                i.Storage,
                i.FileName,
                i.UserName
              };
      return q;
    }

    public IEnumerable<dynamic> UpdEdo(List<tEDO> data, string UserName)
    {
      foreach (var e in data.Where(p => p.ID > 0))
      {
        var q1 = db.tEDO.Find(e.ID);
        if (q1 != null)
        {
          q1.Crypto = e.Crypto;
          q1.EmailTo = e.EmailTo;
          q1.DateCancel = e.DateCancel;
          q1.UserName = UserName;
          q1.InDateTime = DateTime.Now;
          q1.Department = e.Department;
          q1.EDO = e.EDO;
          q1.FileName = e.FileName;
          q1.FIO_owner = e.FIO_owner;
          q1.FIO_resp = e.FIO_resp;
          q1.IsCancel = e.IsCancel;
          q1.Publisher = e.Publisher;
          q1.Srok = e.Srok;
          q1.Storage = e.Storage;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from i in db.tEDO.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                i.ID,
                i.Crypto,
                i.DateCancel,
                i.EmailTo,
                EmailToName = UserDbFunction.ufEmailToStr(i.EmailTo),
                i.Department,
                i.EDO,
                i.FileName,
                i.FIO_owner,
                i.FIO_resp,
                i.InDateTime,
                i.IsCancel,
                i.Publisher,
                i.Srok,
                i.Storage,
                i.UserName
              };
      return q;
    }

    public bool DelEdo(List<tEDO> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.tEDO.Where(p => ids.Contains(p.ID));
        db.tEDO.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }

    public IEnumerable<IGrouping<string, tEDO>> EdoCourriel()
    {
      var q = (from i in
                 ((from i in db.tEDO.Where(p => p.DateCancel == null && p.IsCancel != true && p.Srok <= SqlFunctions.DateAdd("d", 21, DateTime.Today))
                   select i).ToList())
               from e1 in (i.EmailTo.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).AsQueryable()).Distinct()
               orderby i.Srok
               select new tEDO
               {
                 ID = i.ID,
                 Crypto = i.Crypto,
                 Department = i.Department,
                 EDO = i.EDO,
                 FIO_owner = i.FIO_owner,
                 FIO_resp = i.FIO_resp,
                 Publisher = i.Publisher,
                 Srok = i.Srok,
                 Storage = i.Storage,
                 EmailTo = e1
               }).GroupBy(l => l.EmailTo);
      return q;

    }

    public tEDO GetEDO(int id)
    {
      return db.tEDO.Find(id);
    }

    public tReestr GetReestr(int id)
    {
      return db.tReestr.Find(id);
    }
  }

  public class JurModule : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      this.Bind<IJurRepository>().To<JurRepository>().InRequestScope();
    }
  }

}