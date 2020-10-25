using Ninject.Modules;
using Ninject.Web.Common;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using MO5.Models;
using System;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Infrastructure;
using MO5.Helpers;

namespace MO5.Areas.Code.Models
{
  public interface IEnvoiRepository
  {
    IEnumerable<dynamic> getEnvoiList(int? OwnerID, int TypeID, bool? isAuto, bool? IsActive, string sort, string dir);
    IEnumerable<dynamic> addEnvoi(List<tEnvoi> data);
    IEnumerable<dynamic> updEnvoi(List<tEnvoi> data);
    bool delEnvoi(List<tEnvoi> data);
    IEnumerable<dynamic> GetObjClsByParent(int id);
    bool envoyerCourriel(int id, string Comment, string host);
    bool envoyerCourriels(DateTime? dt, string host);

    IEnumerable<dynamic> getConseilList(DateTime? d1, DateTime? d2, int? type, Boolean? nopen, string sort, string dir);
    IEnumerable<dynamic> addConseil(List<tConseil> data);
    IEnumerable<dynamic> updConseil(List<tConseil> data);
    bool delConseil(List<tConseil> data);
    bool conseilCourriel(int? id, string host);
    bool conseilEnabledCourriel();
    bool conseilCourriels(string host);
    bool conseilCourrielAll();
    IEnumerable<dynamic> getCPriorite();

    IEnumerable<dynamic> getConseilHoraire(int? id);
    IEnumerable<dynamic> addConseilHoraire(List<tConseilHoraire> data);
    IEnumerable<dynamic> updConseilHoraire(List<tConseilHoraire> data);
    bool delConseilHoraire(List<tConseilHoraire> data);

    IEnumerable<dynamic> getEnvoiHoraire(int? id);
    IEnumerable<dynamic> addEnvoiHoraire(List<tEnvoiHoraire> data);
    IEnumerable<dynamic> updEnvoiHoraire(List<tEnvoiHoraire> data);
    bool delEnvoiHoraire(List<tEnvoiHoraire> data);
    IEnumerable<dynamic> getEnvoiHoraireType();

    IEnumerable<dynamic> getEnvoiExecList(int? OwnerID, int TypeID, DateTime? d1, DateTime? d2, bool? IsExec, string sort, string dir);
    IEnumerable<dynamic> addEnvoiExec(List<tEnvoiExec> data);
    IEnumerable<dynamic> updEnvoiExec(List<tEnvoiExec> data);
    bool delEnvoiExec(List<tEnvoiExec> data);
    bool envoiExecCourriel(int InstOwnerID, string EmailTo, string host);
    bool envoiExecRiCourriel(int InstOwnerID, string EmailTo, string host);

    IEnumerable<dynamic> getRiskMapList(string sort, string dir);
    IEnumerable<dynamic> addRiskMap(List<tRiskMap> data);
    IEnumerable<dynamic> updRiskMap(List<tRiskMap> data);
    bool delRiskMap(List<tRiskMap> data);
    IEnumerable<dynamic> getRMLevel();
    IEnumerable<dynamic> getRiskMapHoraire(int? id);
    IEnumerable<dynamic> addRiskMapHoraire(List<tRiskMapHoraire> data);
    IEnumerable<dynamic> updRiskMapHoraire(List<tRiskMapHoraire> data);
    bool delRiskMapHoraire(List<tRiskMapHoraire> data);
    IEnumerable<IGrouping<RiskMapGrp, RiskMapCour>> riskMapCourriel(List<int> id, string host);

    IEnumerable<dynamic> getEMailList(string sort, string dir);
    IEnumerable<dynamic> addEMail(List<EMailItem> data);
    IEnumerable<dynamic> updEMail(List<EMailItem> data);
    bool delEMail(List<EMailItem> data);
    bool AddToUAKM(int id, int typeId);
  }

  public class EnvoiRepository : IEnvoiRepository
  {
    private readonly MiddleOfficeEntities db = new MiddleOfficeEntities() { };
    private readonly IConfigurationProvider _configProvider;

