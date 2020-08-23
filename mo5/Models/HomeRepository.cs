using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace MO5.Models
{
  public interface IHomeRepository
  {
    IEnumerable<WorkDateList> getWorkDatesList(int year);
    IEnumerable<dynamic> updWorkDate(List<WorkDateList> data);
    bool setupWorkDates(int year);
    DateTime? GetLastWorkDate();
    DateTime GetWorkDateT(DateTime dt, int t);
    int GetTBetweenWorkDates(DateTime dt1, DateTime dt2);
  }

  public class HomeRepository : IHomeRepository
  {
    private MiddleOfficeEntities db = new MiddleOfficeEntities();

    public IEnumerable<WorkDateList> getWorkDatesList(int year)
    {
      var q = from _d in getDates(year)
              join _wd in db.tWorkDate.Where(w => SqlFunctions.DatePart("year", w.WorkDate) == year).Select(w => w.WorkDate) on _d equals _wd into _wd_
              from _wd in _wd_.DefaultIfEmpty()
              select new WorkDateList { d = _d, w = _wd == _d };
      return q;
    }

    public IEnumerable<dynamic> updWorkDate(List<WorkDateList> data)
    {
      foreach (var d in data)
      {
        var wd = db.tWorkDate.FirstOrDefault(w => w.WorkDate == d.d);
        if (wd == null && d.w)
        {
          db.tWorkDate.Add(new tWorkDate { WorkDate = d.d });
        }
        if (wd != null && !d.w)
        {
          db.tWorkDate.Remove(wd);
        }
      }
      db.SaveChanges();
      var q = from _d in data
              join _wd in db.tWorkDate on _d.d equals _wd.WorkDate into _wd_
              from _wd in _wd_.DefaultIfEmpty()
              select new WorkDateList { d = _d.d, w = _wd != null };
      return q;

    }

    public bool setupWorkDates(int year)
    {
      foreach (var d in getDates(year))
      {
        var wd = db.tWorkDate.FirstOrDefault(w => w.WorkDate == d);
        if (wd == null && !(new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday }).Contains(d.DayOfWeek))
        {
          db.tWorkDate.Add(new tWorkDate { WorkDate = d });
        }
        if (wd != null && (new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday }).Contains(d.DayOfWeek))
        {
          db.tWorkDate.Remove(wd);
        }
      }
      db.SaveChanges();
      return true;
    }

    private static IEnumerable<DateTime> getDates(int year)
    {
      for (DateTime d = new DateTime(year, 1, 1); d < new DateTime(year + 1, 1, 1); d = d.AddDays(1))
      {
        yield return d;
      }
    }

    public DateTime? GetLastWorkDate()
    {
      return db.tWorkDate.Where(d => d.WorkDate < DateTime.Today).OrderByDescending(d => d.WorkDate).FirstOrDefault()?.WorkDate;
    }

    public DateTime GetWorkDateT(DateTime dt, int t)
    {
      return db.tWorkDate.Where(d => d.WorkDate >= dt).OrderBy(d => d.WorkDate).Take(t + 1).Max(d => d.WorkDate);
    }

    public int GetTBetweenWorkDates(DateTime dt1, DateTime dt2)
    {
      var t = db.tWorkDate.Where(d => d.WorkDate >= dt1 && d.WorkDate <= dt2).Count();
      return t > 0 ? t - 1 : 0;
    }
  }
  public class HomeModule : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      this.Bind<IHomeRepository>().To<HomeRepository>().InRequestScope();
    }
  }

  public class WorkDateList
  {
    public DateTime d { get; set; }
    public bool w { get; set; }
  }
}