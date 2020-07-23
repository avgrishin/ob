using Ninject.Modules;
using Ninject.Web.Common;
using MO5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Text.RegularExpressions;
using MO5.Helpers;
using System.Net.Mail;
using System.Data.Entity.SqlServer;
using System.Text;

namespace MO5.Areas.Code.Models
{
  public interface IEnregRepository
  {
    IEnumerable<dynamic> GetEnregList(DateTime? d1, DateTime? d2, Boolean? sd, string UserName, bool? isOnlyMy, int EnregTypeID, string sort, string dir);
    IEnumerable<dynamic> AddEnreg(List<tEnregistrement> data, int EnregTypeID, string UserName);
    IEnumerable<dynamic> UpdEnreg(List<tEnregistrement> data, string UserName, bool? isOnlyMy, bool IsAdmin, int EnregTypeID);
    bool DelEnreg(List<tEnregistrement> data, string UserName, bool? isOnlyMy, int EnregTypeID);
    IEnumerable<dynamic> getTreaties(string q, int limit);
    IEnumerable<dynamic> GetTreatyList(string filter, string sort, string dir);
    IEnumerable<dynamic> getObjClsByParent(int id);
    IEnumerable<dynamic> GetDocType(int id, int EnregTypeID);
    IEnumerable<dynamic> GetUserList(int TypeID);
    bool DelUser(List<UserList> data);
    tTreaty GetTreaty(int treatyId);
    bool IsTreatyInUser(int treatyId, int userId);
    void AddTreatyToUser(int treatyId, int userId);
    void RemoveTreatyFromUser(int treatyId, int userId);
    IEnumerable<dynamic> GetUserTreaty(int id);
    List<NotExecEnreg> GetNotExecEnreg(int EnregTypeID);
    IEnumerable<dynamic> getEMailList(string sort, string dir);
    IEnumerable<dynamic> GetDTSteps(int id);
    IEnumerable<dynamic> AddDTSteps(List<tEnregDTSteps> data);
    IEnumerable<dynamic> UpdDTSteps(List<tEnregDTSteps> data, string Name);
    bool DelDTSteps(List<tEnregDTSteps> data);

    Guid? GetEnregStepID(int id, int EnregTypeID);
    Guid? AddZeroStep(int id, int EnregTypeID);
    tEnregSteps GetEnregStep(Guid id);
    bool enregConfirm(Guid id, string Login, int EnregTypeID);
    bool enrCourriel(int id, string url, string host, int EnregTypeID);
    dynamic getEnreg(Guid id, int EnregTypeID);
    IEnumerable<dynamic> GetEnreLog(int id, int EnregID);
  }

  public class NotExecEnreg
  {
    public string Numero { get; set; }
    public string finName { get; set; }
    public string trName { get; set; }
    public DateTime? RecuDate { get; set; }
    public string DocType { get; set; }
    public string DayDogType { get; set; }
    public int? DaysDog { get; set; }
    public DateTime? DogDateEnd { get; set; }
    public string Comment { get; set; }
    public string Remarque { get; set; }
    public bool? Original { get; set; }
  }

  public class EnregRepository : IEnregRepository
  {
    private MiddleOfficeEntities db = new MiddleOfficeEntities() { };