    public EnvoiRepository(IConfigurationProvider configProvider)
    {
      _configProvider = configProvider;
    }
    public IEnumerable<dynamic> getEnvoiList(int? OwnerID, int TypeID, bool? isAuto, bool? IsActive, string sort, string dir)
    {
      var q1 = db.tEnvoi.Where(p => p.InstOwnerID == OwnerID && p.IsAuto == isAuto && p.TypeID == TypeID);
      if (IsActive == true)
        q1 = q1.Where(p => p.IsEnabled == true);
      var q = (from e in q1
               join c1 in db.tObjClassifier on e.PeriodichID equals c1.ObjClassifierID into _c1
               from c1 in _c1.DefaultIfEmpty()
               join c2 in db.tObjClassifier on e.InstOwnerID equals c2.ObjClassifierID into _c2
               from c2 in _c2.DefaultIfEmpty()
               select new
               {
                 id = e.ID,
                 e.InstOwnerID,
                 InstOwner = c2.Name,
                 e.EmailCc,
                 e.EmailTo,
                 e.IsAuto,
                 e.Mesto,
                 e.Osnovan,
                 e.PoryadPredst,
                 e.PeriodichID,
                 Periodich = c1.Name,
                 e.SrokRask,
                 e.SrokRass,
                 e.TypeInf,
                 e.VidAktiv,
                 e.IsEnabled
               });

      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      return q.ToList().Select(p => new
      {
        p.id,
        p.InstOwnerID,
        p.InstOwner,
        p.EmailCc,
        p.EmailTo,
        EmailToName = string.Join(", ", ql.Where(f => p.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        EmailCcName = string.Join(", ", ql.Where(f => p.EmailCc.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        p.IsAuto,
        p.Mesto,
        p.Osnovan,
        p.PoryadPredst,
        p.PeriodichID,
        p.Periodich,
        p.SrokRask,
        p.SrokRass,
        p.TypeInf,
        p.VidAktiv,
        p.IsEnabled
      });
    }

    public IEnumerable<dynamic> addEnvoi(List<tEnvoi> data)
    {
      db.tEnvoi.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.ID);
      var q = from e in db.tEnvoi.Where(p => ids.Contains(p.ID))
              join c1 in db.tObjClassifier on e.PeriodichID equals c1.ObjClassifierID into _c1
              from c1 in _c1.DefaultIfEmpty()
              join c2 in db.tObjClassifier on e.InstOwnerID equals c2.ObjClassifierID into _c2
              from c2 in _c2.DefaultIfEmpty()
              select new
              {
                id = e.ID,
                e.InstOwnerID,
                InstOwner = c2.Name,
                e.EmailCc,
                e.EmailTo,
                e.IsAuto,
                e.Mesto,
                e.Osnovan,
                e.PoryadPredst,
                e.PeriodichID,
                Periodich = c1.Name,
                e.SrokRask,
                e.SrokRass,
                e.TypeInf,
                e.VidAktiv,
                e.IsEnabled
              };

      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      return q.ToList().Select(p => new
      {
        p.id,
        p.InstOwnerID,
        p.InstOwner,
        p.EmailCc,
        p.EmailTo,
        EmailToName = string.Join(", ", ql.Where(f => p.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        EmailCcName = string.Join(", ", ql.Where(f => p.EmailCc.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        p.IsAuto,
        p.Mesto,
        p.Osnovan,
        p.PoryadPredst,
        p.PeriodichID,
        p.Periodich,
        p.SrokRask,
        p.SrokRass,
        p.TypeInf,
        p.VidAktiv,
        p.IsEnabled
      });
    }

    public IEnumerable<dynamic> updEnvoi(List<tEnvoi> data)
    {
      foreach (var d in data)
      {
        var q1 = db.tEnvoi.Find(d.ID);
        if (q1 != null)
        {
          q1.IsAuto = d.IsAuto;
          q1.InstOwnerID = d.InstOwnerID;
          q1.Mesto = d.Mesto;
          q1.Osnovan = d.Osnovan;
          q1.PoryadPredst = d.PoryadPredst;
          q1.Periodich = d.Periodich;
          q1.PeriodichID = d.PeriodichID;
          q1.SrokRask = d.SrokRask;
          q1.TypeInf = d.TypeInf;
          q1.VidAktiv = d.VidAktiv;
          q1.SrokRass = d.SrokRass;
          q1.EmailTo = d.EmailTo;
          q1.EmailCc = d.EmailCc;
          q1.IsEnabled = d.IsEnabled;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from e in db.tEnvoi.AsNoTracking().Where(p => ids.Contains(p.ID))
              join c1 in db.tObjClassifier.AsNoTracking() on e.PeriodichID equals c1.ObjClassifierID into _c1
              from c1 in _c1.DefaultIfEmpty()
              join c2 in db.tObjClassifier on e.InstOwnerID equals c2.ObjClassifierID into _c2
              from c2 in _c2.DefaultIfEmpty()
              select new
              {
                id = e.ID,
                e.InstOwnerID,
                InstOwner = c2.Name,
                e.EmailCc,
                e.EmailTo,
                e.IsAuto,
                e.Mesto,
                e.Osnovan,
                e.PoryadPredst,
                e.PeriodichID,
                Periodich = c1.Name,
                e.SrokRask,
                e.SrokRass,
                e.TypeInf,
                e.VidAktiv,
                e.IsEnabled
              };

      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      return q.ToList().Select(p => new
      {
        p.id,
        p.InstOwnerID,
        p.InstOwner,
        p.EmailCc,
        p.EmailTo,
        EmailToName = string.Join(", ", ql.Where(f => p.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        EmailCcName = string.Join(", ", ql.Where(f => p.EmailCc.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        p.IsAuto,
        p.Mesto,
        p.Osnovan,
        p.PoryadPredst,
        p.PeriodichID,
        p.Periodich,
        p.SrokRask,
        p.SrokRass,
        p.TypeInf,
        p.VidAktiv,
        p.IsEnabled
      });
    }

    public bool delEnvoi(List<tEnvoi> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.tEnvoi.Where(p => ids.Contains(p.ID));
        db.tEnvoi.RemoveRange(e);
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
      //return false;
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

    public bool envoyerCourriels(DateTime? dt, string host)
    {
      var de1 = dt ?? DateTime.Today;
      var db1 = db.tWorkDate.Where(p => p.WorkDate < de1).OrderByDescending(p => p.WorkDate).FirstOrDefault().WorkDate;

      var fwdy = de1.AddDays(1 - de1.DayOfYear);
      var fwdm = de1.AddDays(1 - de1.Day);
      var fwdq = de1.AddDays(1 - de1.Day).AddMonths(-(de1.Month - 1) % 3);
      var fwdqm = de1.AddDays(1 - de1.Day).AddMonths(-(de1.Month - 1) % 3 + 1).AddDays(-1);

      var fwd1 = de1.AddDays(1 - (int)de1.DayOfWeek);
      var fwd2 = de1.AddDays(2 - (int)de1.DayOfWeek);
      var fwd3 = de1.AddDays(3 - (int)de1.DayOfWeek);
      var fwd4 = de1.AddDays(4 - (int)de1.DayOfWeek);
      var fwd5 = de1.AddDays(5 - (int)de1.DayOfWeek);

      var q =
        from t in
      (
        from eh in db.tEnvoiHoraire
        where eh.EnvoiHoraireTypeID == 1 && eh.ModeID == 0 && eh.Day + eh.Month * 100 > db1.Day + db1.Month * 100 && eh.Day + eh.Month * 100 <= de1.Day + de1.Month * 100
        select eh.ID
      ).Union(
        db.tEnvoiHoraire.Where(p => p.EnvoiHoraireTypeID == 2 && p.ModeID == 0).Select(p => new { p.ID, p.Day }).ToList()
        .Select(j => new { j.ID, w = db.tWorkDate.Where(w => w.WorkDate >= fwdy).OrderBy(w => w.WorkDate).Take(j.Day ?? 1).Max(w => w.WorkDate) })
         .Where(p => p.w > db1 && p.w <= de1).Select(p => p.ID)
      ).Union(
        db.tEnvoiHoraire.Where(p => p.EnvoiHoraireTypeID == 3 && p.ModeID == 0).Select(p => new { p.ID, p.Day }).ToList()
        .Select(j => new { j.ID, w = db.tWorkDate.Where(w => w.WorkDate >= fwdq).OrderBy(w => w.WorkDate).Take(j.Day ?? 1).Max(w => w.WorkDate) })
         .Where(p => p.w > db1 && p.w <= de1).Select(p => p.ID)
      ).Union(
        db.tEnvoiHoraire.Where(p => p.EnvoiHoraireTypeID == 4 && p.ModeID == 0).Select(p => new { p.ID, p.Day }).ToList()
        .Select(j => new { j.ID, w = db.tWorkDate.Where(w => w.WorkDate >= fwdm).OrderBy(w => w.WorkDate).Take(j.Day ?? 1).Max(w => w.WorkDate) })
         .Where(p => p.w > db1 && p.w <= de1).Select(p => p.ID)
      ).Union(
        db.tEnvoiHoraire.Where(p => p.EnvoiHoraireTypeID == 5 && p.ModeID == 0).Select(p => new { p.ID, p.Day }).ToList()
        .Select(j => new { j.ID, w = db.tWorkDate.Where(w => w.WorkDate >= fwdqm).OrderBy(w => w.WorkDate).Take(j.Day ?? 1).Max(w => w.WorkDate) })
         .Where(p => p.w > db1 && p.w <= de1).Select(p => p.ID)
      ).Union(
        from eh in db.tEnvoiHoraire
        where eh.EnvoiHoraireTypeID == 6 && eh.ModeID == 0 && fwd1 > db1 && fwd1 <= de1
        select eh.ID
      ).Union(
        from eh in db.tEnvoiHoraire
        where eh.EnvoiHoraireTypeID == 7 && eh.ModeID == 0 && fwd2 > db1 && fwd2 <= de1
        select eh.ID
      ).Union(
        from eh in db.tEnvoiHoraire
        where eh.EnvoiHoraireTypeID == 8 && eh.ModeID == 0 && fwd3 > db1 && fwd3 <= de1
        select eh.ID
      ).Union(
        from eh in db.tEnvoiHoraire
        where eh.EnvoiHoraireTypeID == 9 && eh.ModeID == 0 && fwd4 > db1 && fwd4 <= de1
        select eh.ID
      ).Union(
        from eh in db.tEnvoiHoraire
        where eh.EnvoiHoraireTypeID == 10 && eh.ModeID == 0 && fwd5 > db1 && fwd5 <= de1
        select eh.ID
      ).Union(
        from eh in db.tEnvoiHoraire
        where eh.EnvoiHoraireTypeID == 11 && eh.ModeID == 0 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdy) > db1 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdy) <= de1
        select eh.ID
      ).Union(
        from eh in db.tEnvoiHoraire
        where eh.EnvoiHoraireTypeID == 12 && eh.ModeID == 0 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdq) > db1 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdq) <= de1
        select eh.ID
      ).Union(
        from eh in db.tEnvoiHoraire
        where eh.EnvoiHoraireTypeID == 13 && eh.ModeID == 0 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdm) > db1 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdm) <= de1
        select eh.ID
      ).Union(
        from eh in db.tEnvoiHoraire
        where eh.EnvoiHoraireTypeID == 14 && eh.ModeID == 0 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdqm) > db1 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdqm) <= de1
        select eh.ID
      )
        join eh in db.tEnvoiHoraire.Where(p => p.tEnvoi.IsEnabled == true) on t equals eh.ID
        select new { ID = eh.EnvoiID, eh.Comment };
      foreach (var e in q)
      {
        try
        {
          envoyerCourriel(e.ID, e.Comment, host);
        }
        catch (Exception ex)
        { }
      }
      return true;
    }

    public bool envoyerCourriel(int id, string Comment, string host)
    {
      var q = db.tEnvoi.Find(id);
      if (q != null)
      {
        var q2 = db.tObjClassifier.Find(q.InstOwnerID);
        var q1 = db.tEnvoiHoraire.FirstOrDefault(p => p.EnvoiID == id && p.ModeID == 1);
        DateTime? dt = null;
        if (q1 != null)
        {
          switch (q1.EnvoiHoraireTypeID)
          {
            case 1:
              if (q1.Month.HasValue && q1.Day.HasValue)
                dt = new DateTime(DateTime.Today.Year, q1.Month.Value, q1.Day.Value);
              break;
            case 2:
              if ((q1.Day ?? 1) > 0)
              {
                dt = new DateTime(DateTime.Today.Year, 1, 1);
                dt = db.tWorkDate.Where(p => p.WorkDate >= dt).OrderBy(p => p.WorkDate).Take(q1.Day ?? 1).OrderByDescending(p => p.WorkDate).FirstOrDefault().WorkDate;
              }
              break;
            case 3:
              if ((q1.Day ?? 1) > 0)
              {
                dt = new DateTime(DateTime.Today.Year, (DateTime.Today.Month - 1) / 3 * 3 + 1, 1);
                dt = db.tWorkDate.Where(p => p.WorkDate >= dt).OrderBy(p => p.WorkDate).Take(q1.Day ?? 1).OrderByDescending(p => p.WorkDate).FirstOrDefault().WorkDate;
              }
              break;
            case 4:
              if ((q1.Day ?? 1) > 0)
              {
                dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dt = db.tWorkDate.Where(p => p.WorkDate >= dt).OrderBy(p => p.WorkDate).Take(q1.Day ?? 1).OrderByDescending(p => p.WorkDate).FirstOrDefault().WorkDate;
              }
              break;
            case 11:
              if ((q1.Day ?? 1) > 0)
              {
                dt = new DateTime(DateTime.Today.Year, 1, 1).AddDays((q1.Day ?? 1) - 1);
                dt = db.tWorkDate.Where(p => p.WorkDate >= dt).OrderBy(p => p.WorkDate).FirstOrDefault().WorkDate;
              }
              break;
            case 12:
              if ((q1.Day ?? 1) > 0)
              {
                dt = new DateTime(DateTime.Today.Year, (DateTime.Today.Month - 1) / 3 * 3 + 1, 1).AddDays((q1.Day ?? 1) - 1);
                dt = db.tWorkDate.Where(p => p.WorkDate >= dt).OrderBy(p => p.WorkDate).FirstOrDefault().WorkDate;
              }
              break;
            case 13:
              if ((q1.Day ?? 1) > 0)
              {
                dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays((q1.Day ?? 1) - 1);
                dt = db.tWorkDate.Where(p => p.WorkDate >= dt).OrderBy(p => p.WorkDate).FirstOrDefault().WorkDate;
              }
              break;
          }
        }
        SmtpClient sc = new SmtpClient(/*ConfigurationManager.AppSettings["SMTPServer"]*/);
        MailMessage message = new MailMessage();
        message.From = new MailAddress(ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
        if (q.EmailTo != null)
          message.To.Add(host.Contains("localhost") ? "AVGrishin@gmail.com" : q.EmailTo);
        if (q.EmailCc != null)
          message.CC.Add(host.Contains("localhost") ? "AVGrishin@list.ru" : q.EmailCc);
        StringBuilder sb = new StringBuilder();
        sb.Append("<style>td, span, th {font-size:.8em;font-family: \"Segoe UI\", Verdana, Helvetica, Sans-Serif;} span {font-style:italic} th{font-size:.7em}</style>");
        sb.Append("<span>Уважаемые коллеги,<br>напоминаем о сроках предоставления следующей отчетности/информации:<br><br>");
        sb.AppendFormat("<table border='1' cellspacing='0'><tr><th>К</th><th>Тип раскрываемой информации</th><th>Срок представления</th><th>Место представления</th><th>Основание</th></tr><tr><td>{5}</td><td>{0}</td><td>{1}{4}</td><td>{2}</td><td>{3}</td></tr></table>", q.TypeInf, q.SrokRask, q.Mesto, q.Osnovan, dt.HasValue ? "(" + dt.Value.ToShortDateString() + ")" : "", q2 != null ? q2.Name : "");
        sb.Append("<br>Подпись: Внутренний контроль</span>");
        message.Body = sb.ToString();
        message.IsBodyHtml = true;
        message.Priority = MailPriority.High;
        message.Headers.Add("Importance", "High");
        message.IsBodyHtml = true;
        message.Subject = string.Format("Напоминание о сроках предоставления отчетности/информации {0}", Comment);
        sc.Send(message);

        return true;
      }
      return false;
    }

    public IEnumerable<dynamic> getConseilList(DateTime? d1, DateTime? d2, int? type, Boolean? nopen, string sort, string dir)
    {
      var q = from e in db.tConseil
              join l1 in db.taLib.Where(p => p.LConcept == 483545 && p.LParent == 483545) on e.Priorite equals l1.LID1 into l1_
              from l1 in l1_.DefaultIfEmpty()
              select new
              {
                id = e.ID,
                e.EmailCc,
                e.EmailTo,
                e.Violation,
                e.Conseil,
                e.Terme,
                e.Prolongation,
                e.ExecDate,
                e.Possesseur,
                e.Commentaire,
                e.IsEnabled,
                e.PrononceDate,
                e.Priorite,
                PrioriteNom = l1.LName,
                e.MinNomRiskPrice,
                e.MaxNomRiskPrice
              };
      if (type == 0)
      {
        if (d1.HasValue)
        {
          q = q.Where(a => a.PrononceDate >= d1);
        }
        if (d2.HasValue)
        {
          q = q.Where(a => a.PrononceDate <= d2);
        }
      }
      else if (type == 1)
      {
        if (d1.HasValue)
        {
          q = q.Where(a => a.Terme >= d1);
        }
        if (d2.HasValue)
        {
          q = q.Where(a => a.Terme <= d2);
        }
      }
      else if (type == 2)
      {
        if (d1.HasValue)
        {
          q = q.Where(a => a.ExecDate >= d1);
        }
        if (d2.HasValue)
        {
          q = q.Where(a => a.ExecDate <= d2);
        }
      }
      if (nopen == true)
        q = q.Where(a => a.IsEnabled == true);
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> addConseil(List<tConseil> data)
    {
      foreach (var e in data)
        e.InDateTime = DateTime.Now;
      db.tConseil.AddRange(data);

      db.SaveChanges();
      var ids = data.Select(p => p.ID);

      var q = from e in db.tConseil.AsNoTracking().Where(p => ids.Contains(p.ID))
              join l1 in db.taLib.Where(p => p.LConcept == 483545 && p.LParent == 483545) on e.Priorite equals l1.LID1 into l1_
              from l1 in l1_.DefaultIfEmpty()
              select new
              {
                id = e.ID,
                e.EmailCc,
                e.EmailTo,
                e.Violation,
                e.Conseil,
                e.Terme,
                e.Prolongation,
                e.ExecDate,
                e.Possesseur,
                e.Commentaire,
                e.IsEnabled,
                e.Priorite,
                PrioriteNom = l1.LName,
                e.MinNomRiskPrice,
                e.MaxNomRiskPrice
              };

      return q;
    }

    public IEnumerable<dynamic> updConseil(List<tConseil> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tConseil.Find(e.ID);
        if (q1 != null)
        {
          q1.Commentaire = e.Commentaire;
          q1.Conseil = e.Conseil;
          q1.EmailCc = e.EmailCc;
          q1.EmailTo = e.EmailTo;
          q1.InDateTime = DateTime.Now;
          q1.IsEnabled = e.IsEnabled;
          q1.Possesseur = e.Possesseur;
          q1.Prolongation = e.Prolongation;
          q1.Terme = e.Terme;
          q1.Violation = e.Violation;
          q1.Priorite = e.Priorite;
          q1.PrononceDate = e.PrononceDate;
          q1.ExecDate = e.ExecDate;
          q1.MinNomRiskPrice = e.MinNomRiskPrice;
          q1.MaxNomRiskPrice = e.MaxNomRiskPrice;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from c in db.tConseil.AsNoTracking().Where(p => ids.Contains(p.ID))
              join l1 in db.taLib.Where(p => p.LConcept == 483545 && p.LParent == 483545) on c.Priorite equals l1.LID1 into l1_
              from l1 in l1_.DefaultIfEmpty()
              select new
              {
                id = c.ID,
                c.EmailCc,
                c.EmailTo,
                c.Violation,
                c.Conseil,
                c.Terme,
                c.Prolongation,
                c.ExecDate,
                c.Possesseur,
                c.Commentaire,
                c.IsEnabled,
                c.Priorite,
                PrioriteNom = l1.LName,
                c.MinNomRiskPrice,
                c.MaxNomRiskPrice
              };

      return q;
    }

    public bool delConseil(List<tConseil> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.tConseil.Where(p => ids.Contains(p.ID));
        db.tConseil.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }

    public bool conseilCourriels(string host)
    {
      var de1 = DateTime.Today;
      var db1 = db.tWorkDate.Where(p => p.WorkDate < de1).OrderByDescending(p => p.WorkDate).FirstOrDefault().WorkDate;

      var fwdy = de1.AddDays(1 - de1.DayOfYear);
      var fwdm = de1.AddDays(1 - de1.Day);
      var fwdq = de1.AddDays(1 - de1.Day).AddMonths(-(de1.Month - 1) % 3);
      var fwdqm = de1.AddDays(1 - de1.Day).AddMonths(-(de1.Month - 1) % 3 + 1).AddDays(-1);

      var fwd1 = de1.AddDays(1 - (int)de1.DayOfWeek);
      var fwd2 = de1.AddDays(2 - (int)de1.DayOfWeek);
      var fwd3 = de1.AddDays(3 - (int)de1.DayOfWeek);
      var fwd4 = de1.AddDays(4 - (int)de1.DayOfWeek);
      var fwd5 = de1.AddDays(5 - (int)de1.DayOfWeek);

      var q =
        from t in
      (
        from eh in db.tConseilHoraire
        where eh.EnvoiHoraireTypeID == 1 && eh.Day + eh.Month * 100 > db1.Day + db1.Month * 100 && eh.Day + eh.Month * 100 <= de1.Day + de1.Month * 100
        select eh.ID
      ).Union(
        db.tConseilHoraire.Where(p => p.EnvoiHoraireTypeID == 2).Select(p => new { p.ID, p.Day }).ToList()
        .Select(j => new { j.ID, w = db.tWorkDate.Where(w => w.WorkDate >= fwdy).OrderBy(w => w.WorkDate).Take(j.Day ?? 1).Max(w => w.WorkDate) })
         .Where(p => p.w > db1 && p.w <= de1).Select(p => p.ID)
      ).Union(
        db.tConseilHoraire.Where(p => p.EnvoiHoraireTypeID == 3).Select(p => new { p.ID, p.Day }).ToList()
        .Select(j => new { j.ID, w = db.tWorkDate.Where(w => w.WorkDate >= fwdq).OrderBy(w => w.WorkDate).Take(j.Day ?? 1).Max(w => w.WorkDate) })
         .Where(p => p.w > db1 && p.w <= de1).Select(p => p.ID)
      ).Union(
        db.tConseilHoraire.Where(p => p.EnvoiHoraireTypeID == 4).Select(p => new { p.ID, p.Day }).ToList()
        .Select(j => new { j.ID, w = db.tWorkDate.Where(w => w.WorkDate >= fwdm).OrderBy(w => w.WorkDate).Take(j.Day ?? 1).Max(w => w.WorkDate) })
         .Where(p => p.w > db1 && p.w <= de1).Select(p => p.ID)
      ).Union(
        db.tConseilHoraire.Where(p => p.EnvoiHoraireTypeID == 5).Select(p => new { p.ID, p.Day }).ToList()
        .Select(j => new { j.ID, w = db.tWorkDate.Where(w => w.WorkDate >= fwdqm).OrderBy(w => w.WorkDate).Take(j.Day ?? 1).Max(w => w.WorkDate) })
         .Where(p => p.w > db1 && p.w <= de1).Select(p => p.ID)
      ).Union(
        from eh in db.tConseilHoraire
        where eh.EnvoiHoraireTypeID == 6 && fwd1 > db1 && fwd1 <= de1
        select eh.ID
      ).Union(
        from eh in db.tConseilHoraire
        where eh.EnvoiHoraireTypeID == 7 && fwd2 > db1 && fwd2 <= de1
        select eh.ID
      ).Union(
        from eh in db.tConseilHoraire
        where eh.EnvoiHoraireTypeID == 8 && fwd3 > db1 && fwd3 <= de1
        select eh.ID
      ).Union(
        from eh in db.tConseilHoraire
        where eh.EnvoiHoraireTypeID == 9 && fwd4 > db1 && fwd4 <= de1
        select eh.ID
      ).Union(
        from eh in db.tConseilHoraire
        where eh.EnvoiHoraireTypeID == 10 && fwd5 > db1 && fwd5 <= de1
        select eh.ID
      ).Union(
        from eh in db.tConseilHoraire
        where eh.EnvoiHoraireTypeID == 11 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdy) > db1 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdy) <= de1
        select eh.ID
      ).Union(
        from eh in db.tConseilHoraire
        where eh.EnvoiHoraireTypeID == 12 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdq) > db1 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdq) <= de1
        select eh.ID
      ).Union(
        from eh in db.tConseilHoraire
        where eh.EnvoiHoraireTypeID == 13 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdm) > db1 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdm) <= de1
        select eh.ID
      ).Union(
        from eh in db.tConseilHoraire
        where eh.EnvoiHoraireTypeID == 14 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdqm) > db1 && SqlFunctions.DateAdd("d", eh.Day - 1, fwdqm) <= de1
        select eh.ID
      )
        join eh in db.tConseilHoraire.Where(p => p.tConseil.IsEnabled == true) on t equals eh.ID
        select new { ID = eh.ConseilID };
      foreach (var e in q)
      {
        try
        {
          conseilCourriel(e.ID, host);
        }
        catch (Exception ex)
        { }
      }
      return true;
    }

    public bool conseilCourriel(int? id, string host)
    {
      var q = db.tConseil.FirstOrDefault(p => p.ID == id);
      if (q != null)
      {
        SmtpClient sc = new SmtpClient();
        MailMessage message = new MailMessage();
        message.From = new MailAddress($"Внутренний контроль <{ConfigurationManager.AppSettings["EMailFrom"]}>");
        if (q.EmailTo != null)
          message.To.Add(host.Contains("localhost") ? "qbcontrol@qbfin.ru" : q.EmailTo);
        if (q.EmailCc != null)
          message.CC.Add(host.Contains("localhost") ? "qbcontrol@qbfin.ru" : q.EmailCc);
        StringBuilder sb = new StringBuilder();
        sb.Append("<style>td, span, th {font-size:.8em;font-family: \"Segoe UI\", Verdana, Helvetica, Sans-Serif;vertical-align:top} span {font-style:italic} th{font-size:.7em}</style>");
        sb.Append("<span>Уважаемые коллеги,<br>напоминаю Вам о сроках исполнения рекомендаций, выпущенных генеральным директором и заместителем генерального директора-контролером.<br>Прошу Вас в срок не позднее одного рабочего дня, следующего за днем получения настоящего напоминания, предоставить информацию о статусе и сроках исполнения рекомендации.<br><br>Спасибо!<br><br>");
        //sb.Append("<span>Уважаемые коллеги,<br>напоминаю Вам о сроках исполнения рекомендаций, выпущенных заместителем генерального директора-контролером АО \"УК \".<br>Прошу Вас в срок не позднее одного рабочего дня, следующего за днем получения настоящего напоминания, предоставить информацию о статусе и сроках исполнения рекомендации.<br>Дополнительно сообщаю, что информация о неисполненных в срок рекомендациях будет рассматриваться на заседаниях Правления, а также Совета директоров АО \"УК \".<br><br>Спасибо!<br><br>");
        sb.AppendFormat("<table border='1' cellspacing='0'><tr><th width='40%'>Содержание недостатка/нарушения</th><th>Содержание рекомендации</th><th>Срок выполнения рекомендации</th><th>Статус выполнения</th><th>Срок неисполнения рекомендаций</th><th>Срок для продления выполнения рекомендации</th><th>Комментарий</th></tr><tr><td>{0}</td><td>{1}</td><td>{2:dd.MM.yyyy}</td><td>{3}</td><td>{4}</td><td>{5:dd.MM.yyyy}</td><td>{6}</td></tr></table>", q.Violation.Replace("\n", "<br>"), q.Conseil, q.Terme, q.Terme < DateTime.Today ? "Просрочено" : "&nbsp;", q.Terme.HasValue && q.Terme < DateTime.Today ? DateTime.Today.Subtract(q.Terme.Value).TotalDays.ToString() + " дней" : "&nbsp;", q.Prolongation, q.Commentaire);
        sb.Append("<br>С уважением, ");
        sb.Append("<br>Заместитель генерального директора - контролер");
        message.Body = sb.ToString();
        message.IsBodyHtml = true;
        message.Priority = MailPriority.High;
        message.Headers.Add("Importance", "High");
        message.IsBodyHtml = true;
        message.Subject = "Напоминание по исполнению рекомендаций контролера";
        sc.Send(message);

        return true;
      }
      return false;
    }

    public bool conseilCourrielAll()
    {
      SmtpClient sc = new SmtpClient();
      MailMessage message = new MailMessage();
      message.From = new MailAddress($"Внутренний контроль <{ConfigurationManager.AppSettings["EMailFrom"]}>");
      message.Bcc.Add("GrishinAV@uralsib.ru");
      StringBuilder sb = new StringBuilder();
      sb.Append("<style>td, span, th {font-size:.8em;font-family: \"Segoe UI\", Verdana, Helvetica, Sans-Serif;vertical-align:top} span {font-style:italic} th{font-size:.7em}</style>");
      sb.Append("<table border='1' cellspacing='0'><tr><th width='40%'>Содержание недостатка/нарушения</th><th>Содержание рекомендации</th><th>Срок выполнения рекомендации</th><th>Статус выполнения</th><th>Срок неисполнения рекомендаций</th><th>Срок для продления выполнения рекомендации</th><th>Владельцы</th></tr>");
      var q1 = from c in db.tConseil.Where(p => p.IsEnabled == true)
               orderby c.Terme
               select new
               {
                 c.Violation,
                 c.Conseil,
                 c.Terme,
                 c.Prolongation,
                 c.EmailTo
               };

      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      var q2 = q1.ToList().Select(p => new
      {
        p.Violation,
        p.Conseil,
        p.Terme,
        p.Prolongation,
        EmailToName = string.Join(", ", ql.Where(f => p.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray())
      });

      foreach (var q in q2)
      {
        sb.AppendFormat("<tr style=\"color:{6}\"><td>{0}</td><td>{1}</td><td>{2:dd.MM.yyyy}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{7}</td></tr>",
          q.Violation.Replace("\n", "<br>"),
          q.Conseil,
          q.Terme,
          q.Terme < DateTime.Today ? "Просрочено" : "&nbsp;",
          q.Terme.HasValue && q.Terme < DateTime.Today ? DateTime.Today.Subtract(q.Terme.Value).TotalDays.ToString() + " дней" : "&nbsp;",
          q.Prolongation.HasValue ? q.Prolongation.Value.ToShortDateString() : "&nbsp;",
          q.Terme < DateTime.Today ? "red" : q.Terme < DateTime.Today.AddDays(7) ? "maroon" : "black",
          q.EmailToName
        );
      }
      sb.Append("</table>");
      sb.Append("<br>С уважением, ");
      sb.Append("<br>Заместитель генерального директора - контролер");
      message.Body = sb.ToString();
      message.IsBodyHtml = true;
      message.Priority = MailPriority.High;
      message.Headers.Add("Importance", "High");
      message.IsBodyHtml = true;
      message.Subject = "Журнал рекомендаций контролера";
      sc.Send(message);

      return true;
    }

    public IEnumerable<dynamic> getCPriorite()
    {
      var q = from l in db.taLib.Where(p => p.LConcept == 483545 && p.LParent == 483545)
              select new { Text = l.LName, Value = l.LID1 };
      return q;
    }

    public bool conseilEnabledCourriel()
    {
      var q1 = db.tConseil.Where(p => p.IsEnabled == true).OrderBy(p => p.Terme).ToList();
      if (q1.Count > 0)
      {
        SmtpClient sc = new SmtpClient();
        MailMessage message = new MailMessage();
        message.From = new MailAddress($"Внутренний контроль <{ConfigurationManager.AppSettings["EMailFrom"]}>");
        message.To.Add(_configProvider.GetValue<string>("conseilEnbCourriel"));
        //message.Bcc.Add(ConfigurationManager.AppSettings["EMailFrom"]);
        StringBuilder sb = new StringBuilder();
        sb.Append("<style>td, span, th {font-size:.8em;font-family: \"Segoe UI\", Verdana, Helvetica, Sans-Serif;vertical-align:top} span {font-style:italic} th{font-size:.7em}</style>");
        sb.Append("<table border='1' cellspacing='0'>");
        sb.Append("<tr><th width='40%'>Содержание недостатка/нарушения</th><th>Содержание рекомендации</th><th>Дата вынесения</th><th>Срок выполнения рекомендации</th><th>Срок неисполнения рекомендаций</th><th>Срок для продления выполнения рекомендации</th><th>Владелец</th></tr>");
        foreach (var q in q1)
        {
          sb.AppendFormat("<tr style='color:{7}'><td>{0}</td><td>{1}</td><td>{5:dd.MM.yy}</td><td>{2:dd.MM.yy}</td><td>{3}</td><td>{4}</td><td>{6}</td></tr>",
            (q.Violation ?? "").Replace("\n", "<br>"),
            q.Conseil,
            q.Terme,
            q.Terme.HasValue && q.Terme < DateTime.Today ? DateTime.Today.Subtract(q.Terme.Value).TotalDays.ToString() + " дней" : "&nbsp;",
            q.Prolongation.HasValue ? q.Prolongation.Value.ToShortDateString() : "&nbsp;",
            q.PrononceDate,
            q.Possesseur,
            q.Terme < DateTime.Today ? "red" : "black;"
          );
        }
        sb.Append("</table>");
        message.Body = sb.ToString();
        message.IsBodyHtml = true;
        message.Priority = MailPriority.High;
        message.Headers.Add("Importance", "High");
        message.IsBodyHtml = true;
        message.Subject = "Незакрытые напоминания по исполнению рекомендаций контролера";
        sc.Send(message);
        return true;
      }
      return false;
    }

    public IEnumerable<dynamic> getConseilHoraire(int? id)
    {
      var q = from eh in db.tConseilHoraire.Where(p => p.ConseilID == id)
              select new
              {
                id = eh.ID,
                eh.ConseilID,
                eh.EnvoiHoraireTypeID,
                EnvoiHoraireType = eh.tEnvoiHoraireType.Name,
                eh.Day,
                eh.Month
              };
      return q;
    }

    public IEnumerable<dynamic> addConseilHoraire(List<tConseilHoraire> data)
    {
      db.tConseilHoraire.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.ID);
      var q = from eh in db.tConseilHoraire.Where(p => ids.Contains(p.ID))
              select new
              {
                id = eh.ID,
                eh.ConseilID,
                eh.EnvoiHoraireTypeID,
                EnvoiHoraireType = eh.tEnvoiHoraireType.Name,
                eh.Day,
                eh.Month
              };
      return q;
    }

    public IEnumerable<dynamic> updConseilHoraire(List<tConseilHoraire> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tConseilHoraire.Find(e.ID);
        if (q1 != null)
        {
          q1.EnvoiHoraireTypeID = e.EnvoiHoraireTypeID;
          q1.Day = e.Day;
          q1.Month = e.Month;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from eh in db.tConseilHoraire.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                id = eh.ID,
                eh.ConseilID,
                eh.EnvoiHoraireTypeID,
                EnvoiHoraireType = eh.tEnvoiHoraireType.Name,
                eh.Day,
                eh.Month
              };
      return q;
    }

    public bool delConseilHoraire(List<tConseilHoraire> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.tConseilHoraire.Where(p => ids.Contains(p.ID));
        db.tConseilHoraire.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }

    public IEnumerable<dynamic> getEnvoiHoraire(int? id)
    {
      var q = from eh in db.tEnvoiHoraire.Where(p => p.EnvoiID == id)
              select new
              {
                id = eh.ID,
                eh.EnvoiID,
                eh.EnvoiHoraireTypeID,
                eh.ModeID,
                Mode = eh.ModeID == 0 ? "Напоминание" : "Срок направления",
                EnvoiHoraireType = eh.tEnvoiHoraireType.Name,
                eh.Day,
                eh.Month,
                eh.Comment
              };
      return q;
    }

    public IEnumerable<dynamic> addEnvoiHoraire(List<tEnvoiHoraire> data)
    {
      db.tEnvoiHoraire.AddRange(data);
      db.SaveChanges();

      var ids = data.Select(p => p.ID);
      var q = from eh in db.tEnvoiHoraire.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                id = eh.ID,
                eh.EnvoiID,
                eh.EnvoiHoraireTypeID,
                eh.ModeID,
                Mode = eh.ModeID == 0 ? "Напоминание" : "Срок направления",
                EnvoiHoraireType = eh.tEnvoiHoraireType.Name,
                eh.Day,
                eh.Month,
                eh.Comment
              };
      return q;
    }

    public IEnumerable<dynamic> updEnvoiHoraire(List<tEnvoiHoraire> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tEnvoiHoraire.Find(e.ID);
        if (q1 != null)
        {
          q1.EnvoiHoraireTypeID = e.EnvoiHoraireTypeID;
          q1.ModeID = e.ModeID;
          q1.Day = e.Day;
          q1.Month = e.Month;
          q1.Comment = e.Comment;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from eh in db.tEnvoiHoraire.AsNoTracking().Where(p => ids.Contains(p.ID))
              select new
              {
                id = eh.ID,
                eh.EnvoiID,
                eh.ModeID,
                Mode = eh.ModeID == 0 ? "Напоминание" : "Срок направления",
                eh.EnvoiHoraireTypeID,
                EnvoiHoraireType = eh.tEnvoiHoraireType.Name,
                eh.Day,
                eh.Month,
                eh.Comment
              };

      return q;
    }

    public bool delEnvoiHoraire(List<tEnvoiHoraire> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.tEnvoiHoraire.Where(p => ids.Contains(p.ID));
        db.tEnvoiHoraire.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }

    public IEnumerable<dynamic> getEnvoiHoraireType()
    {
      var q = db.tEnvoiHoraireType.Select(p => new { id = p.ID, p.Name });
      return q;
    }

    public IEnumerable<dynamic> getEnvoiExecList(int? OwnerID, int TypeID, DateTime? d1, DateTime? d2, bool? IsExec, string sort, string dir)
    {
      var q1 = db.tEnvoiExec.Where(p => 1 == 1);
      if (d1.HasValue)
        q1 = q1.Where(p => p.Date1 >= d1);
      if (d2.HasValue)
        q1 = q1.Where(p => p.Date1 <= d2);
      if (IsExec == true)
        q1 = q1.Where(p => !p.Date2.HasValue && p.tEnvoi.IsEnabled == true);
      var q = from t in q1
              join en in db.tEnvoi.Where(p => p.InstOwnerID == OwnerID && p.TypeID == TypeID) on t.EnvoiID equals en.ID
              join c1 in db.tObjClassifier on en.PeriodichID equals c1.ObjClassifierID into _c1
              from c1 in _c1.DefaultIfEmpty()
              join c2 in db.tObjClassifier on en.InstOwnerID equals c2.ObjClassifierID into _c2
              from c2 in _c2.DefaultIfEmpty()
              select new
              {
                id = t.ID,
                t.EnvoiID,
                en.InstOwnerID,
                InstOwner = c2.Name,
                t.Date1,
                t.Date2,
                en.TypeInf,
                en.Osnovan,
                en.Mesto,
                en.PoryadPredst,
                en.PeriodichID,
                Periodich = c1.Name,
                en.SrokRask,
                en.IsAuto,
                en.IsEnabled,
                en.EmailCc,
                en.EmailTo,
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      return q.ToList().Select(p => new
      {
        p.id,
        p.EnvoiID,
        p.InstOwnerID,
        p.InstOwner,
        p.Date1,
        p.Date2,
        p.TypeInf,
        p.Osnovan,
        p.Mesto,
        p.PoryadPredst,
        p.PeriodichID,
        p.Periodich,
        p.SrokRask,
        p.IsAuto,
        p.IsEnabled,
        p.EmailCc,
        p.EmailTo,
        EmailToName = string.Join(", ", ql.Where(f => p.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        EmailCcName = string.Join(", ", ql.Where(f => p.EmailCc.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
      });

    }

    public IEnumerable<dynamic> addEnvoiExec(List<tEnvoiExec> data)
    {
      foreach (var e in data)
        e.InDateTime = DateTime.Now;

      db.tEnvoiExec.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.ID);

      var q = from t in db.tEnvoiExec.Where(p => ids.Contains(p.ID))
              join en in db.tEnvoi on t.EnvoiID equals en.ID
              join c1 in db.tObjClassifier on en.PeriodichID equals c1.ObjClassifierID into _c1
              from c1 in _c1.DefaultIfEmpty()
              join c2 in db.tObjClassifier on en.InstOwnerID equals c2.ObjClassifierID into _c2
              from c2 in _c2.DefaultIfEmpty()
              select new
              {
                id = t.ID,
                t.EnvoiID,
                en.InstOwnerID,
                InstOwner = c2.Name,
                t.Date1,
                t.Date2,
                en.TypeInf,
                en.Osnovan,
                en.Mesto,
                en.PoryadPredst,
                en.PeriodichID,
                Periodich = c1.Name,
                en.SrokRask,
                en.EmailTo,
                en.EmailCc,
                en.IsAuto,
                en.IsEnabled
              };

      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      return q.ToList().Select(p => new
      {
        p.id,
        p.EnvoiID,
        p.InstOwnerID,
        p.InstOwner,
        p.Date1,
        p.Date2,
        p.TypeInf,
        p.Osnovan,
        p.Mesto,
        p.PoryadPredst,
        p.PeriodichID,
        p.Periodich,
        p.SrokRask,
        p.IsAuto,
        p.IsEnabled,
        p.EmailCc,
        p.EmailTo,
        EmailToName = string.Join(", ", ql.Where(f => p.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        EmailCcName = string.Join(", ", ql.Where(f => p.EmailCc.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
      });
    }

    public IEnumerable<dynamic> updEnvoiExec(List<tEnvoiExec> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tEnvoiExec.Find(e.ID);
        if (q1 != null)
        {
          q1.Date1 = e.Date1;
          q1.Date2 = e.Date2;
          q1.InDateTime = DateTime.Now;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from t in db.tEnvoiExec.Where(p => ids.Contains(p.ID))
              join en in db.tEnvoi on t.EnvoiID equals en.ID
              join c1 in db.tObjClassifier on en.PeriodichID equals c1.ObjClassifierID into _c1
              from c1 in _c1.DefaultIfEmpty()
              join c2 in db.tObjClassifier on en.InstOwnerID equals c2.ObjClassifierID into _c2
              from c2 in _c2.DefaultIfEmpty()
              select new
              {
                id = t.ID,
                t.EnvoiID,
                en.InstOwnerID,
                InstOwner = c2.Name,
                t.Date1,
                t.Date2,
                en.TypeInf,
                en.Osnovan,
                en.Mesto,
                en.PoryadPredst,
                en.PeriodichID,
                Periodich = c1.Name,
                en.SrokRask,
                en.EmailTo,
                en.EmailCc,
                en.IsAuto,
                en.IsEnabled
              };

      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      return q.ToList().Select(p => new
      {
        p.id,
        p.EnvoiID,
        p.InstOwnerID,
        p.InstOwner,
        p.Date1,
        p.Date2,
        p.TypeInf,
        p.Osnovan,
        p.Mesto,
        p.PoryadPredst,
        p.PeriodichID,
        p.Periodich,
        p.SrokRask,
        p.IsAuto,
        p.IsEnabled,
        p.EmailCc,
        p.EmailTo,
        EmailToName = string.Join(", ", ql.Where(f => p.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        EmailCcName = string.Join(", ", ql.Where(f => p.EmailCc.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
      });
    }

    public bool delEnvoiExec(List<tEnvoiExec> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.tEnvoiExec.Where(p => ids.Contains(p.ID));
        db.tEnvoiExec.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }

    public bool envoiExecCourriel(int InstOwnerID, string EmailTo, string host)
    {
      var d = DateTime.Today;//.AddDays(-DateTime.Today.DayOfYear);
      var q = from t in db.tEnvoiExec.Where(p => p.Date2 == null || p.Date2 > d)
              join en in db.tEnvoi.Where(p => p.IsEnabled == true && p.TypeID == 1 && p.InstOwnerID == InstOwnerID) on t.EnvoiID equals en.ID
              join c1 in db.tObjClassifier on en.PeriodichID equals c1.ObjClassifierID into _c1
              from c1 in _c1.DefaultIfEmpty()
              orderby t.Date1
              select new
              {
                id = t.ID,
                t.EnvoiID,
                t.Date1,
                t.Date2,
                en.TypeInf,
                en.Osnovan,
                en.Mesto,
                en.PoryadPredst,
                en.PeriodichID,
                Periodich = c1.Name,
                en.SrokRask,
                en.EmailTo
              };

      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      var q1 = q.ToList().Select(p => new
      {
        p.id,
        p.EnvoiID,
        p.Date1,
        p.Date2,
        p.TypeInf,
        p.Osnovan,
        p.Mesto,
        p.PoryadPredst,
        p.PeriodichID,
        p.Periodich,
        p.SrokRask,
        p.EmailTo,
        Email = string.Join(", ", ql.Where(f => p.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray())
      });

      SmtpClient sc = new SmtpClient();
      MailMessage message = new MailMessage();
      message.From = new MailAddress(ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
      message.To.Add(host.Contains("localhost") ? "qbcontrol@qbfin.ru" : EmailTo);
      StringBuilder sb = new StringBuilder();
      sb.Append("<style>td, span, th {font-size:.8em;font-family: \"Segoe UI\", Verdana, Helvetica, Sans-Serif;} span {font-style:italic} th{font-size:.7em}</style>");
      sb.Append("<span>Уважаемые коллеги,<br>напоминаем о сроках предоставления следующей отчетности/информации:<br><br>");
      sb.Append("<table border='1' cellspacing='0'><tr><th>Дата, не позднее</th><th>Дата факт. раскрытия</th><th>Вид отчетности</th><th>Основание</th><th>Место предоставления</th><th>Порядок предоставления</th><th>Периодичность</th><th>Срок направления</th><th>Исполнители</th></tr>");
      foreach (var qd in q1)
      {
        sb.AppendFormat("<tr style=\"color:{8}\"><td>{0:dd.MM.yy}</td><td>{9}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td></tr>",
          qd.Date1,
          qd.TypeInf,
          qd.Osnovan,
          qd.Mesto,
          qd.PoryadPredst,
          qd.Periodich,
          qd.SrokRask,
          qd.Email,
          qd.Date2.HasValue ? "gray" : qd.Date1 < DateTime.Today ? "burlywood" : qd.Date1 < DateTime.Today.AddDays(14) ? "red" : qd.Date1 < DateTime.Today.AddDays(28) ? "maroon" : "black",
          qd.Date2.HasValue ? qd.Date2.Value.ToString("dd.MM.yy") : "&nbsp;");
      }
      sb.Append("</table>");

      sb.Append("<br>Подпись: Внутренний контроль</span>");
      message.Body = sb.ToString();
      message.IsBodyHtml = true;
      message.Priority = MailPriority.High;
      message.Headers.Add("Importance", "High");
      message.IsBodyHtml = true;
      message.Subject = "Контроль за предоставлением отчетности";
      sc.Send(message);
      return true;
    }

    public bool envoiExecRiCourriel(int InstOwnerID, string EmailTo, string host)
    {
      var d = DateTime.Today;//.AddDays(-DateTime.Today.DayOfYear);
      var q = from t in db.tEnvoiExec.Where(p => p.Date2 == null || p.Date2 > d)
              join en in db.tEnvoi.Where(p => p.IsEnabled == true && p.TypeID == 2 && p.InstOwnerID == InstOwnerID) on t.EnvoiID equals en.ID
              join c1 in db.tObjClassifier on en.PeriodichID equals c1.ObjClassifierID into _c1
              from c1 in _c1.DefaultIfEmpty()
              orderby t.Date1
              select new
              {
                id = t.ID,
                t.EnvoiID,
                t.Date1,
                t.Date2,
                en.TypeInf,
                en.Osnovan,
                en.PeriodichID,
                Periodich = c1.Name,
                en.SrokRask,
                en.EmailTo,
                en.EmailCc,
                en.InstOwnerID
              };

      var ql = db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622).Select(p => new { p.LName, p.LName1 });

      var q1 = q.ToList().Select(p => new
      {
        p.id,
        p.EnvoiID,
        p.Date1,
        p.Date2,
        p.TypeInf,
        p.Osnovan,
        p.PeriodichID,
        p.Periodich,
        p.SrokRask,
        EmailTo = string.Join(", ", ql.Where(f => p.EmailTo.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray()),
        EmailCc = string.Join(", ", ql.Where(f => p.EmailCc.IndexOf(f.LName1) > -1).OrderBy(f => f.LName).Select(f => f.LName.Trim()).ToArray())
      });

      SmtpClient sc = new SmtpClient();
      MailMessage message = new MailMessage();
      message.From = new MailAddress(ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
      message.To.Add(host.Contains("localhost") ? "qbcontrol@qbfin.ru" : EmailTo);
      StringBuilder sb = new StringBuilder();
      sb.Append("<style>td, span, th {font-size:.8em;font-family: \"Segoe UI\", Verdana, Helvetica, Sans-Serif;} span {font-style:italic} th{font-size:.7em}</style>");
      sb.Append("<span>Уважаемые коллеги,<br>напоминаем о сроках предоставления следующей отчетности/информации:<br><br>");
      sb.Append("<table border='1' cellspacing='0'><tr><th>Дата, не позднее</th><th>Дата факт. раскрытия</th><th>Вид раскрываемой информации</th><th>Основание</th><th>Периодичность</th><th>Срок раскрытия</th><th>Ответственные лица за предоставление информации</th><th>Ответственные лица за раскрытие информации</th></tr>");
      foreach (var qd in q1)
      {
        sb.AppendFormat("<tr style=\"color:{7}\"><td>{0:dd.MM.yy}</td><td>{8}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>",
          qd.Date1,
          qd.TypeInf,
          qd.Osnovan,
          qd.Periodich,
          qd.SrokRask,
          qd.EmailTo,
          qd.EmailCc,
          qd.Date2.HasValue ? "gray" : qd.Date1 < DateTime.Today ? "burlywood" : qd.Date1 < DateTime.Today.AddDays(14) ? "red" : qd.Date1 < DateTime.Today.AddDays(28) ? "maroon" : "black",
          qd.Date2.HasValue ? qd.Date2.Value.ToString("dd.MM.yy") : "&nbsp;");
      }
      sb.Append("</table>");

      sb.Append("<br>Подпись: Внутренний контроль</span>");
      message.Body = sb.ToString();
      message.IsBodyHtml = true;
      message.Priority = MailPriority.High;
      message.Headers.Add("Importance", "High");
      message.IsBodyHtml = true;
      message.Subject = "Контроль за сроками раскрытия информации";
      sc.Send(message);
      return true;
    }

    public IEnumerable<dynamic> getRiskMapList(string sort, string dir)
    {
      var q = from e in db.tRiskMap
              join li in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639) on e.Influence equals li.LBInt1 into l1_
              from li in l1_.DefaultIfEmpty()
              join lp in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639) on e.Probabilite equals lp.LBInt1 into l2_
              from lp in l2_.DefaultIfEmpty()
              join lf in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639) on e.ControlForce equals lf.LBInt1 into l3_
              from lf in l3_.DefaultIfEmpty()
              join la in db.taLib.Where(p => p.LConcept == 482644 && p.LParent == 482644) on li.LName + lp.LName + lf.LName equals la.LName into l4_
              from la in l4_.DefaultIfEmpty()
              select new { id = e.ID, e.EmailCc, e.EmailTo, e.BisProc, e.But, e.Control, e.ControlForce, e.Dep, e.IsEnabled, e.EssentielRisk, e.Influence, e.JurPersonne, e.NumRisk, e.PossesseurBut, e.PossesseurControl, e.Probabilite, e.RiskName, InfluenceName = li.LName, ProbabiliteName = lp.LName, ControlForceName = lf.LName, TotalName = la.LName1 };

      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;

    }

    public IEnumerable<dynamic> addRiskMap(List<tRiskMap> data)
    {
      foreach (var e in data)
        e.InDateTime = DateTime.Now;

      db.tRiskMap.AddRange(data);
      db.SaveChanges();

      var ids = data.Select(p => p.ID);
      var q = from e in db.tRiskMap.Where(p => ids.Contains(p.ID))
              join li in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639) on e.Influence equals li.LBInt1 into l1_
              from li in l1_.DefaultIfEmpty()
              join lp in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639) on e.Probabilite equals lp.LBInt1 into l2_
              from lp in l2_.DefaultIfEmpty()
              join lf in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639) on e.ControlForce equals lf.LBInt1 into l3_
              from lf in l3_.DefaultIfEmpty()
              join la in db.taLib.Where(p => p.LConcept == 482644 && p.LParent == 482644) on li.LName + lp.LName + lf.LName equals la.LName into l4_
              from la in l4_.DefaultIfEmpty()
              select new
              {
                id = e.ID,
                e.EmailCc,
                e.EmailTo,
                e.BisProc,
                e.But,
                e.Control,
                e.ControlForce,
                e.Dep,
                e.IsEnabled,
                e.EssentielRisk,
                e.Influence,
                e.JurPersonne,
                e.NumRisk,
                e.PossesseurBut,
                e.PossesseurControl,
                e.Probabilite,
                e.RiskName,
                InfluenceName = li.LName,
                ProbabiliteName = lp.LName,
                ControlForceName = lf.LName,
                TotalName = la.LName1
              };

      return q;
    }

    public IEnumerable<dynamic> updRiskMap(List<tRiskMap> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tRiskMap.Find(e.ID);
        if (q1 != null)
        {
          q1.BisProc = e.BisProc;
          q1.But = e.But;
          q1.Control = e.Control;
          q1.ControlForce = e.ControlForce;
          q1.Dep = e.Dep;
          q1.EmailCc = e.EmailCc;
          q1.EmailTo = e.EmailTo;
          q1.InDateTime = DateTime.Now;
          q1.IsEnabled = e.IsEnabled;
          q1.EssentielRisk = e.EssentielRisk;
          q1.Influence = e.Influence;
          q1.JurPersonne = e.JurPersonne;
          q1.NumRisk = e.NumRisk;
          q1.PossesseurBut = e.PossesseurBut;
          q1.PossesseurControl = e.PossesseurControl;
          q1.Probabilite = e.Probabilite;
          q1.RiskName = e.RiskName;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from r in db.tRiskMap.AsNoTracking().Where(p => ids.Contains(p.ID))
              join li in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639) on r.Influence equals li.LBInt1 into l1_
              from li in l1_.DefaultIfEmpty()
              join lp in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639) on r.Probabilite equals lp.LBInt1 into l2_
              from lp in l2_.DefaultIfEmpty()
              join lf in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639) on r.ControlForce equals lf.LBInt1 into l3_
              from lf in l3_.DefaultIfEmpty()
              join la in db.taLib.Where(p => p.LConcept == 482644 && p.LParent == 482644) on li.LName + lp.LName + lf.LName equals la.LName into l4_
              from la in l4_.DefaultIfEmpty()
              select new
              {
                id = r.ID,
                r.EmailCc,
                r.EmailTo,
                r.BisProc,
                r.But,
                r.Control,
                r.ControlForce,
                r.Dep,
                r.IsEnabled,
                r.EssentielRisk,
                r.Influence,
                r.JurPersonne,
                r.NumRisk,
                r.PossesseurBut,
                r.PossesseurControl,
                r.Probabilite,
                r.RiskName,
                InfluenceName = li.LName,
                ProbabiliteName = lp.LName,
                ControlForceName = lf.LName,
                TotalName = la.LName1
              };
      return q;
    }

    public bool delRiskMap(List<tRiskMap> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.tRiskMap.Where(p => ids.Contains(p.ID));
        db.tRiskMap.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }

    public IEnumerable<dynamic> getRMLevel()
    {
      var q = from l in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639)
              select new
              {
                Text = l.LName,
                Value = l.LBInt1
              };
      return q;
    }

    public IEnumerable<dynamic> getRiskMapHoraire(int? id)
    {
      var q = from eh in db.tRiskMapHoraire.Where(p => p.RiskMapID == id)
              select new
              {
                id = eh.ID,
                eh.RiskMapID,
                eh.EnvoiHoraireTypeID,
                EnvoiHoraireType = eh.tEnvoiHoraireType.Name,
                eh.Day,
                eh.Month
              };
      return q;
    }

    public IEnumerable<dynamic> addRiskMapHoraire(List<tRiskMapHoraire> data)
    {
      db.tRiskMapHoraire.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.ID);

      var q = from eh in db.tRiskMapHoraire.Where(p => ids.Contains(p.ID))
              select new
              {
                id = eh.ID,
                eh.RiskMapID,
                eh.EnvoiHoraireTypeID,
                EnvoiHoraireType = eh.tEnvoiHoraireType.Name,
                eh.Day,
                eh.Month
              };
      return q;
    }

    public IEnumerable<dynamic> updRiskMapHoraire(List<tRiskMapHoraire> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tRiskMapHoraire.Find(e.ID);
        if (q1 != null)
        {
          q1.EnvoiHoraireTypeID = e.EnvoiHoraireTypeID;
          q1.Day = e.Day;
          q1.Month = e.Month;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);

      var q = from eh in db.tRiskMapHoraire.Where(p => ids.Contains(p.ID))
              select new
              {
                id = eh.ID,
                eh.RiskMapID,
                eh.EnvoiHoraireTypeID,
                EnvoiHoraireType = eh.tEnvoiHoraireType.Name,
                eh.Day,
                eh.Month
              };

      return q;
    }

    public bool delRiskMapHoraire(List<tRiskMapHoraire> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);

        var e = db.tRiskMapHoraire.Where(p => ids.Contains(p.ID));
        db.tRiskMapHoraire.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (Exception /*ex*/)
      {
        return false;
      }
    }

    public IEnumerable<IGrouping<RiskMapGrp, RiskMapCour>> riskMapCourriel(List<int> id, string host)
    {
      var q = (from r1 in
                 (
                   (from r in db.tRiskMap.Where(r => id.Contains(r.ID) && r.EmailTo != null).ToList()
                    from Email in db.tRiskMap.Where(r1 => r1.ID == r.ID).ToList().Select(p => p.EmailTo).SingleOrDefault().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).AsQueryable()
                    select new { i = 0, r.ID, Email })
                   .Union(from r in db.tRiskMap.Where(r => id.Contains(r.ID) && r.EmailCc != null).ToList()
                          from Email in db.tRiskMap.Where(r1 => r1.ID == r.ID).ToList().Select(p => p.EmailCc).SingleOrDefault().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).AsQueryable()
                          select new { i = 1, r.ID, Email }))
               join e in db.tRiskMap on r1.ID equals e.ID
               //join li in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639) on e.Influence equals li.LBInt1 into l1_
               //from li in l1_.DefaultIfEmpty()
               //join lp in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639) on e.Probabilite equals lp.LBInt1 into l2_
               //from lp in l2_.DefaultIfEmpty()
               //join lf in db.taLib.Where(p => p.LConcept == 482639 && p.LParent == 482639) on e.ControlForce equals lf.LBInt1 into l3_
               //from lf in l3_.DefaultIfEmpty()
               //join la in db.taLib.Where(p => p.LConcept == 482644 && p.LParent == 482644) on li.LName + lp.LName + lf.LName equals la.LName into l4_
               //from la in l4_.DefaultIfEmpty()
               select new RiskMapCour
               {
                 i = r1.i,
                 Email = r1.Email,
                 Dep = e.Dep,
                 BisProc = e.BisProc,
                 //e.NumRisk,
                 RiskName = e.RiskName,
                 //InfluenceName = li.LName,
                 //ProbabiliteName = lp.LName,
                 //ControlForceName = lf.LName,
                 //TotalName = la.LName1,
                 //e.EssentielRisk,
                 //e.IsEnabled,
                 c1 = (r1.i == 0 ? e.But : e.Control),
                 //c2 = (r1.i == 0 ? e.PossesseurBut : e.PossesseurControl),
                 PossesseurBut = e.PossesseurBut,
                 PossesseurControl = e.PossesseurControl
               }).GroupBy(l => new RiskMapGrp { Email = l.Email, i = l.i });
      return q;
    }

    public IEnumerable<dynamic> getEMailList(string sort, string dir)
    {
      var q = from l in db.taLib.Where(p => p.LConcept == 458622 && p.LParent == 458622)
              select new { id = l.LID, name = l.LName, email = l.LName1 };

      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> addEMail(List<EMailItem> data)
    {
      var l = data.Select(e => new taLib { LConcept = 458622, LParent = 458622, LName = e.Name, LName1 = e.Email, InDateTime = DateTime.Now });
      db.taLib.AddRange(l);
      db.SaveChanges();

      var ids = l.Select(p => p.LID);
      var q = (from lb in db.taLib.Where(p => ids.Contains(p.LID))
               select new
               {
                 id = lb.LID,
                 name = lb.LName,
                 email = lb.LName1
               });
      return q;
    }

    public IEnumerable<dynamic> updEMail(List<EMailItem> data)
    {
      foreach (var e in data)
      {
        var q1 = db.taLib.FirstOrDefault(p => p.LID == e.ID && p.LConcept == 458622);
        if (q1 != null)
        {
          q1.LName = e.Name;
          q1.LName1 = e.Email;
          q1.InDateTime = DateTime.Now;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);

      var q = from lb in db.taLib.AsNoTracking().Where(p => ids.Contains(p.LID))
              select new { id = lb.LID, name = lb.LName, email = lb.LName1 };
      return q;
    }

    public bool delEMail(List<EMailItem> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);
        var e = db.taLib.Where(o => ids.Contains(o.LID) && o.LConcept == 458622);
        db.taLib.RemoveRange(e);
        db.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool AddToUAKM(int id, int typeId)
    {
      var q = db.taLib.Find(id);
      if (q != null)
      {
        var q1 = new tObjClassifier()
        {
          Name = q.LName,
          Comment = q.LName1,
          ParentID = typeId,
          InDateTime = DateTime.Now,
          UserName = "",
          NameBrief = ""
        };
        db.tObjClassifier.Add(q1);
        db.SaveChanges();
        return true;
      }
      return false;
    }
  }

  public class EnvoiModule : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      this.Bind<IEnvoiRepository>().To<EnvoiRepository>().InRequestScope();
    }
  }

  public class RiskMapGrp
  {
    public string Email { get; set; }
    public int i { get; set; }
  }

  public class RiskMapCour : RiskMapGrp
  {
    public string Dep { get; set; }
    public string BisProc { get; set; }
    public string RiskName { get; set; }
    public string c1 { get; set; }
    public string PossesseurBut { get; set; }
    public string PossesseurControl { get; set; }
  }
}
