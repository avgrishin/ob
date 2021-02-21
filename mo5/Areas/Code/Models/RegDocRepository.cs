using MO5.Helpers;
using MO5.Models;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text.RegularExpressions;

namespace MO5.Areas.Code.Models
{
  public interface IRegDocRepository
  {
    IEnumerable<dynamic> getRegDocList(int? OwnerID, string sort, string dir, DateTime? d1, DateTime? d2, int? type, Boolean? sd, bool? Direction, string UserName, bool IsAdmin);
    IEnumerable<dynamic> addRegDoc(List<tRegDoc> data, string UserName);
    IEnumerable<dynamic> updRegDoc(List<tRegDoc> data, int? OwnerID, string UserName);
    bool delRegDoc(List<tRegDoc> data, int? OwnerID);
    IGrouping<string, RegDocEmail>[] regdocCourriel(int? id, string host);
    tRegDoc getRegDoc(int? ID);
    IEnumerable<dynamic> getEMailList(string sort, string dir);
    IEnumerable<ObjCls> GetObjClsByParent(int id);

    string getNextRegNum1();

    IEnumerable<dynamic> getRegDocContrList(string filter, string sort, string dir);
    IEnumerable<dynamic> addRegDocContr(List<tRegDocContr> data);
    IEnumerable<dynamic> updRegDocContr(List<tRegDocContr> data);
    bool delRegDocContr(List<tRegDocContr> data);

  }

  public class RegDocRepository : IRegDocRepository
  {
    private readonly MiddleOfficeEntities db = new MiddleOfficeEntities() { };
    private readonly IConfigurationProvider _configProvider;