    public IEnumerable<dynamic> GetEnregList(DateTime? d1, DateTime? d2, Boolean? sd, string UserName, bool? isOnlyMy, int EnregTypeID, string sort, string dir)
    {
      var q = (from e in db.tEnregistrement.Where(p => p.RecuDate >= d1 && p.RecuDate <= d2 && p.EnregTypeID == EnregTypeID)
               join tr in db.tTreaty on e.TreatyID equals tr.TreatyID into tr_
               from tr1 in tr_.DefaultIfEmpty()
               join f in db.tFinInst on tr1.FinInstID equals f.FinInstID into f_
               from f in f_.DefaultIfEmpty()
               from us in db.taLib.Where(a => a.LID == e.EmployeID).DefaultIfEmpty()
               from dt in db.tObjClassifier.Where(a => a.ObjClassifierID == e.DocTypeID).DefaultIfEmpty()
               select new
               {
                 id = e.ID,
                 e.Numero,
                 e.TreatyID,
                 trNameBrief = tr1.Name.Trim(),
                 ClnName = f.Name.Trim(),
                 e.RecuDate,
                 e.Tm,
                 e.Original,
                 e.ScanCopy,
                 e.FullOut,
                 IsShowFO = dt.RequiredFlag == 1,
                 e.DocTypeID,
                 DTName = dt.Name,
                 e.EmployeID,
                 EmployeNom = us.LName,
                 e.Remarque,
                 e.MethodID,
                 Method = e.tObjClassifier1.Name,
                 e.StatusID,
                 Status = e.tObjClassifier.Name,
                 IsDone = e.tObjClassifier.Comment == "1" || (e.tObjClassifier.Comment == "2" && (e.Remarque != null || e.FileNameD != null)),
                 e.DocNum,
                 e.DayDogTypeID,
                 e.DaysDog,
                 e.DaysFact,
                 e.FileName,
                 e.FileNameO,
                 e.FileNameD,
                 e.Qty,
                 DateDog = e.DayDogTypeID == 1 ? UserDbFunction.ufAddWorkDate(e.RecuDate, e.DaysDog) : SqlFunctions.DateAdd("d", e.DaysDog, e.RecuDate),
                 e.DateFact,
                 //DateFact = UserDbFunction.ufAddWorkDate(e.RecuDate, e.DaysFact),
                 DayDogType = e.DayDogTypeID == 1 ? "рабочие" : "календарные",
                 Step = (int?)e.tEnregSteps.Step,
                 IsStepConfirmed = (bool?)e.tEnregSteps.IsConfirmed,
                 e.UserName
               });
      if (sd ?? false)
        q = q.Where(p => p.IsDone == false);
      if (isOnlyMy == true)
        q = q.Where(p => p.UserName == UserName);
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> AddEnreg(List<tEnregistrement> data, int EnregTypeID, string UserName)
    {
      foreach (var e in data)
      {
        e.EnregTypeID = EnregTypeID;
        e.InDateTime = DateTime.Now;
        e.UserName = UserName;
      }
      db.tEnregistrement.AddRange(data);
      db.SaveChanges();

      var ids = data.Select(p => p.ID);
      var q = from e in db.tEnregistrement.AsNoTracking().Where(p => ids.Contains(p.ID))
              join tr in db.tTreaty on e.TreatyID equals tr.TreatyID into tr_
              from tr1 in tr_.DefaultIfEmpty()
              join f in db.tFinInst on tr1.FinInstID equals f.FinInstID into f_
              from f in f_.DefaultIfEmpty()
              from us in db.taLib.Where(a => a.LID == e.EmployeID).DefaultIfEmpty()
              from dt in db.tObjClassifier.Where(a => a.ObjClassifierID == e.DocTypeID).DefaultIfEmpty()
              select new
              {
                id = e.ID,
                e.Numero,
                e.TreatyID,
                trNameBrief = tr1.Name.Trim(),
                ClnName = f.Name.Trim(),
                e.RecuDate,
                e.Tm,
                e.Original,
                e.ScanCopy,
                e.FullOut,
                IsShowFO = dt.RequiredFlag == 1,
                e.DocTypeID,
                DTName = dt.Name,
                e.EmployeID,
                EmployeNom = us.LName,
                e.Remarque,
                e.MethodID,
                Method = e.tObjClassifier1.Name,
                e.StatusID,
                Status = e.tObjClassifier.Name,
                IsDone = e.tObjClassifier.Comment == "1" || (e.tObjClassifier.Comment == "2" && (e.Remarque != null || e.FileNameD != null)),
                e.DocNum,
                e.DayDogTypeID,
                e.DaysDog,
                e.DaysFact,
                e.FileName,
                e.FileNameO,
                e.FileNameD,
                e.Qty,
                DateDog = e.DayDogTypeID == 1 ? UserDbFunction.ufAddWorkDate(e.RecuDate, e.DaysDog) : SqlFunctions.DateAdd("d", e.DaysDog, e.RecuDate),
                e.DateFact,
                DayDogType = e.DayDogTypeID == 1 ? "рабочие" : "календарные",
                Step = (int?)e.tEnregSteps.Step,
                IsStepConfirmed = (bool?)e.tEnregSteps.IsConfirmed
              };
      return q;
    }

    public IEnumerable<dynamic> UpdEnreg(List<tEnregistrement> data, string UserName, bool? isOnlyMy, bool IsAdmin, int EnregTypeID)
    {
      foreach (var e in data)
      {
        var q1 = db.tEnregistrement.Find(e.ID);
        if (q1 != null)
        {
          if (q1.EnregTypeID == EnregTypeID)
          {
            if (isOnlyMy == false || q1.UserName == UserName)
            {
              q1.DateDog = e.DateDog;
              q1.DayDogTypeID = e.DayDogTypeID;
              q1.DaysDog = e.DaysDog;
              q1.DateFact = e.DateFact;
              q1.DaysFact = e.DaysFact;
              q1.DocTypeID = e.DocTypeID;
              q1.EmployeID = e.EmployeID;
              q1.FileName = e.FileName;
              q1.FileNameO = e.FileNameO;
              q1.FileNameD = e.FileNameD;
              q1.InDateTime = DateTime.Now;
              q1.Numero = e.Numero;
              q1.Original = e.Original;
              q1.Qty = e.Qty;
              if (IsAdmin)
              {
                q1.RecuDate = e.RecuDate;
                q1.Tm = e.Tm;
              }
              q1.Remarque = e.Remarque;
              q1.ScanCopy = e.ScanCopy;
              q1.FullOut = e.FullOut;
              q1.Temps = e.Temps;
              q1.TreatyID = e.TreatyID;
              q1.MethodID = e.MethodID;
              q1.StatusID = e.StatusID;
              q1.DocNum = e.DocNum;
              db.SaveChanges();
            }
          }
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from e in db.tEnregistrement.AsNoTracking().Where(p => ids.Contains(p.ID) && p.EnregTypeID == EnregTypeID)
              join tr in db.tTreaty on e.TreatyID equals tr.TreatyID into tr_
              from tr1 in tr_.DefaultIfEmpty()
              join f in db.tFinInst on tr1.FinInstID equals f.FinInstID into f_
              from f in f_.DefaultIfEmpty()
              from us in db.taLib.Where(a => a.LID == e.EmployeID).DefaultIfEmpty()
              from dt in db.tObjClassifier.Where(a => a.ObjClassifierID == e.DocTypeID).DefaultIfEmpty()
              select new
              {
                id = e.ID,
                e.Numero,
                e.TreatyID,
                trNameBrief = tr1.Name.Trim(),
                ClnName = f.Name.Trim(),
                e.RecuDate,
                e.Tm,
                e.Original,
                e.ScanCopy,
                e.FullOut,
                e.DocTypeID,
                DTName = dt.Name,
                e.EmployeID,
                EmployeNom = us.LName,
                e.Remarque,
                e.MethodID,
                Method = e.tObjClassifier1.Name,
                e.StatusID,
                Status = e.tObjClassifier.Name,
                IsDone = e.tObjClassifier.Comment == "1" || (e.tObjClassifier.Comment == "2" && (e.Remarque != null || e.FileNameD != null)),
                e.DocNum,
                e.DayDogTypeID,
                e.DaysDog,
                e.DaysFact,
                e.FileName,
                e.FileNameO,
                e.FileNameD,
                e.Qty,
                DateDog = e.DayDogTypeID == 1 ? UserDbFunction.ufAddWorkDate(e.RecuDate, e.DaysDog) : SqlFunctions.DateAdd("d", e.DaysDog, e.RecuDate),
                e.DateFact,
                DayDogType = e.DayDogTypeID == 1 ? "рабочие" : "календарные",
                Step = (int?)e.tEnregSteps.Step,
                IsStepConfirmed = (bool?)e.tEnregSteps.IsConfirmed
              };
      return q;
    }

    public bool DelEnreg(List<tEnregistrement> data, string UserName, bool? isOnlyMy, int EnregTypeID)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        foreach (var e in data)
        {
          var q1 = db.tEnregistrement.Find(e.ID);
          if (q1 != null)
          {
            if (q1.EnregTypeID == EnregTypeID)
            {
              if (isOnlyMy == false || q1.UserName == UserName)
              {
                db.tEnregistrement.Remove(q1);
              }
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

    public IEnumerable<dynamic> getTreaties(string q, int limit)
    {
      return (from t in db.tTreaty
                //join ttt in db.tTreatyTreatyTypes.Where(t => new int[] { 1, 2 }.Contains(t.TreatyTypeID)) on t.TreatyID equals ttt.TreatyID
              join f in db.tFinInst on t.FinInstID equals f.FinInstID
              where t.IsDisabled == false && (t.DateFinish == null || t.DateFinish > DateTime.Today) && (t.Name.Contains(q) || f.Name.Contains(q))
              orderby t.Name
              select new { id = t.TreatyID, name = f.Name.Trim(), brief = t.Name.Trim() }).Take(limit);
    }

    public IEnumerable<dynamic> GetDocType(int id, int EnregTypeID)
    {
      return (from oc in db.tObjClassifier
              where oc.ParentID == id
              where oc.NameBrief == EnregTypeID.ToString()
              orderby oc.Name
              select new
              {
                Value = oc.ObjClassifierID,
                Text = oc.Name
              });
    }
    public IEnumerable<dynamic> getObjClsByParent(int id)
    {
      return (from oc in db.tObjClassifier
              where oc.ParentID == id
              orderby oc.Name
              select new
              {
                Value = oc.ObjClassifierID,
                Text = oc.Name,
                oc.RequiredFlag,
                oc.Comment
              });
    }

    public IEnumerable<dynamic> GetUserList(int TypeID)
    {
      return from o in db.tObjClassifier
             where o.ParentID == TypeID
             select new
             {
               id = o.ObjClassifierID,
               o.Name,
               Email = o.Comment
             };
    }

    public bool DelUser(List<UserList> data)
    {
      try
      {
        var ids = data.Select(p => p.id);
        var e = db.tObjClassifier.Where(p => ids.Contains(p.ObjClassifierID));
        db.tObjClassifier.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (System.Data.Entity.Infrastructure.DbUpdateException /*ex*/)
      {
        throw new Exception("Db update Exception");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public tTreaty GetTreaty(int treatyId)
    {
      return db.tTreaty.Find(treatyId);
    }

    public bool IsTreatyInUser(int treatyId, int userId)
    {
      return db.tObjClsRelation.Any(p => p.ObjClassifierID == userId && p.ObjectID == treatyId);
    }

    public void AddTreatyToUser(int treatyId, int userId)
    {
      var q = new tObjClsRelation
      {
        Comment = "",
        ObjClassifierID = userId,
        ObjType = -612291030,
        OnDate = DateTime.Today,
        UserName = "",
        InDateTime = DateTime.Now,
        ObjectID = treatyId,
        Value = 0
      };
      db.tObjClsRelation.Add(q);
      db.SaveChanges();
    }

    public void RemoveTreatyFromUser(int treatyId, int userId)
    {
      var q = db.tObjClsRelation.Where(p => p.ObjClassifierID == userId && p.ObjectID == treatyId);
      db.tObjClsRelation.RemoveRange(q);
      db.SaveChanges();
    }

    public IEnumerable<dynamic> GetUserTreaty(int id)
    {
      var q = from t in db.tTreaty
              join f in db.tFinInst on t.FinInstID equals f.FinInstID into f_
              from f in f_.DefaultIfEmpty()
              join o in db.tObjClsRelation.Where(p => p.ObjClassifierID == id) on t.TreatyID equals o.ObjectID into o_
              from o in o_.DefaultIfEmpty()
              select new
              {
                id = t.TreatyID,
                TreatyName = t.Name,
                InstName = f.Name,
                InTreaty = o != null
              };
      return q;
    }

    //public IEnumerable<dynamic> getWorkerList()
    //{
    //  var q = from l in db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622)
    //          orderby l.LName
    //          select new { id = l.LID, name = l.LName, email = l.LName1 };

    //  return q;
    //}

    public List<NotExecEnreg> GetNotExecEnreg(int EnregTypeID)
    {
      var q = from e in db.tEnregistrement
              join tr in db.tTreaty on e.TreatyID equals tr.TreatyID
              join f in db.tFinInst on tr.FinInstID equals f.FinInstID
              from dt in db.tObjClassifier.Where(a => a.ObjClassifierID == e.DocTypeID).DefaultIfEmpty()
              from w in db.tfAddWorkDate(e.RecuDate, e.DaysDog)
              where e.EnregTypeID == EnregTypeID
              where !(e.tObjClassifier.Comment == "1" || (e.tObjClassifier.Comment == "2" && (e.Remarque != null || e.FileNameD != null)))
              where (e.DayDogTypeID == 1 ? w.Value : SqlFunctions.DateAdd("d", e.DaysDog, e.RecuDate)) < UserDbFunction.ufAddWorkDate(DateTime.Today, 7)
              select new NotExecEnreg
              {
                Numero = e.Numero,
                finName = f.Name,
                trName = tr.Name,
                RecuDate = e.RecuDate,
                DocType = dt.Name,
                DaysDog = e.DaysDog,
                DayDogType = e.DayDogTypeID == 1 ? "рабочие" : "календарные",
                DogDateEnd = e.DayDogTypeID == 1 ? w.Value : SqlFunctions.DateAdd("d", e.DaysDog, e.RecuDate),
                Comment = e.Remarque,
                Original = e.Original
              };
      return q.OrderBy(p => p.DogDateEnd).ToList();
    }

    public IEnumerable<dynamic> getEMailList(string sort, string dir)
    {
      var q = from l in db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622)
              select new { id = l.LID, name = l.LName, email = l.LName1 };

      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> GetDTSteps(int id)
    {
      var q = from dt in db.tEnregDTSteps
              where dt.DocTypeID == id
              orderby dt.Step
              select new
              {
                dt.ID,
                dt.DocTypeID,
                dt.Step,
                dt.EmailTo,
                EmailToName = UserDbFunction.ufEmailToStr(dt.EmailTo)
              };
      return q;
    }

    public IEnumerable<dynamic> AddDTSteps(List<tEnregDTSteps> data)
    {
      db.tEnregDTSteps.AddRange(data);
      db.SaveChanges();

      var ids = data.Select(p => p.ID);
      var q = from dt in db.tEnregDTSteps.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                dt.ID,
                dt.DocTypeID,
                dt.Step,
                dt.EmailTo,
                EmailToName = UserDbFunction.ufEmailToStr(dt.EmailTo)
              };
      return q;
    }

    public IEnumerable<dynamic> UpdDTSteps(List<tEnregDTSteps> data, string Name)
    {
      foreach (var e in data)
      {
        var q1 = db.tEnregDTSteps.Find(e.ID);
        if (q1 != null)
        {
          q1.EmailTo = e.EmailTo;
          q1.Step = e.Step;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from dt in db.tEnregDTSteps.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                dt.ID,
                dt.DocTypeID,
                dt.Step,
                dt.EmailTo,
                EmailToName = UserDbFunction.ufEmailToStr(dt.EmailTo)
              };
      return q;
    }

    public bool DelDTSteps(List<tEnregDTSteps> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.tEnregDTSteps.Where(p => ids.Contains(p.ID));
        db.tEnregDTSteps.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }

    public Guid? GetEnregStepID(int id, int EnregTypeID)
    {
      var o = db.tEnregistrement.Find(id);
      return o?.EnregTypeID == EnregTypeID ? o?.StepID : null;
    }

    public Guid? AddZeroStep(int id, int EnregTypeID)
    {
      var o = db.tEnregistrement.Find(id);
      if (o != null)
      {
        if (o.EnregTypeID == EnregTypeID)
        {
          if (o.StepID == null)
          {
            var s = new tEnregSteps { ID = Guid.NewGuid(), EnregID = o.ID, Step = 0, IsConfirmed = false, InDateTime = DateTime.Now };
            db.tEnregSteps.Add(s);
            db.SaveChanges();
            o.StepID = s.ID;
            db.SaveChanges();
            return s.ID;
          }
        }
      }
      return null;
    }

    public tEnregSteps GetEnregStep(Guid id)
    {
      var o = db.tEnregSteps.Find(id);
      return o;
    }

    public bool enregConfirm(Guid id, string Login, int EnregTypeID)
    {
      using (var dbTrans = db.Database.BeginTransaction())
      {
        try
        {
          var o = db.tEnregSteps.Find(id);
          if (o == null) return false;
          if (o.tEnregistrement1?.EnregTypeID != EnregTypeID) return false;
          var q2 = new tEnregistrementLog { EnregID = o.EnregID, EnregStepID = id, Login = Login, InDateTime = DateTime.Now };
          db.tEnregistrementLog.Add(q2);
          db.SaveChanges();

          if (!o.IsConfirmed)
          {

            o.IsConfirmed = true;
            o.InDateTimeC = DateTime.Now;
            o.UserName = Login;
            db.SaveChanges();
            var dts = db.tEnregDTSteps.Where(p => p.DocTypeID == o.tEnregistrement1.DocTypeID && p.Step > o.Step).OrderBy(p => p.Step).FirstOrDefault();
            if (dts != null)
            {
              var s = new tEnregSteps { ID = Guid.NewGuid(), EnregID = o.EnregID, Step = dts.Step, IsConfirmed = false, InDateTime = DateTime.Now };
              db.tEnregSteps.Add(s);
              db.SaveChanges();
              o.tEnregistrement1.StepID = s.ID;
              db.SaveChanges();
              dbTrans.Commit();
              return true;
            }
            else
            {
              o.tEnregistrement1.StatusID = db.tObjClassifier.FirstOrDefault(p => p.ParentID == 27203 && p.Comment == "1")?.ObjClassifierID;
              o.tEnregistrement1.DateFact = DateTime.Today;
              db.SaveChanges();
              dbTrans.Commit();
              return false;
            }
          }
          dbTrans.Commit();
          return false;
        }
        catch (Exception)
        {
          dbTrans.Rollback();
          throw;
        }
      }
    }

    public bool enrCourriel(int id, string url, string host, int EnregTypeID)
    {
      var q = (from e in db.tEnregistrement
               where e.ID == id && e.EnregTypeID == EnregTypeID
               join tr in db.tTreaty on e.TreatyID equals tr.TreatyID into tr_
               from tr1 in tr_.DefaultIfEmpty()
               join f in db.tFinInst on tr1.FinInstID equals f.FinInstID into f_
               from f in f_.DefaultIfEmpty()
               from dt in db.tObjClassifier.Where(a => a.ObjClassifierID == e.DocTypeID).DefaultIfEmpty()
               select new
               {
                 id = e.ID,
                 e.Numero,
                 e.RecuDate,
                 e.DocTypeID,
                 DTName = dt.Name,
                 trNameBrief = tr1.Name.Trim(),
                 ClnName = f.Name.Trim(),
                 DateDog = e.DayDogTypeID == 1 ? UserDbFunction.ufAddWorkDate(e.RecuDate, e.DaysDog) : SqlFunctions.DateAdd("d", e.DaysDog, e.RecuDate),
                 e.FileName,
                 e.FileNameO,
                 e.tEnregSteps.Step
               }).FirstOrDefault();
      var sb = new StringBuilder();
      sb.Append("<style>table{border-collapse:collapse;}td,th{border:1px solid gray;}td,span,th{font-size:.8em;font-family:'Segoe UI',Verdana,Helvetica,Sans-Serif;text-align:left;}span{font-style:italic;}th{font-size: .8em;}.r{text-align:right}</style>");
      sb.Append("<h4>Уважаемый коллега, подтвердите исполнение поручения по <a href='" + url + "'>ссылке</a>.</h4>");
      sb.Append("<table>");
      sb.Append($"<tr><td>Номер и дата поручения</td><td>{q.Numero} от {q.RecuDate:dd.MM.yyyy}</td></tr>");
      sb.Append($"<tr><td>Тип документа</td><td>{q.DTName}</td></tr>");
      sb.Append($"<tr><td>Договор</td><td>{q.trNameBrief}</td></tr>");
      sb.Append($"<tr><td>Клиент</td><td>{q.ClnName}</td></tr>");
      sb.Append($"<tr><td>Дата исполнения до</td><td>{q.DateDog:dd.MM.yyyy}</td></tr>");
      //sb.Append($"<tr><td>Скан поручения</td><td><a href='http://{host}/code/enreg/GetFile?data={q.FileName}'>Файл</a></td></tr>");
      sb.Append($"<tr><td>Скан поручения</td><td><a href='{host}?data={q.FileName}'>Файл</a></td></tr>");
      sb.Append("</table>");
      SmtpClient sc = new SmtpClient();
      MailMessage message = new MailMessage();
      message.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
      var dts = db.tEnregDTSteps.FirstOrDefault(p => p.DocTypeID == q.DocTypeID && p.Step == q.Step);
      if (host.Contains("localhost") || dts?.EmailTo == null)
      {
        message.To.Add("qbcontrol@qbfin.ru");
      }
      else
      {
        message.To.Add(dts?.EmailTo);
      }
      message.Body = sb.ToString();
      message.IsBodyHtml = true;
      message.Priority = MailPriority.High;
      message.Headers.Add("Importance", "High");
      message.Subject = "Поручения к исполнению";
      sc.Send(message);
      return true;
    }

    public dynamic getEnreg(Guid id, int EnregTypeID)
    {
      var q = (from es in db.tEnregSteps
               where es.ID == id
               let e = es.tEnregistrement1
               where e.EnregTypeID == EnregTypeID
               join tr in db.tTreaty on e.TreatyID equals tr.TreatyID into tr_
               from tr1 in tr_.DefaultIfEmpty()
               join f in db.tFinInst on tr1.FinInstID equals f.FinInstID into f_
               from f in f_.DefaultIfEmpty()
               from dt in db.tObjClassifier.Where(a => a.ObjClassifierID == e.DocTypeID).DefaultIfEmpty()
               select new
               {
                 id = es.ID,
                 es.EnregID,
                 e.Numero,
                 e.RecuDate,
                 e.DocTypeID,
                 DTName = dt.Name,
                 trNameBrief = tr1.Name.Trim(),
                 ClnName = f.Name.Trim(),
                 DateDog = e.DayDogTypeID == 1 ? UserDbFunction.ufAddWorkDate(e.RecuDate, e.DaysDog) : SqlFunctions.DateAdd("d", e.DaysDog, e.RecuDate),
                 e.FileName,
                 e.FileNameO,
                 es.Step,
                 es.IsConfirmed
               }).FirstOrDefault().ToExpando();
      return q;
    }

    public IEnumerable<dynamic> GetEnreLog(int id, int EnregTypeID)
    {
      var q = from el in db.tEnregistrementLog
              where el.EnregID == id && el.tEnregistrement.EnregTypeID == EnregTypeID
              select new
              {
                id = el.ID,
                el.tEnregSteps.Step,
                el.Login,
                el.InDateTime
              };
      return q;
    }

    public IEnumerable<dynamic> GetTreatyList(string filter, string sort, string dir)
    {
      var q1 = db.tTreaty.Where(p => 1 == 1);
      if (!string.IsNullOrEmpty(filter))
        q1 = q1.Where(p => p.Name.Contains(filter) || p.tFinInst.Name.Contains(filter));
      var q = from tr in q1
              select new
              {
                tr.TreatyID,
                tr.Name,
                tr.FinInstID,
                ClientName = tr.tFinInst.Name,
                tr.IsDisabled,
                tr.DateStart,
                tr.DateFinish
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

  }

  public class UserList
  {
    public int id { get; set; }
    public string Name { get; set; }
  }

  public class EnregModule : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      this.Bind<IEnregRepository>().To<EnregRepository>().InRequestScope();
    }
  }

}