    public RegDocRepository(IConfigurationProvider configProvider)
    {
      _configProvider = configProvider;
    }
    public IEnumerable<dynamic> getRegDocList(int? OwnerID, string sort, string dir, DateTime? d1, DateTime? d2, int? type, Boolean? sd, bool? Direction, string UserName, bool IsAdmin)
    {
      var q1 = db.tRegDoc.AsQueryable();
      if (!IsAdmin)
      {
        var email = (from u in db.aspnet_Users
                     where u.UserName == UserName
                     select u.aspnet_Membership.Email)
                        .FirstOrDefault();
        q1 = q1.Where(p => p.EmailTo.Contains(email) || p.EmailCc.Contains(email));
      }
      if (OwnerID.HasValue)
        q1 = q1.Where(p => p.InstOwnerID == OwnerID);
      if (Direction.HasValue)
        q1 = q1.Where(p => p.Direction == Direction);
      if (type == 0)
      {
        if (d1.HasValue)
        {
          q1 = q1.Where(a => a.ODate >= d1);
        }
        if (d2.HasValue)
        {
          q1 = q1.Where(a => a.ODate <= d2);
        }
      }
      else if (type == 1)
      {
        if (d1.HasValue)
        {
          q1 = q1.Where(a => a.TDate >= d1);
        }
        if (d2.HasValue)
        {
          q1 = q1.Where(a => a.TDate <= d2);
        }
      }
      else if (type == 2)
      {
        if (d1.HasValue)
        {
          q1 = q1.Where(a => a.ADate >= d1);
        }
        if (d2.HasValue)
        {
          q1 = q1.Where(a => a.ADate <= d2);
        }
      }
      if (sd == true)
        q1 = q1.Where(a => a.ADate == null);
      var q = from i in q1
              join oc in db.tObjClassifier on i.TypeID equals oc.ObjClassifierID into oc_
              from oc in oc_.DefaultIfEmpty()
              join c2 in db.tObjClassifier on i.InstOwnerID equals c2.ObjClassifierID into _c2
              from c2 in _c2.DefaultIfEmpty()
              join li in db.taLib on i.OrigUserID equals li.LID into li_
              from li in li_.DefaultIfEmpty()
              select new
              {
                i.Id,
                i.ADate,
                i.ANum,
                i.Directed,
                i.ContrID,
                i.tRegDocContr.Name,
                i.EmailTo,
                i.EmailCc,
                i.InDateTime,
                i.ODate,
                i.RegNum,
                i.TDate,
                i.Theme,
                i.TypeID,
                TypeName = oc.Name,
                i.InstOwnerID,
                InstOwner = c2.Name,
                i.FileNameI,
                i.FileNameO,
                i.LotusLinkI,
                i.UserName,
                i.Comment,
                i.Resolution,
                i.DocDate,
                i.DocNum,
                i.IsAcquaintance,
                i.IsComplaint,
                i.Direction,
                i.OrigUserID,
                OrigUser = li.LName,
                i.IsClientP,
                i.IsComplaintN
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));

      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      return q.ToList().Select(i => new
      {
        i.Id,
        i.ADate,
        i.ANum,
        i.Directed,
        i.Name,
        i.ContrID,
        i.EmailTo,
        i.EmailCc,
        i.InDateTime,
        i.ODate,
        i.RegNum,
        i.TDate,
        i.Theme,
        i.TypeID,
        i.TypeName,
        i.FileNameI,
        i.FileNameO,
        i.LotusLinkI,
        i.UserName,
        i.Comment,
        i.Resolution,
        i.DocDate,
        i.DocNum,
        i.IsAcquaintance,
        i.IsComplaint,
        i.InstOwnerID,
        i.InstOwner,
        EmailToName = string.Join(", ", ql.Where(f => i.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        EmailCcName = string.Join(", ", ql.Where(f => i.EmailCc.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        i.Direction,
        i.OrigUserID,
        i.OrigUser,
        i.IsClientP,
        i.IsComplaintN
      });
    }

    public IEnumerable<dynamic> addRegDoc(List<tRegDoc> data, string UserName)
    {
      foreach (var e in data)
      {
        e.InDateTime = DateTime.Now;
        e.UserName = UserName.Left(60);
      }
      db.tRegDoc.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.Id);

      var q = from i in db.tRegDoc.AsNoTracking().Where(p => ids.Contains(p.Id))
              join oc in db.tObjClassifier on i.TypeID equals oc.ObjClassifierID into oc_
              from oc in oc_.DefaultIfEmpty()
              join c2 in db.tObjClassifier on i.InstOwnerID equals c2.ObjClassifierID into _c2
              from c2 in _c2.DefaultIfEmpty()
              join li in db.taLib on i.OrigUserID equals li.LID into li_
              from li in li_.DefaultIfEmpty()
              select new
              {
                i.Id,
                i.ADate,
                i.ANum,
                i.Directed,
                i.ContrID,
                i.tRegDocContr.Name,
                i.EmailTo,
                i.EmailCc,
                i.InDateTime,
                i.ODate,
                i.RegNum,
                i.TDate,
                i.Theme,
                i.TypeID,
                TypeName = oc.Name,
                i.InstOwnerID,
                InstOwner = c2.Name,
                i.FileNameI,
                i.FileNameO,
                i.LotusLinkI,
                i.UserName,
                i.Comment,
                i.Resolution,
                i.DocDate,
                i.DocNum,
                i.IsAcquaintance,
                i.IsComplaint,
                i.Direction,
                i.OrigUserID,
                OrigUser = li.LName,
                i.IsClientP,
                i.IsComplaintN
              };
      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      return q.ToList().Select(i => new
      {
        i.Id,
        i.ADate,
        i.ANum,
        i.Directed,
        i.Name,
        i.ContrID,
        i.EmailTo,
        i.EmailCc,
        i.InDateTime,
        i.ODate,
        i.RegNum,
        i.TDate,
        i.Theme,
        i.TypeID,
        i.TypeName,
        i.FileNameI,
        i.FileNameO,
        i.LotusLinkI,
        i.UserName,
        i.Comment,
        i.Resolution,
        i.DocDate,
        i.DocNum,
        i.IsAcquaintance,
        i.IsComplaint,
        i.InstOwnerID,
        i.InstOwner,

        EmailToName = string.Join(", ", ql.Where(f => i.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        EmailCcName = string.Join(", ", ql.Where(f => i.EmailCc.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        i.Direction,
        i.OrigUserID,
        i.OrigUser,
        i.IsClientP,
        i.IsComplaintN
      });
    }

    public IEnumerable<dynamic> updRegDoc(List<tRegDoc> data, int? OwnerID, string UserName)
    {
      foreach (var e in data.Where(p => p.Id > 0))
      {
        var q1 = db.tRegDoc.Find(e.Id);
        if (q1 != null)
        {
          if ((OwnerID ?? q1.InstOwnerID) == q1.InstOwnerID)
          {
            q1.ADate = e.ADate;
            q1.InstOwnerID = e.InstOwnerID;
            q1.ANum = e.ANum;
            q1.Comment = e.Comment;
            q1.ContrID = e.ContrID;
            q1.Directed = e.Directed;
            q1.DocDate = e.DocDate;
            q1.DocNum = e.DocNum;
            q1.EmailCc = e.EmailCc;
            q1.EmailTo = e.EmailTo;
            q1.FileNameI = e.FileNameI;
            q1.FileNameO = e.FileNameO;
            q1.IsAcquaintance = e.IsAcquaintance;
            q1.IsComplaint = e.IsComplaint;
            q1.IsComplaintN = e.IsComplaintN;
            q1.IsClientP = e.IsClientP;
            q1.LotusLinkI = e.LotusLinkI;
            q1.ODate = e.ODate;
            q1.RegNum = e.RegNum;
            q1.Resolution = e.Resolution;
            q1.TDate = e.TDate;
            q1.Theme = e.Theme;
            q1.TypeID = e.TypeID;
            q1.InDateTime = DateTime.Now;
            q1.UserName = UserName.Left(60);
            q1.Direction = e.Direction;
            q1.OrigUserID = e.OrigUserID;
          }
        }
        db.SaveChanges();
      }

      var ids = data.Select(p => p.Id);

      var q = from i in db.tRegDoc.AsNoTracking().Where(p => ids.Contains(p.Id))
              join oc in db.tObjClassifier on i.TypeID equals oc.ObjClassifierID into oc_
              from oc in oc_.DefaultIfEmpty()
              join c2 in db.tObjClassifier on i.InstOwnerID equals c2.ObjClassifierID into _c2
              from c2 in _c2.DefaultIfEmpty()
              join li in db.taLib on i.OrigUserID equals li.LID into li_
              from li in li_.DefaultIfEmpty()
              select new
              {
                i.Id,
                i.ADate,
                i.ANum,
                i.Directed,
                i.ContrID,
                i.tRegDocContr.Name,
                i.EmailTo,
                i.EmailCc,
                i.InDateTime,
                i.ODate,
                i.RegNum,
                i.TDate,
                i.Theme,
                i.TypeID,
                TypeName = oc.Name,
                i.InstOwnerID,
                InstOwner = c2.Name,
                i.FileNameI,
                i.FileNameO,
                i.LotusLinkI,
                i.UserName,
                i.Comment,
                i.Resolution,
                i.DocDate,
                i.DocNum,
                i.IsAcquaintance,
                i.IsComplaint,
                i.Direction,
                i.OrigUserID,
                OrigUser = li.LName,
                i.IsClientP,
                i.IsComplaintN
              };

      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      return q.ToList().Select(i => new
      {
        i.Id,
        i.ADate,
        i.ANum,
        i.Directed,
        i.Name,
        i.ContrID,
        i.EmailTo,
        i.EmailCc,
        i.InDateTime,
        i.ODate,
        i.RegNum,
        i.TDate,
        i.Theme,
        i.TypeID,
        i.TypeName,
        i.FileNameI,
        i.FileNameO,
        i.LotusLinkI,
        i.UserName,
        i.Comment,
        i.Resolution,
        i.DocDate,
        i.DocNum,
        i.IsAcquaintance,
        i.IsComplaint,
        i.InstOwnerID,
        i.InstOwner,
        EmailToName = string.Join(", ", ql.Where(f => i.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        EmailCcName = string.Join(", ", ql.Where(f => i.EmailCc.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        i.Direction,
        i.OrigUserID,
        i.OrigUser,
        i.IsClientP,
        i.IsComplaintN
      });
    }

    public bool delRegDoc(List<tRegDoc> data, int? OwnerID)
    {
      try
      {
        var ids = data.Select(p => p.Id);
        var e = db.tRegDoc.Where(p => ids.Contains(p.Id) && (OwnerID ?? p.InstOwnerID) == p.InstOwnerID);
        var i = db.tRegDoc.RemoveRange(e);
        db.SaveChanges();

        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }

    public IEnumerable<string> regdocEmail(int id)
    {
      var q = db.tRegDoc.Find(id);
      if (q != null)
      {
        return ((q.EmailTo + "," + q.EmailCc).Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).AsQueryable()).Distinct();
      }
      return null;
    }

    public IGrouping<string, RegDocEmail>[] regdocCourriel(int? id, string host)
    {
      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      var q = (from i in
                 ((from i in db.tRegDoc.Where(p => (id == null && p.ADate == null && p.IsAcquaintance == false && p.Direction == false) || p.Id == id)
                   join oc in db.tObjClassifier on i.TypeID equals oc.ObjClassifierID into oc_
                   from oc in oc_.DefaultIfEmpty()
                   join c2 in db.tObjClassifier on i.InstOwnerID equals c2.ObjClassifierID into _c2
                   from c2 in _c2.DefaultIfEmpty()
                   join li in db.taLib on i.OrigUserID equals li.LID into li_
                   from li in li_.DefaultIfEmpty()
                   select new
                   {
                     i.Id,
                     i.ADate,
                     i.ANum,
                     Directed = i.tRegDocContr.Name,
                     i.EmailTo,
                     i.EmailCc,
                     i.InDateTime,
                     i.ODate,
                     i.RegNum,
                     i.TDate,
                     i.Theme,
                     i.TypeID,
                     i.FileNameI,
                     i.LotusLinkI,
                     TypeName = oc.Name,
                     i.InstOwnerID,
                     InstOwner = c2.Name,
                     i.UserName,
                     i.Comment,
                     i.Resolution,
                     i.DocDate,
                     i.DocNum,
                     i.IsAcquaintance,
                     i.IsComplaint,
                     OrigUser = li.LName,
                     i.IsClientP,
                     i.IsComplaintN
                   }).ToList())
               from e1 in ((i.EmailTo + "," + i.EmailCc + (
                i.InstOwnerID == 26187/*ИК*/? _configProvider.GetValue<string>("regdocCourrielIK") :
                i.InstOwnerID == 26188/*УК*/? _configProvider.GetValue<string>("regdocCourrielUK") : "")).Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).AsQueryable()).Distinct()
               orderby i.TDate
               select new RegDocEmail
               {
                 Id = i.Id,
                 ADate = i.ADate,
                 ANum = i.ANum,
                 Directed = i.Directed,
                 EmailTo = e1,
                 EmailToName = string.Join(", ", ql.Where(f => i.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
                 EmailCcName = string.Join(", ", ql.Where(f => i.EmailCc.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
                 InDateTime = i.InDateTime,
                 ODate = i.ODate,
                 RegNum = i.RegNum,
                 TDate = i.TDate,
                 Theme = i.Theme,
                 FileNameI = i.FileNameI,
                 TypeID = i.TypeID,
                 TypeName = i.TypeName,
                 InstOwnerID = i.InstOwnerID,
                 InstOwner = i.InstOwner,
                 UserName = i.UserName,
                 Comment = i.Comment,
                 Resolution = i.Resolution,
                 DocDate = i.DocDate,
                 DocNum = i.DocNum,
                 IsAcquaintance = i.IsAcquaintance,
                 IsComplaint = i.IsComplaint,
                 OrigUser = i.OrigUser,
                 IsClientP = i.IsClientP,
                 IsComplaintN = i.IsComplaintN
               }).GroupBy(l => l.EmailTo);
      return q.ToArray();
    }

    public tRegDoc getRegDoc(int? Id)
    {
      var q = (
        from i in db.tRegDoc
        where i.Id == Id
        select i
      ).FirstOrDefault();
      return q;
    }

    public string getNextRegNum1()
    {
      var rg = new Regex("(.*[^\\d])(\\d+)");
      var q = (from tr in db.tRegDoc
               group tr by 1 into g
               select new
               {
                 RegNum = g.Max(t => t.RegNum)
               }).FirstOrDefault();
      var rn = string.Format("УК{0}-001", DateTime.Today.Year % 100);
      if (q != null)
      {
        var m = rg.Match(q.RegNum);
        if (m.Success)
          rn = m.Groups[1].Value + (int.Parse(m.Groups[2].Value) + 1001).ToString().Right(3);
      }
      return rn;
    }

    public IEnumerable<dynamic> getEMailList(string sort, string dir)
    {
      var q = from l in db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622)
              select new { id = l.LID, name = l.LName, email = l.LName1 };

      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<ObjCls> GetObjClsByParent(int id)
    {
      return (from oc in db.tObjClassifier
              where oc.ParentID == id
              select new ObjCls
              {
                id = oc.ObjClassifierID,
                name = oc.Name,
                comment = oc.Comment
              });
    }

    public IEnumerable<dynamic> getRegDocContrList(string filter, string sort, string dir)
    {
      var q = db.tRegDocContr.Where(p => 1 == 1);
      if (!string.IsNullOrEmpty(filter))
        q = q.Where(p => p.Name.Contains(filter));
      q = q.OrderBy((sort ?? "Name") + (dir == "DESC" ? " descending" : ""));
      return q.Select(p => new { p.Id, p.Name });
    }

    public IEnumerable<dynamic> addRegDocContr(List<tRegDocContr> data)
    {
      db.tRegDocContr.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.Id);
      return db.tRegDocContr.Where(p => ids.Contains(p.Id)).Select(p => new { p.Id, p.Name });
    }

    public IEnumerable<dynamic> updRegDocContr(List<tRegDocContr> data)
    {
      foreach (var e in data.Where(p => p.Id > 0))
      {
        var q1 = db.tRegDocContr.Find(e.Id);
        if (q1 != null)
        {
          q1.Name = e.Name;
        }
        db.SaveChanges();
      }
      var ids = data.Select(p => p.Id);
      return db.tRegDocContr.Where(p => ids.Contains(p.Id)).Select(p => new { p.Id, p.Name });
    }

    public bool delRegDocContr(List<tRegDocContr> data)
    {
      try
      {
        var ids = data.Select(p => p.Id);
        var e = db.tRegDocContr.Where(p => ids.Contains(p.Id));
        db.tRegDocContr.RemoveRange(e);
        db.SaveChanges();

        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }
  }
  public class ObjCls
  {
    public int id { get; set; }
    public string name { get; set; }
    public string comment { get; set; }
  }
  public class RegDocModule : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      this.Bind<IRegDocRepository>().To<RegDocRepository>().InRequestScope();
    }
  }
}


