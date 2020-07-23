using MO5.Helpers;
using MO5.Models;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;

namespace MO5.Areas.Code.Models
{
  public interface IInvestDeclRepository
  {
    IEnumerable<dynamic> GetInvestDeclList(bool? enb, int? type, string sort, string dir);
    IEnumerable<dynamic> AddInvestDecl(List<tInvestDecl> data);
    IEnumerable<dynamic> UpdInvestDecl(List<tInvestDecl> data);
    bool DelInvestDecl(List<tInvestDecl> data);

    IEnumerable<dynamic> GetInvestDeclLinkList(int InvDeclID, bool? enb, string sort, string dir);
    IEnumerable<dynamic> AddInvestDeclLink(List<tInvestDeclLink> data);
    IEnumerable<dynamic> UpdInvestDeclLink(List<tInvestDeclLink> data);
    bool DelInvestDeclLink(List<tInvestDeclLink> data);

    IEnumerable<dynamic> GetInvestDeclWhereList(int InvDeclID, bool? enb, string sort, string dir);
    IEnumerable<dynamic> AddInvestDeclWhere(List<tInvestDeclWhere> data);
    IEnumerable<dynamic> UpdInvestDeclWhere(List<tInvestDeclWhere> data);
    bool DelInvestDeclWhere(List<tInvestDeclWhere> data);

    IEnumerable<dynamic> GetInvestDeclSecList(int? InvDeclWhereID, int? div, string sort, string dir);
    IEnumerable<dynamic> AddInvestDeclSec(List<tInvestDeclSec> data);
    IEnumerable<dynamic> UpdInvestDeclSec(List<tInvestDeclSec> data);
    bool DelInvestDeclSec(List<tInvestDeclSec> data);

    IEnumerable<dynamic> GetInvestDeclTypeList();
    IEnumerable<dynamic> GetInvestDeclGroupType();

    IEnumerable<dynamic> GetSecurity(string filter, string sort, string dir);

    IEnumerable<dynamic> GetSecSecGrp(int SecGrpID, string filter, string sort, string dir);
    IEnumerable<dynamic> AddSecSecGrp(List<tSecuritySecurityGroup> data);
    IEnumerable<dynamic> UpdSecSecGrp(List<tSecuritySecurityGroup> data);
    bool DelSecSecGrp(List<tSecuritySecurityGroup> data);

    IEnumerable<dynamic> GetSecurityGroup(string filter, string sort, string dir);
    IEnumerable<dynamic> AddSecurityGroup(List<tSecurityGroup> data);
    IEnumerable<dynamic> UpdSecurityGroup(List<tSecurityGroup> data);
    bool DelSecurityGroup(List<tSecurityGroup> data);
    IEnumerable<dynamic> GetEmitentList(string filter, string sort, string dir);
    IEnumerable<dynamic> GetClientList(string filter, bool? all, string sort, string dir);
    IEnumerable<dynamic> AddFinInst(List<tFinInst> data);
    IEnumerable<dynamic> UpdFinInst(List<tFinInst> data);
    bool DelFinInst(List<tFinInst> data);
    IEnumerable<dynamic> GetTreatyList(string filter, string sort, string dir);
    IEnumerable<dynamic> AddTreaty(List<tTreaty> data);
    IEnumerable<dynamic> UpdTreaty(List<tTreaty> data);
    bool DelTreaty(List<tTreaty> data);

    IEnumerable<dynamic> GetCouponList(int id, string sort, string dir);
    IEnumerable<dynamic> GetAmortList(int id, string sort, string dir);

    IEnumerable<dynamic> GetPortfolioList(string filter, int? TypeID, string sort, string dir);
    IEnumerable<dynamic> AddPortfolio(List<tPortfolio> data);
    IEnumerable<dynamic> UpdPortfolio(List<tPortfolio> data);
    bool DelPortfolio(List<tPortfolio> data);
    IEnumerable<dynamic> GetPortfolioTypeList();
    IEnumerable<dynamic> RepRestDU(DateTime? dt);
    IEnumerable<dynamic> GetPortfolioTreatyList(int TreatyID, int PortfolioTypeID);
    bool AddPortfolioTreaty(int TreatyID, int PortfolioID, DateTime DateStart);
    bool DelPortfolioTreaty(int id);

    ObjectResult<upCheckDecl_Result> CheckDecl(int InvestDeclID, DateTime? dt, bool? withMD, Guid? userID);
    tInvestDecl GetInvDecl(int InvestDeclID);
    IEnumerable<dynamic> GetTreatyByPortfolioList(int PortfolioID, string sort, string dir);

    IEnumerable<dynamic> GetModDeal(Guid UserId, bool All, string sort, string dir);
    IEnumerable<dynamic> AddModDeal(List<tModDeal> data, Guid UserId);
    IEnumerable<dynamic> UpdModDeal(List<tModDeal> data, Guid UserId);
    bool DelModDeal(List<tModDeal> data);
    IEnumerable<dynamic> GetTreatyByPortfClientList(int? PortfolioID, int? FinInstID);
    IEnumerable<dynamic> GetFunds();

    double GetModDealQty(int SecurityID, DateTime dt, int FundID, double Price, double Num);
    double? GetBondNominal(int SecurityID, DateTime dt);
    double GetCoupon(int SecurityID, DateTime dt);
    double GetCouponDayCount(DateTime d1, DateTime d2, int couponBaseId);
    double GetFundRate(int FundID, DateTime RateDate, int QuotingFundID);
    QtyNum GetModDealSetNum(int SecurityID, DateTime dt, int FundID, double Price, double Num, double Qty);
    IEnumerable<dynamic> GetRests<T>(IEnumerable<T> TreatyID, DateTime dt, bool withMD, Guid UserID, bool IsGroupSec);
    IEnumerable<dynamic> getTreatyByPortfs(List<int> id, string sort, string dir);
    ObjectResult<upRepCheckDecl_Result> RepCheckDecl(DateTime? dt);
    ObjectResult<upCheckDeclModDeal_Result> CheckDeclModDeal(DateTime dt, Guid? userID);
  }

  public class InvestDeclRepository : IInvestDeclRepository
  {
    private MiddleOfficeEntities db = new MiddleOfficeEntities() { };

    public InvestDeclRepository()
    {
      db.Database.CommandTimeout = 600;
    }
    public IEnumerable<dynamic> GetInvestDeclList(bool? enb, int? type, string sort, string dir)
    {
      var q1 = db.tInvestDecl.Where(p => 1 == 1);
      if (enb.HasValue)
      {
        q1 = q1.Where(p => p.Enb == enb);
      }
      if (type.HasValue)
      {
        q1 = q1.Where(p => p.InvestDeclTypeID == type);
      }
      var q = from i in q1
              select new
              {
                i.InvestDeclID,
                Name = i.Name == "" ? "&lt;пусто&gt;" : (i.Name ?? "&lt;null&gt;"),
                i.Comment,
                i.Enb,
                i.InvestDeclTypeID,
                CreateDate = i.Create_Date,
                ModifyDate = i.Modify_Date,
                Type = i.tInvestDeclType.NameType
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> AddInvestDecl(List<tInvestDecl> data)
    {
      foreach (var e in data)
      {
        e.Create_Date = DateTime.Now;
      }
      db.tInvestDecl.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.InvestDeclID);
      var q = from i in db.tInvestDecl.Where(p => ids.Contains(p.InvestDeclID))
              select new
              {
                i.InvestDeclID,
                Name = i.Name == "" ? "&lt;пусто&gt;" : (i.Name ?? "&lt;null&gt;"),
                i.Comment,
                i.Enb,
                i.InvestDeclTypeID,
                CreateDate = i.Create_Date,
                ModifyDate = i.Modify_Date,
                Type = i.tInvestDeclType.NameType
              };
      return q;
    }

    public IEnumerable<dynamic> UpdInvestDecl(List<tInvestDecl> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tInvestDecl.Find(e.InvestDeclID);
        if (q1 != null)
        {
          q1.Comment = e.Comment;
          q1.Enb = e.Enb;
          q1.InvestDeclTypeID = e.InvestDeclTypeID;
          q1.Name = e.Name;
          q1.Modify_Date = DateTime.Now;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.InvestDeclID);
      var q = from i in db.tInvestDecl.Where(p => ids.Contains(p.InvestDeclID))
              select new
              {
                i.InvestDeclID,
                Name = i.Name == "" ? "&lt;пусто&gt;" : (i.Name ?? "&lt;null&gt;"),
                i.Comment,
                i.Enb,
                i.InvestDeclTypeID,
                CreateDate = i.Create_Date,
                ModifyDate = i.Modify_Date,
                Type = i.tInvestDeclType.NameType
              };
      return q;
    }

    public bool DelInvestDecl(List<tInvestDecl> data)
    {
      try
      {
        var ids = data.Select(p => p.InvestDeclID);
        var e = db.tInvestDecl.Where(p => ids.Contains(p.InvestDeclID));
        db.tInvestDecl.RemoveRange(e);
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

    public IEnumerable<dynamic> GetInvestDeclTypeList()
    {
      var q = (from i in db.tInvestDeclType
               select new { Text = i.NameType, Value = i.InvestDeclTypeID });
      return q;
    }

    public IEnumerable<dynamic> GetInvestDeclGroupType()
    {
      var q = (from i in db.tInvestDeclGroupType
               select new
               {
                 text = i.Name,
                 value = i.ID
               });
      return q;
    }

    public IEnumerable<dynamic> GetInvestDeclWhereList(int InvDeclID, bool? enb, string sort, string dir)
    {
      var q1 = db.tInvestDeclWhere.Where(p => p.InvestDeclID == InvDeclID);
      if (enb.HasValue)
      {
        q1 = q1.Where(p => p.Enb == enb);
      }
      var q = from i in q1
              join t in db.tInvestDeclGroupType on i.FLAG_Group equals t.ID into _t
              from t in _t.DefaultIfEmpty()
              select new
              {
                i.InvestDeclID,
                i.InvestDeclWhereID,
                i.NameWhere,
                i.Comment,
                i.Enb,
                i.FLAG_Calculation,
                Calculation = i.FLAG_Calculation == 0 ? "Абсолютное" : i.FLAG_Calculation == 1 ? "Процентное" : "",
                i.StartValue,
                i.StopValue,
                i.FLAG_Group,
                Group = t.Name,
                CreateDate = i.Create_Date,
                ModifyDate = i.Modify_Date
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> AddInvestDeclWhere(List<tInvestDeclWhere> data)
    {
      foreach (var e in data)
      {
        e.Create_Date = DateTime.Now;
      }
      db.tInvestDeclWhere.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.InvestDeclWhereID);
      var q = from i in db.tInvestDeclWhere.Where(p => ids.Contains(p.InvestDeclWhereID))
              join t in db.tInvestDeclGroupType on i.FLAG_Group equals t.ID
              select new
              {
                i.InvestDeclID,
                i.InvestDeclWhereID,
                i.NameWhere,
                i.Comment,
                i.Enb,
                i.FLAG_Calculation,
                Calculation = i.FLAG_Calculation == 0 ? "Абсолютное" : i.FLAG_Calculation == 1 ? "Процентное" : "",
                i.StartValue,
                i.StopValue,
                i.FLAG_Group,
                Group = t.Name,
                CreateDate = i.Create_Date,
                ModifyDate = i.Modify_Date
              };
      return q;
    }

    public IEnumerable<dynamic> UpdInvestDeclWhere(List<tInvestDeclWhere> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tInvestDeclWhere.Find(e.InvestDeclWhereID);
        if (q1 != null)
        {
          q1.Comment = e.Comment;
          q1.Enb = e.Enb;
          q1.FLAG_Calculation = e.FLAG_Calculation;
          q1.FLAG_Group = e.FLAG_Group;
          q1.NameWhere = e.NameWhere;
          q1.Modify_Date = DateTime.Now;
          q1.StartValue = e.StartValue;
          q1.StopValue = e.StopValue;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.InvestDeclWhereID);
      var q = from i in db.tInvestDeclWhere.Where(p => ids.Contains(p.InvestDeclWhereID))
              join t in db.tInvestDeclGroupType on i.FLAG_Group equals t.ID
              select new
              {
                i.InvestDeclID,
                i.InvestDeclWhereID,
                i.NameWhere,
                i.Comment,
                i.Enb,
                i.FLAG_Calculation,
                Calculation = i.FLAG_Calculation == 0 ? "Абсолютное" : i.FLAG_Calculation == 1 ? "Процентное" : "",
                i.StartValue,
                i.StopValue,
                i.FLAG_Group,
                Group = t.Name,
                CreateDate = i.Create_Date,
                ModifyDate = i.Modify_Date
              };
      return q;
    }

    public bool DelInvestDeclWhere(List<tInvestDeclWhere> data)
    {
      try
      {
        var ids = data.Select(p => p.InvestDeclWhereID);
        var e = db.tInvestDeclWhere.Where(p => ids.Contains(p.InvestDeclWhereID));
        db.tInvestDeclWhere.RemoveRange(e);
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

    public IEnumerable<dynamic> GetInvestDeclSecList(int? InvDeclWhereID, int? div, string sort, string dir)
    {
      var q1 = db.tInvestDeclSec.Where(p => p.InvestDeclWhereID == InvDeclWhereID);
      if (div.HasValue)
      {
        q1 = q1.Where(p => p.FLAG_Div == div);
      }
      var q = from i in q1
              from s in db.tSecurity.Where(p => i.ObjType == 1 && p.SecurityID == i.ObjID).DefaultIfEmpty()
              from sg in db.tSecurityGroup.Where(p => i.ObjType == 2 && p.SecurityGroupID == i.ObjID).DefaultIfEmpty()
              from f in db.tFinInst.Where(p => i.ObjType == 3 && p.FinInstID == i.ObjID).DefaultIfEmpty()
              from oc in db.tObjClassifier.Where(p => i.ObjType == 4 && p.ObjClassifierID == i.ObjID).DefaultIfEmpty()
              select new
              {
                i.InvestDeclSecID,
                i.InvestDeclWhereID,
                i.FLAG_Div,
                i.FLAG_Not,
                i.ObjType,
                i.ObjID,
                i.Enb,
                ObjTypeS = i.ObjType == 1 ? "Бумага" : i.ObjType == 2 ? "Группа бумаг" : i.ObjType == 3 ? "Эмитент" : "",
                Name = i.ObjType == 1 ? s.Name : i.ObjType == 2 ? sg.Name : i.ObjType == 3 ? f.Name : i.ObjType == 4 ? oc.Name : ""
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> AddInvestDeclSec(List<tInvestDeclSec> data)
    {
      foreach (var e in data)
      {

      }
      db.tInvestDeclSec.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.InvestDeclSecID);
      var q = from i in db.tInvestDeclSec.Where(p => ids.Contains(p.InvestDeclSecID))
              from s in db.tSecurity.Where(p => i.ObjType == 1 && p.SecurityID == i.ObjID).DefaultIfEmpty()
              from sg in db.tSecurityGroup.Where(p => i.ObjType == 2 && p.SecurityGroupID == i.ObjID).DefaultIfEmpty()
              from f in db.tFinInst.Where(p => i.ObjType == 3 && p.FinInstID == i.ObjID).DefaultIfEmpty()
              from oc in db.tObjClassifier.Where(p => i.ObjType == 4 && p.ObjClassifierID == i.ObjID).DefaultIfEmpty()
              select new
              {
                i.InvestDeclSecID,
                i.InvestDeclWhereID,
                i.FLAG_Div,
                i.FLAG_Not,
                i.Enb,
                i.ObjType,
                i.ObjID,
                ObjTypeS = i.ObjType == 1 ? "Бумага" : i.ObjType == 2 ? "Группа бумаг" : i.ObjType == 3 ? "Эмитент" : i.ObjType == 4 ? "Группа эмитентов" : "",
                Name = i.ObjType == 1 ? s.Name : i.ObjType == 2 ? sg.Name : i.ObjType == 3 ? f.Name : i.ObjType == 4 ? oc.Name : ""
              };
      return q;
    }

    public IEnumerable<dynamic> UpdInvestDeclSec(List<tInvestDeclSec> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tInvestDeclSec.Find(e.InvestDeclSecID);
        if (q1 != null)
        {
          q1.FLAG_Div = e.FLAG_Div;
          q1.FLAG_Not = e.FLAG_Not;
          q1.ObjID = e.ObjID;
          q1.ObjType = e.ObjType;
          q1.Enb = e.Enb;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.InvestDeclSecID);
      var q = from i in db.tInvestDeclSec.Where(p => ids.Contains(p.InvestDeclSecID))
              from s in db.tSecurity.Where(p => i.ObjType == 1 && p.SecurityID == i.ObjID).DefaultIfEmpty()
              from sg in db.tSecurityGroup.Where(p => i.ObjType == 2 && p.SecurityGroupID == i.ObjID).DefaultIfEmpty()
              from f in db.tFinInst.Where(p => i.ObjType == 3 && p.FinInstID == i.ObjID).DefaultIfEmpty()
              from oc in db.tObjClassifier.Where(p => i.ObjType == 4 && p.ObjClassifierID == i.ObjID).DefaultIfEmpty()
              select new
              {
                i.InvestDeclSecID,
                i.InvestDeclWhereID,
                i.FLAG_Div,
                i.FLAG_Not,
                i.Enb,
                i.ObjType,
                ObjTypeS = i.ObjType == 1 ? "Бумага" : i.ObjType == 2 ? "Группа бумаг" : i.ObjType == 3 ? "Эмитент" : i.ObjType == 4 ? "Группа эмитентов" : "",
                Name = i.ObjType == 1 ? s.Name : i.ObjType == 2 ? sg.Name : i.ObjType == 3 ? f.Name : i.ObjType == 4 ? oc.Name : ""
              };
      return q;
    }

    public bool DelInvestDeclSec(List<tInvestDeclSec> data)
    {
      try
      {
        var ids = data.Select(p => p.InvestDeclSecID);
        var e = db.tInvestDeclSec.Where(p => ids.Contains(p.InvestDeclSecID));
        db.tInvestDeclSec.RemoveRange(e);
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

    public IEnumerable<dynamic> GetSecurity(string filter, string sort, string dir)
    {
      var q1 = db.tSecurity.Where(p => 1 == 1);
      if (!string.IsNullOrEmpty(filter))
        q1 = q1.Where(p => p.Name.Contains(filter) || p.ISIN.Contains(filter) || p.tFinInst.Name.Contains(filter) || p.tFinInst.INN.Contains(filter));
      var q = from s in q1
              join st in db.tSecType on s.Class equals st.SecTypeID into _st
              from st in _st.DefaultIfEmpty()
              select new
              {
                s.SecurityID,
                s.Name,
                s.ISIN,
                s.RegNumber,
                s.DateEnd,
                s.Class,
                SecType = st.Name,
                s.NominalFundID,
                Issuer = s.tFinInst.Name,
                IssuerINN = s.tFinInst.INN
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q.Take(500);
    }

    public IEnumerable<dynamic> GetSecurityGroup(string filter, string sort, string dir)
    {
      var q1 = db.tSecurityGroup.Where(p => p.SecurityGroupTypeID == 5);
      if (!string.IsNullOrEmpty(filter))
        q1 = q1.Where(p => p.Name.Contains(filter));
      var q = from sg in q1
              select new
              {
                sg.SecurityGroupID,
                sg.Name,
                sg.SecurityGroupTypeID
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q.Take(500);
    }

    public IEnumerable<dynamic> AddSecurityGroup(List<tSecurityGroup> data)
    {
      foreach (var e in data)
      {
        e.SecurityGroupTypeID = 5;
      }
      db.tSecurityGroup.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.SecurityGroupID);
      var q = from sg in db.tSecurityGroup.Where(p => ids.Contains(p.SecurityGroupID))
              select new
              {
                sg.SecurityGroupID,
                sg.Name,
                sg.SecurityGroupTypeID
              };
      return q;
    }

    public IEnumerable<dynamic> UpdSecurityGroup(List<tSecurityGroup> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tSecurityGroup.Find(e.SecurityGroupID);
        if (q1 != null)
        {
          q1.Name = e.Name;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.SecurityGroupID);
      var q = from sg in db.tSecurityGroup.Where(p => ids.Contains(p.SecurityGroupID))
              select new
              {
                sg.SecurityGroupID,
                sg.Name,
                sg.SecurityGroupTypeID
              };
      return q;
    }

    public bool DelSecurityGroup(List<tSecurityGroup> data)
    {
      try
      {
        var ids = data.Select(p => p.SecurityGroupID);
        var e = db.tSecurityGroup.Where(p => ids.Contains(p.SecurityGroupID));
        db.tSecurityGroup.RemoveRange(e);
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
                fi.Name1
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q.Take(500);
    }

    public IEnumerable<dynamic> GetSecSecGrp(int SecGrpID, string filter, string sort, string dir)
    {
      var q1 = db.tSecurity.Where(p => 1 == 1);
      if (!string.IsNullOrEmpty(filter))
        q1 = q1.Where(p => p.Name.Contains(filter) || p.ISIN.Contains(filter));

      var q = from ssg in db.tSecuritySecurityGroup.Where(p => p.SecurityGroupID == SecGrpID)
              join s in q1 on ssg.SecurityID equals s.SecurityID
              join st in db.tSecType on s.Class equals st.SecTypeID into _st
              from st in _st.DefaultIfEmpty()
              select new
              {
                ssg.ID,
                ssg.SecurityGroupID,
                ssg.SecurityID,
                ssg.IsActive,
                ssg.InDateTime,
                s.Name,
                s.ISIN,
                s.RegNumber,
                s.DateEnd,
                SecType = st.Name
              };

      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q.Take(500);
    }

    public IEnumerable<dynamic> AddSecSecGrp(List<tSecuritySecurityGroup> data)
    {
      foreach (var e in data)
      {
        e.InDateTime = DateTime.Now;
      }
      db.tSecuritySecurityGroup.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.ID);
      var q = from ssg in db.tSecuritySecurityGroup.Where(p => ids.Contains(p.ID))
              join s in db.tSecurity on ssg.SecurityID equals s.SecurityID
              join st in db.tSecType on s.Class equals st.SecTypeID into _st
              from st in _st.DefaultIfEmpty()
              select new
              {
                ssg.ID,
                ssg.SecurityGroupID,
                ssg.SecurityID,
                ssg.IsActive,
                s.Name,
                s.ISIN,
                s.RegNumber,
                s.DateEnd,
                SecType = st.Name
              };
      return q;
    }

    public IEnumerable<dynamic> UpdSecSecGrp(List<tSecuritySecurityGroup> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tSecuritySecurityGroup.Find(e.ID);
        if (q1 != null)
        {
          q1.SecurityID = e.SecurityID;
          q1.IsActive = e.IsActive;
          q1.InDateTime = DateTime.Now;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.ID);
      var q = from ssg in db.tSecuritySecurityGroup.Where(p => ids.Contains(p.ID))
              join s in db.tSecurity on ssg.SecurityID equals s.SecurityID
              join st in db.tSecType on s.Class equals st.SecTypeID into _st
              from st in _st.DefaultIfEmpty()
              select new
              {
                ssg.ID,
                ssg.SecurityGroupID,
                ssg.SecurityID,
                ssg.IsActive,
                s.Name,
                s.ISIN,
                s.RegNumber,
                s.DateEnd,
                SecType = st.Name
              };
      return q;
    }

    public bool DelSecSecGrp(List<tSecuritySecurityGroup> data)
    {
      try
      {
        var ids = data.Select(p => p.ID);

        var e = db.tSecuritySecurityGroup.Where(p => ids.Contains(p.ID));
        db.tSecuritySecurityGroup.RemoveRange(e);
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

    public IEnumerable<dynamic> GetInvestDeclLinkList(int InvDeclID, bool? enb, string sort, string dir)
    {
      var q1 = db.tInvestDeclLink.Where(p => p.InvestDeclID == InvDeclID);
      if (enb.HasValue)
      {
        q1 = q1.Where(p => p.Enb == enb);
      }
      var q = from i in q1
              from f in db.tFinInst.Where(p => i.ObjType == 3 && p.FinInstID == i.ObjID).DefaultIfEmpty()
              from t in db.tTreaty.Where(p => i.ObjType == 5 && p.TreatyID == i.ObjID).DefaultIfEmpty()
              from p in db.tPortfolio.Where(p => i.ObjType == 1 && p.PortfolioID == i.ObjID).DefaultIfEmpty()
              select new
              {
                i.InvestDeclID,
                i.InvestDeclLinkID,
                i.Enb,
                i.DateStart,
                i.DateFinish,
                i.ObjID,
                i.ObjType,
                ObjTypeS = i.ObjType == 3 ? "Клиент" : i.ObjType == 5 ? "Договор" : i.ObjType == 1 ? "Портфель" : "",
                Name = i.ObjType == 3 ? f.Name : i.ObjType == 5 ? t.Name : i.ObjType == 1 ? p.Name : ""
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> AddInvestDeclLink(List<tInvestDeclLink> data)
    {
      db.tInvestDeclLink.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.InvestDeclLinkID);
      var q = from i in db.tInvestDeclLink.Where(p => ids.Contains(p.InvestDeclLinkID))
              from f in db.tFinInst.Where(p => i.ObjType == 3 && p.FinInstID == i.ObjID).DefaultIfEmpty()
              from t in db.tTreaty.Where(p => i.ObjType == 5 && p.TreatyID == i.ObjID).DefaultIfEmpty()
              from p in db.tPortfolio.Where(p => i.ObjType == 1 && p.PortfolioID == i.ObjID).DefaultIfEmpty()
              select new
              {
                i.InvestDeclID,
                i.InvestDeclLinkID,
                i.Enb,
                i.DateStart,
                i.DateFinish,
                i.ObjID,
                i.ObjType,
                ObjTypeS = i.ObjType == 3 ? "Клиент" : i.ObjType == 5 ? "Договор" : i.ObjType == 1 ? "Портфель" : "",
                Name = i.ObjType == 3 ? f.Name : i.ObjType == 5 ? t.Name : i.ObjType == 1 ? p.Name : ""
              };
      return q;
    }

    public IEnumerable<dynamic> UpdInvestDeclLink(List<tInvestDeclLink> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tInvestDeclLink.Find(e.InvestDeclLinkID);
        if (q1 != null)
        {
          q1.DateFinish = e.DateFinish;
          q1.DateStart = e.DateStart;
          q1.Enb = e.Enb;
          q1.ObjID = e.ObjID;
          q1.ObjType = e.ObjType;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.InvestDeclLinkID);
      var q = from i in db.tInvestDeclLink.Where(p => ids.Contains(p.InvestDeclLinkID))
              from f in db.tFinInst.Where(p => i.ObjType == 3 && p.FinInstID == i.ObjID).DefaultIfEmpty()
              from t in db.tTreaty.Where(p => i.ObjType == 5 && p.TreatyID == i.ObjID).DefaultIfEmpty()
              from p in db.tPortfolio.Where(p => i.ObjType == 1 && p.PortfolioID == i.ObjID).DefaultIfEmpty()
              select new
              {
                i.InvestDeclID,
                i.InvestDeclLinkID,
                i.Enb,
                i.DateStart,
                i.DateFinish,
                i.ObjID,
                i.ObjType,
                ObjTypeS = i.ObjType == 3 ? "Клиент" : i.ObjType == 5 ? "Договор" : i.ObjType == 1 ? "Портфель" : "",
                Name = i.ObjType == 3 ? f.Name : i.ObjType == 5 ? t.Name : i.ObjType == 1 ? p.Name : ""
              };
      return q;
    }

    public bool DelInvestDeclLink(List<tInvestDeclLink> data)
    {
      try
      {
        var ids = data.Select(p => p.InvestDeclLinkID);
        var e = db.tInvestDeclLink.Where(p => ids.Contains(p.InvestDeclLinkID));
        db.tInvestDeclLink.RemoveRange(e);
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

    public IEnumerable<dynamic> GetClientList(string filter, bool? all, string sort, string dir)
    {
      var q1 = db.tFinInst.Where(p => 1 == 1);
      if (all != true)
        q1 = q1.Where(p => db.tTreaty.Any(t => t.FinInstID == p.FinInstID));
      if (!string.IsNullOrEmpty(filter))
        q1 = q1.Where(p => p.Name.Contains(filter));
      var q = from fi in q1
              select new
              {
                fi.FinInstID,
                fi.Name
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> AddFinInst(List<tFinInst> data)
    {
      if (data.Count() > 0)
      {
        var fid = db.tFinInst.Max(p => p.FinInstID);
        foreach (var e in data)
        {
          e.FinInstID = ++fid;
        }
      }
      db.tFinInst.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.FinInstID);
      var q = from fi in db.tFinInst.Where(p => ids.Contains(p.FinInstID))
              select new
              {
                fi.FinInstID,
                fi.Name,
                fi.INN,
                fi.KPP,
                fi.Name1
              };
      return q;
    }

    public IEnumerable<dynamic> UpdFinInst(List<tFinInst> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tFinInst.Find(e.FinInstID);
        if (q1 != null)
        {
          q1.Name = e.Name;
          q1.INN = e.INN;
          q1.KPP = e.KPP;
          q1.Name1 = e.Name1;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.FinInstID);
      var q = from fi in db.tFinInst.Where(p => ids.Contains(p.FinInstID))
              select new
              {
                fi.FinInstID,
                fi.Name,
                fi.INN,
                fi.KPP,
                fi.Name1
              };
      return q;
    }

    public bool DelFinInst(List<tFinInst> data)
    {
      try
      {
        var ids = data.Select(p => p.FinInstID);
        var e = db.tFinInst.Where(p => ids.Contains(p.FinInstID));
        db.tFinInst.RemoveRange(e);
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

    public IEnumerable<dynamic> GetTreatyByPortfolioList(int PortfolioID, string sort, string dir)
    {
      var q = from pt in db.tPortfolioTreaty
              join f in db.tFinInst on pt.tTreaty.FinInstID equals f.FinInstID into f_
              from f in f_.DefaultIfEmpty()
              where pt.PortfolioID == PortfolioID
              select new
              {
                pt.ID,
                pt.TreatyID,
                pt.PortfolioID,
                pt.DateStart,
                pt.DateFinish,
                TreatyName = pt.tTreaty.Name,
                ClientName = f.Name
              };

      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> GetTreatyByPortfClientList(int? PortfolioID, int? FinInstID)
    {
      var dt = DateTime.Today;
      var q2 = db.tTreaty.Where(p => 1 == 1);
      if (PortfolioID > 0)
        q2 = q2.Where(p => p.tPortfolioTreaty.Where(f => f.DateStart <= dt && f.DateFinish > dt && f.PortfolioID == PortfolioID).Any());
      else if (FinInstID > 0)
        q2 = q2.Where(p => p.FinInstID == FinInstID);
      else
        q2 = q2.Where(p => 1 == 2);
      //var q1 = db.tPortfolioTreaty.Where(p => p.DateStart <= dt && p.DateFinish > dt);
      //if (PortfolioID.Value > 0)
      //  q1 = q1.Where(p => p.PortfolioID == PortfolioID);
      //else if (FinInstID.Value > 0)
      //  q1 = q1.Where(p => p.tTreaty.FinInstID == FinInstID);
      //else
      //  q1 = q1.Where(p => 1 == 2);

      var q = from qq in q2
              select new
              {
                qq.TreatyID,
                TreatyName = qq.Name
              };

      return q;
    }

    public IEnumerable<dynamic> AddTreaty(List<tTreaty> data)
    {
      if (data.Count() > 0)
      {
        var tid = db.tTreaty.Max(p => p.TreatyID);
        foreach (var e in data)
        {
          e.TreatyID = ++tid;
        }
      }
      db.tTreaty.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.TreatyID);
      var q = from tr in db.tTreaty.Where(p => ids.Contains(p.TreatyID))
              join f in db.tFinInst on tr.FinInstID equals f.FinInstID into f_
              from f in f_.DefaultIfEmpty()
              select new
              {
                tr.TreatyID,
                tr.Name,
                tr.FinInstID,
                ClientName = f.Name,
                tr.IsDisabled,
                tr.DateStart,
                tr.DateFinish
              };
      return q;
    }

    public IEnumerable<dynamic> UpdTreaty(List<tTreaty> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tTreaty.Find(e.TreatyID);
        if (q1 != null)
        {
          q1.DateFinish = e.DateFinish;
          q1.DateStart = e.DateStart;
          q1.IsDisabled = e.IsDisabled;
          q1.Name = e.Name;
          q1.FinInstID = e.FinInstID;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.TreatyID);
      var q = from tr in db.tTreaty.Where(p => ids.Contains(p.TreatyID))
              join f in db.tFinInst on tr.FinInstID equals f.FinInstID into f_
              from f in f_.DefaultIfEmpty()
              select new
              {
                tr.TreatyID,
                tr.Name,
                tr.FinInstID,
                ClientName = f.Name,
                tr.IsDisabled,
                tr.DateStart,
                tr.DateFinish
              };
      return q;
    }

    public bool DelTreaty(List<tTreaty> data)
    {
      try
      {
        var ids = data.Select(p => p.TreatyID);
        var e = db.tTreaty.Where(p => ids.Contains(p.TreatyID));
        db.tTreaty.RemoveRange(e);
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

    public IEnumerable<dynamic> GetCouponList(int id, string sort, string dir)
    {
      var q = from c in db.tCoupon.Where(p => p.SecurityID == id)
              orderby c.Num, c.DateStart
              select new
              {
                c.ID,
                c.Num,
                c.DateStart,
                c.DateEnd,
                c.DatePay,
                c.Rate,
                c.Price,
                c.SecurityID
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));

      return q;
    }

    public IEnumerable<dynamic> GetAmortList(int id, string sort, string dir)
    {
      var q = from c in db.tAmortization.Where(p => p.SecurityID == id)
              orderby c.ADate
              select new
              {
                c.ID,
                c.ADate,
                c.Nominal,
                c.Value,
                c.SecurityID
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> GetPortfolioList(string filter, int? TypeID, string sort, string dir)
    {
      var q1 = db.tPortfolio.Where(p => 1 == 1);
      if (TypeID.HasValue)
        q1 = q1.Where(p => p.PortfolioTypeID == TypeID);
      if (!string.IsNullOrEmpty(filter))
        q1 = q1.Where(p => p.Name.Contains(filter));
      var q = from qp in q1
              select new
              {
                qp.PortfolioID,
                qp.Name,
                qp.PortfolioTypeID,
                PortfolioType = qp.tPortfolioType.Name
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> GetPortfolioTypeList()
    {
      var q = from p in db.tPortfolioType
              select new
              {
                p.PortfolioTypeID,
                p.Name
              };
      return q;
    }

    public ObjectResult<upCheckDecl_Result> CheckDecl(int InvestDeclID, DateTime? dt, bool? withMD, Guid? userID)
    {
      dt = dt ?? DateTime.Today;
      var q3 = db.upCheckDecl(investDeclID: InvestDeclID, dt, withMD, userID, false, false, false);


      //var q1 = (from idl in db.tInvestDeclLink
      //          where idl.InvestDeclID == InvestDeclID && idl.Enb == true && idl.ObjType == 5
      //          select new { TreatyID = (int)idl.ObjID })

      //         .Union

      //         (from idl in db.tInvestDeclLink
      //          join tr in db.tTreaty.Where(p => p.IsDisabled != true) on idl.ObjID equals tr.FinInstID
      //          where idl.InvestDeclID == InvestDeclID && idl.Enb == true && idl.ObjType == 3
      //          select new { tr.TreatyID })

      //         .Union

      //         (from idl in db.tInvestDeclLink
      //          join pt in db.tPortfolioTreaty.Where(p => p.DateStart <= dt && p.DateFinish >= dt) on idl.ObjID equals pt.PortfolioID
      //          where idl.InvestDeclID == InvestDeclID && idl.Enb == true && idl.ObjType == 1
      //          select new { TreatyID = (int)pt.TreatyID }).Select(p => p.TreatyID);

      //var q2 = (from t in (from r in db.tODRests
      //                     where q1.Contains(r.Reg3ID ?? 0)
      //                     join t in db.tODTurns.Where(p => p.TDate >= dt) on r.ID equals t.RestID
      //                     group new { r, t } by new { r.Reg3ID, r.ValueID } into grp
      //                     select new
      //                     {
      //                       grp.Key.Reg3ID,
      //                       grp.Key.ValueID,
      //                       Num = grp.Sum(p => p.t.Type * p.t.Value)
      //                     })
      //          join tr in db.tTreaty on t.Reg3ID equals tr.TreatyID into _tr
      //          from tr in _tr.DefaultIfEmpty()
      //            //join f in db.tFinInst on tr.FinInstID equals f.FinInstID into _f
      //            //from f in _f.DefaultIfEmpty()
      //          join s in db.tSecurity on t.ValueID equals s.SecurityID into _s
      //          from s in _s.DefaultIfEmpty()
      //          join sr in db.tSecurityRate.Where(p => p.RateDate == dt) on t.ValueID equals sr.SecurityID into _sr
      //          from sr in _sr.DefaultIfEmpty()
      //          select new
      //          {
      //            TreatyID = t.Reg3ID,
      //            tr.FinInstID,
      //            SecurityID = t.ValueID,
      //            Num = (s.Class == 0 ? -1 : 1) * t.Num,
      //            sr.Course,
      //            sr.Coupon,
      //            Qty = (s.Class == 0 ? -1 : 1) * t.Num * sr.Course
      //          });
      ////var q2 = q.ToList();

      //var q3 = (from t2 in q2
      //          join s in db.tSecurity on t2.SecurityID equals s.SecurityID
      //          from fd in new List<int> { 0, 1 }
      //          from idw in (
      //          from idw in db.tInvestDeclWhere
      //          where idw.InvestDeclID == InvestDeclID && idw.Enb == true && (fd == 0 || idw.FLAG_Calculation == 1)
      //          &&
      //          (
      //            from ids in db.tInvestDeclSec
      //            where ids.InvestDeclWhereID == idw.InvestDeclWhereID && ids.Enb == true && ids.FLAG_Div == fd && ids.FLAG_Not == 1 && ids.ObjType == 2 && ids.ObjID == 3
      //            select ids)
      //          .Concat
      //          (
      //            from ids in db.tInvestDeclSec
      //            join ssg in db.tSecuritySecurityGroup.Where(p => p.SecurityID == t2.SecurityID) on ids.ObjID equals ssg.SecurityGroupID
      //            where ids.InvestDeclWhereID == idw.InvestDeclWhereID && ids.Enb == true && ids.FLAG_Div == fd && ids.FLAG_Not == 1 && ids.ObjType == 2
      //            select ids)
      //          .Concat
      //          (
      //            from ids in db.tInvestDeclSec
      //            where ids.InvestDeclWhereID == idw.InvestDeclWhereID && ids.Enb == true && ids.FLAG_Div == fd && ids.FLAG_Not == 1 && ids.ObjType == 1 & ids.ObjID == t2.SecurityID
      //            select ids)
      //          .Concat
      //          (
      //            from ids in db.tInvestDeclSec
      //            where ids.InvestDeclWhereID == idw.InvestDeclWhereID && ids.Enb == true && ids.FLAG_Div == fd && ids.FLAG_Not == 1 && ids.ObjType == 3 & ids.ObjID == s.IssuerID
      //            select ids)
      //          .Any() == true
      //          &&
      //          (
      //            from ids in db.tInvestDeclSec
      //            join ssg in db.tSecuritySecurityGroup.Where(p => p.SecurityID == t2.SecurityID) on ids.ObjID equals ssg.SecurityGroupID
      //            where ids.InvestDeclWhereID == idw.InvestDeclWhereID && ids.Enb == true && ids.FLAG_Div == fd && ids.FLAG_Not == 0 && ids.ObjType == 2
      //            select ids)
      //          .Concat
      //          (
      //            from ids in db.tInvestDeclSec
      //            where ids.InvestDeclWhereID == idw.InvestDeclWhereID && ids.Enb == true && ids.FLAG_Div == fd && ids.FLAG_Not == 0 && ids.ObjType == 1 & ids.ObjID == t2.SecurityID
      //            select ids)
      //          .Concat
      //          (
      //            from ids in db.tInvestDeclSec
      //            where ids.InvestDeclWhereID == idw.InvestDeclWhereID && ids.Enb == true && ids.FLAG_Div == fd && ids.FLAG_Not == 0 && ids.ObjType == 3 & ids.ObjID == s.IssuerID
      //            select ids)
      //          .Any() == false
      //          select idw
      //          ).DefaultIfEmpty()
      //          select new
      //          {
      //            InvestDeclWhereID = (int?)idw.InvestDeclWhereID,
      //            FinInstID = idw.FLAG_Group == 18 /* Лимит на контрагента*/ ? null : t2.FinInstID,
      //            t2.SecurityID,
      //            s.IssuerID,
      //            FLAG_Div = fd,
      //            t2.Num,
      //            t2.Course,
      //            t2.Coupon
      //          }).ToList();

      return q3;
    }

    public tInvestDecl GetInvDecl(int InvestDeclID)
    {
      return db.tInvestDecl.Find(InvestDeclID);
    }

    public IEnumerable<dynamic> RepRestDU(DateTime? dt)
    {
      dt = dt ?? DateTime.Today;
      var q = from t in (from r in db.tODRests
                         join t in db.tODTurns.Where(p => p.TDate >= dt) on r.ID equals t.RestID
                         group new { r, t } by new { r.Reg3ID, r.ValueID } into grp
                         let s = grp.Sum(p => p.t.Type * p.t.Value)
                         where s != 0
                         select new
                         {
                           grp.Key.Reg3ID,
                           grp.Key.ValueID,
                           Num = grp.Sum(p => p.t.Type * p.t.Value)
                         })
              join tr in db.tTreaty on t.Reg3ID equals tr.TreatyID into _tr
              from tr in _tr.DefaultIfEmpty()
              join f in db.tFinInst on tr.FinInstID equals f.FinInstID into _f
              from f in _f.DefaultIfEmpty()
              join s in db.tSecurity on t.ValueID equals s.SecurityID into _s
              from s in _s.DefaultIfEmpty()
              join e in db.tFinInst on s.IssuerID equals e.FinInstID into _e
              from e in _e.DefaultIfEmpty()
              join sr in db.tSecurityRate.Where(p => p.RateDate == dt) on t.ValueID equals sr.SecurityID into _sr
              from sr in _sr.DefaultIfEmpty()
              select new
              {
                TreatyID = t.Reg3ID,
                TreatyName = tr.Name,
                FinInstName = f.Name,
                SecName = s.Name,
                Issuer = e.Name,
                INN = e.INN,
                s.ISIN,
                Num = (s.Class == 0 ? 1 * -1 : 1) * t.Num,
                Course = sr.Course != null ? sr.Course : (s.Class == 4 && s.IssuerID == 12418022 ? s.Nominal : 0),
                Qty = (s.Class == 0 ? 1 * -1 : 1) * t.Num * sr.Course != null ? sr.Course : (s.Class == 4 && s.IssuerID == 12418022 ? s.Nominal : 0),
                Coupon = sr.Coupon ?? 0,
                SecurityID = (int?)s.SecurityID
              };
      return q.OrderBy(p => p.FinInstName).ThenBy(p => p.TreatyName).ThenBy(p => p.SecName);
    }

    public IEnumerable<dynamic> AddPortfolio(List<tPortfolio> data)
    {
      db.tPortfolio.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.PortfolioID);
      var q = from qp in db.tPortfolio.Where(p => ids.Contains(p.PortfolioID))
              select new
              {
                qp.PortfolioID,
                qp.Name,
                qp.PortfolioTypeID,
                PortfolioType = qp.tPortfolioType.Name
              };
      return q;
    }

    public IEnumerable<dynamic> UpdPortfolio(List<tPortfolio> data)
    {
      foreach (var e in data)
      {
        var q1 = db.tPortfolio.Find(e.PortfolioID);
        if (q1 != null)
        {
          q1.InDateTime = DateTime.Now;
          q1.Name = e.Name;
          q1.PortfolioTypeID = e.PortfolioTypeID;
          db.SaveChanges();
        }
      }
      var ids = data.Select(p => p.PortfolioID);
      var q = from qp in db.tPortfolio.Where(p => ids.Contains(p.PortfolioID))
              select new
              {
                qp.PortfolioID,
                qp.Name,
                qp.PortfolioTypeID,
                PortfolioType = qp.tPortfolioType.Name
              };
      return q;
    }

    public bool DelPortfolio(List<tPortfolio> data)
    {
      try
      {
        var ids = data.Select(p => p.PortfolioID);
        var e = db.tPortfolio.Where(p => ids.Contains(p.PortfolioID));
        db.tPortfolio.RemoveRange(e);
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

    public IEnumerable<dynamic> GetPortfolioTreatyList(int TreatyID, int PortfolioTypeID)
    {
      var q = from pt in db.tPortfolioTreaty
              where pt.TreatyID == TreatyID
              where pt.tPortfolio.PortfolioTypeID == PortfolioTypeID
              orderby pt.DateStart
              select new
              {
                pt.ID,
                pt.TreatyID,
                pt.PortfolioID,
                pt.DateStart,
                pt.DateFinish,
                pt.tPortfolio.Name,
                TypeName = pt.tPortfolio.tPortfolioType.Name
              };
      return q;
    }

    public bool AddPortfolioTreaty(int TreatyID, int PortfolioID, DateTime DateStart)
    {
      var treaty = db.tTreaty.Find(TreatyID);
      if (treaty == null)
        return false;
      var portfolio = db.tPortfolio.Find(PortfolioID);
      if (portfolio == null)
        return false;
      var maxDateStart = db.tPortfolioTreaty.Where(p => p.TreatyID == TreatyID && p.tPortfolio.PortfolioTypeID == portfolio.PortfolioTypeID).Max(p => (DateTime?)p.DateStart);
      if (maxDateStart >= DateStart)
        return false;
      if (maxDateStart.HasValue)
      {
        var pu = db.tPortfolioTreaty.First(p => p.TreatyID == TreatyID && p.tPortfolio.PortfolioTypeID == portfolio.PortfolioTypeID && p.DateStart == maxDateStart.Value);
        if (pu.PortfolioID == PortfolioID)
          return false;
        pu.DateFinish = DateStart.AddDays(-1);
      }
      var ptn = new tPortfolioTreaty();
      ptn.PortfolioID = PortfolioID;
      ptn.TreatyID = TreatyID;
      ptn.DateStart = DateStart;
      ptn.DateFinish = new DateTime(2050, 12, 31);
      ptn.InDateTime = DateTime.Now;
      db.tPortfolioTreaty.Add(ptn);
      db.SaveChanges();
      return true;
    }

    public bool DelPortfolioTreaty(int id)
    {
      var pt = db.tPortfolioTreaty.Find(id);
      if (pt == null)
        return false;
      if (db.tPortfolioTreaty.Any(p => p.TreatyID == pt.TreatyID && p.DateStart > pt.DateStart && p.tPortfolio.PortfolioTypeID == pt.tPortfolio.PortfolioTypeID))
        return false;
      var maxDateStart = db.tPortfolioTreaty.Where(p => p.TreatyID == pt.TreatyID && p.tPortfolio.PortfolioTypeID == pt.tPortfolio.PortfolioTypeID && p.DateStart < pt.DateStart).Max(p => (DateTime?)p.DateStart);
      if (maxDateStart.HasValue)
      {
        var pup = db.tPortfolioTreaty.Where(p => p.TreatyID == pt.TreatyID && p.tPortfolio.PortfolioTypeID == pt.tPortfolio.PortfolioTypeID && p.DateStart == maxDateStart.Value).First();
        pup.DateFinish = new DateTime(2050, 12, 31);
      }
      db.tPortfolioTreaty.Remove(pt);
      db.SaveChanges();
      return true;
    }

    public IEnumerable<dynamic> GetModDeal(Guid UserId, bool All, string sort, string dir)
    {
      var q1 = db.tModDeal.AsEnumerable();
      if (!All)
        q1 = q1.Where(p => p.UserId == UserId);
      var q = from md in q1
              join u in db.aspnet_Users on md.UserId equals u.UserId into _u
              from u in _u.DefaultIfEmpty()
              select new
              {
                md.DealDate,
                md.DealPrice,
                md.Direction,
                md.FinInstID,
                md.FundID,
                md.InDateTime,
                md.ModDealID,
                md.Num,
                md.PortfolioID,
                Qty = Math.Round(md.Qty ?? 0, 2),
                md.AccInt,
                md.SecurityID,
                md.SupplyDate,
                md.TreatyID,
                md.UserId,
                md.ValueDate,
                SecName = md.tSecurity?.Name,
                md.tSecurity?.ISIN,
                ClientName = md.tFinInst?.Name,
                TreatyName = md.tTreaty?.Name,
                Fund = md.tFund?.Name,
                PortfName = md.tPortfolio?.Name,
                md.DealTypeID,
                md.CreateDate
              };
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }

    public IEnumerable<dynamic> AddModDeal(List<tModDeal> data, Guid UserId)
    {
      foreach (var e in data)
      {
        e.InDateTime = DateTime.Now;
        var dt = DateTime.Now;
        e.CreateDate = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        e.UserId = UserId;
        if (e.SecurityID.HasValue && e.DealDate.HasValue && e.DealPrice.HasValue && e.Num.HasValue)
          e.Qty = GetModDealQty(e.SecurityID.Value, e.DealDate.Value, e.FundID.Value, e.DealPrice.Value, (double)e.Num.Value);
      }
      db.tModDeal.AddRange(data);
      db.SaveChanges();
      var ids = data.Select(p => p.ModDealID);
      var q = from md in db.tModDeal.Where(p => ids.Contains(p.ModDealID))
              join u in db.aspnet_Users on md.UserId equals u.UserId into _u
              from u in _u.DefaultIfEmpty()
              select new
              {
                md.DealDate,
                md.DealPrice,
                md.Direction,
                md.FinInstID,
                md.FundID,
                md.InDateTime,
                md.ModDealID,
                md.Num,
                md.PortfolioID,
                Qty = Math.Round(md.Qty ?? 0, 2),
                md.AccInt,
                md.SecurityID,
                md.SupplyDate,
                md.TreatyID,
                md.UserId,
                md.ValueDate,
                SecName = md.tSecurity.Name,
                md.tSecurity.ISIN,
                ClientName = md.tFinInst.Name,
                TreatyName = md.tTreaty.Name,
                Fund = md.tFund.Name,
                PortfName = md.tPortfolio.Name,
                md.DealTypeID,
                md.CreateDate
              };
      return q;
    }

    public IEnumerable<dynamic> UpdModDeal(List<tModDeal> data, Guid UserId)
    {
      foreach (var e in data)
      {
        var q1 = db.tModDeal.Find(e.ModDealID);
        if (q1 != null)
        {
          q1.InDateTime = DateTime.Now;
          q1.UserId = UserId;
          q1.AccInt = e.AccInt;
          q1.DealDate = e.DealDate;
          q1.DealPrice = e.DealPrice;
          q1.Direction = e.Direction;
          q1.FinInstID = e.FinInstID;
          q1.FundID = e.FundID;
          q1.Num = e.Num;
          q1.PortfolioID = e.PortfolioID;
          q1.SecurityID = e.SecurityID;
          q1.SupplyDate = e.SupplyDate;
          q1.TreatyID = e.TreatyID;
          q1.ValueDate = e.ValueDate;
          q1.DealTypeID = e.DealTypeID;
          q1.CreateDate = e.CreateDate;
          if (q1.SecurityID.HasValue && q1.DealDate.HasValue && q1.DealPrice.HasValue && q1.Num.HasValue)
            q1.Qty = GetModDealQty(q1.SecurityID.Value, q1.DealDate.Value, q1.FundID.Value, q1.DealPrice.Value, (double)q1.Num.Value);
          db.SaveChanges();
        }
      }

      var ids = data.Select(p => p.ModDealID);
      var q = from md in db.tModDeal.Where(p => ids.Contains(p.ModDealID))
              join u in db.aspnet_Users on md.UserId equals u.UserId into _u
              from u in _u.DefaultIfEmpty()
              select new
              {
                md.DealDate,
                md.DealPrice,
                md.Direction,
                md.FinInstID,
                md.FundID,
                md.InDateTime,
                md.ModDealID,
                md.Num,
                md.PortfolioID,
                Qty = Math.Round(md.Qty ?? 0, 2),
                md.AccInt,
                md.SecurityID,
                md.SupplyDate,
                md.TreatyID,
                md.UserId,
                md.ValueDate,
                SecName = md.tSecurity.Name,
                md.tSecurity.ISIN,
                ClientName = md.tFinInst.Name,
                TreatyName = md.tTreaty.Name,
                Fund = md.tFund.Name,
                PortfName = md.tPortfolio.Name,
                md.DealTypeID,
                md.CreateDate
              };
      return q;
    }

    public bool DelModDeal(List<tModDeal> data)
    {
      try
      {
        var ids = data.Select(p => p.ModDealID);
        var e = db.tModDeal.Where(p => ids.Contains(p.ModDealID));
        db.tModDeal.RemoveRange(e);
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

    public double GetModDealQty(int SecurityID, DateTime dt, int FundID, double Price, double Num)
    {
      double Qty = 0;
      var sec = db.tSecurity.FirstOrDefault(p => p.SecurityID == SecurityID);
      if (sec != null)
      {
        if (sec.Class == 2)
        {
          Qty = Num * (Price * (GetBondNominal(SecurityID, dt) ?? 0) / 100 + GetCoupon(SecurityID, dt)) * GetFundRate(sec.NominalFundID ?? 1, dt, FundID);
        }
        else if (sec.Class == 1)
        {
          Qty = Price * Num;
        }
      }
      return Qty;
    }

    public QtyNum GetModDealSetNum(int SecurityID, DateTime dt, int FundID, double Price, double Num, double Qty)
    {
      QtyNum qn = new QtyNum { Num = Num, Qty = Qty };
      if (Price > 0 && Qty > 0)
      {
        var sec = db.tSecurity.FirstOrDefault(p => p.SecurityID == SecurityID);
        if (sec != null)
        {
          if (sec.Class == 2)
          {
            var Price1 = (Price * (GetBondNominal(SecurityID, dt) ?? 0) / 100 + GetCoupon(SecurityID, dt)) * GetFundRate(sec.NominalFundID ?? 1, dt, FundID);
            qn.Num = Math.Floor(Qty / Price1);
            qn.Qty = Math.Round(Price1 * qn.Num, 2);
          }
          else if (sec.Class == 1)
          {
            qn.Num = Math.Floor((Qty + 0.01) / Price);
            qn.Qty = Price * qn.Num;
          }
        }
      }
      return qn;
    }

    public double? GetBondNominal(int SecurityID, DateTime dt)
    {
      double? Nominal = 0;
      var sec = db.tSecurity.FirstOrDefault(p => p.SecurityID == SecurityID && p.Class == 2);
      if (sec != null)
      {
        var amort = db.tAmortization.Where(p => p.SecurityID == SecurityID && p.ADate <= dt).OrderByDescending(p => p.ADate).FirstOrDefault();
        Nominal = (amort == null ? sec.Nominal : amort.Nominal);
      }
      return Nominal;
    }

    public double GetCoupon(int SecurityID, DateTime dt)
    {
      double Coupon = 0;
      var sec = db.tSecurity.FirstOrDefault(p => p.SecurityID == SecurityID && p.Class == 2);
      if (sec != null)
      {
        var coup = db.tCoupon.FirstOrDefault(p => p.SecurityID == SecurityID && p.DateStart <= dt && p.DateEnd > dt);
        if (coup != null)
        {
          if (coup.Price > 0)
          {
            Coupon = Math.Round(coup.Price * GetCouponDayCount(coup.DateStart.Value, dt, sec.CouponBasisID.Value) / (GetCouponDayCount(coup.DateStart.Value, coup.DateEnd.Value, sec.CouponBasisID.Value)) ?? 0, 5);
          }
          else
          {
            var amort = db.tAmortization.Where(p => p.SecurityID == SecurityID && p.ADate <= dt).OrderByDescending(p => p.ADate).FirstOrDefault();
            var Nominal = GetBondNominal(SecurityID, dt);
            Coupon = Math.Round(Nominal * coup.Rate * GetCouponDayCount(coup.DateStart.Value, dt, sec.CouponBasisID.Value) / 100 ?? 0, 2);
          }
        }
      }
      return Coupon;
    }

    public double GetCouponDayCount(DateTime d1, DateTime d2, int couponBaseId)
    {
      double ret = 0;
      switch (couponBaseId)
      {
        case 1: //30/360 (30/360 ISDA)
          ret = (d2.Year - d1.Year) * 360 + (d2.Month - d1.Month) * 30 + (d2.Day == 31 && d1.Day == 30 ? 30 : d2.Day) - (d1.Day == 31 ? 30 : d1.Day);
          break;
        case 3:
          ret = (d2 - d1).TotalDays;
          break;
        case 4: //30/360 German 30E/360 ISDA
          ret = (d2.Year - d1.Year) * 360 + (d2.Month - d1.Month) * 30 + (d2.Day == 31 ? 30 : d2.Month == 2 && d2.AddDays(1).Month == 3 ? 30 : d2.Day) - (d1.Day == 31 ? 30 : d1.Month == 2 && d1.AddDays(1).Month == 3 ? 30 : d1.Day);
          break;
        case 6: //Actual/Actual (ISDA)
          ret = (d2 - d1).TotalDays;
          break;
        case 7:
          ret = (d2.Year - d1.Year) * 360 + (d2.Month - d1.Month) * 30 + (d2.Day == 31 ? 30 : d2.Day) - (d1.Day == 31 ? 30 : d1.Day);
          break;
        case 10: //Actual/365L
          ret = (d2 - d1).TotalDays;
          break;
        case 13: //Actual/364
          ret = (d2 - d1).TotalDays;
          break;
        case 15: //Actual / Actual(ICMA)
          ret = (d2 - d1).TotalDays;
          break;
      }
      return ret;
    }

    public double GetFundRate(int FundID, DateTime RateDate, int QuotingFundID)
    {
      double Rate = 1;
      if (FundID == QuotingFundID) return Rate;
      if (FundID != 1)
      {
        var r = db.tRate.Where(p => p.SecurityID == FundID && p.TradeSystemID == 1 && p.RateDate <= RateDate).OrderByDescending(p => p.RateDate).FirstOrDefault();
        if (r != null)
        {
          Rate *= r.CourseCurrent ?? 1;
        }
      }
      if (QuotingFundID != 1)
      {
        var r = db.tRate.Where(p => p.SecurityID == QuotingFundID && p.TradeSystemID == 1 && p.RateDate <= RateDate).OrderByDescending(p => p.RateDate).FirstOrDefault();
        if (r != null)
        {
          Rate /= r.CourseCurrent ?? 1;
        }
      }
      return Rate;
    }

    public IEnumerable<dynamic> GetFunds()
    {
      return db.tSecurity.Where(p => p.Class == 0 && p.ISIN != null).OrderBy(p => p.Name).Select(p => new { id = p.SecurityID, name = p.Name });
    }

    public IEnumerable<dynamic> GetRests<T>(IEnumerable<T> TreatyID, DateTime dt, bool withMD, Guid UserID, bool IsGroupSec)
    {
      DataTable tid = TreatyID.ToDataTable();

      SqlParameter[] parameters = {
        new SqlParameter
        {
          SqlDbType = SqlDbType.Structured,
          Direction = ParameterDirection.Input,
          ParameterName = "t",
          TypeName = "dbo.typeID",
          Value = tid
        },
        new SqlParameter
        {
          SqlDbType = SqlDbType.SmallDateTime,
          Direction = ParameterDirection.Input,
          ParameterName = "dt",
          Value = dt
        },
        new SqlParameter
        {
          SqlDbType = SqlDbType.Bit,
          Direction = ParameterDirection.Input,
          ParameterName = "withMD",
          Value = withMD
        },
        new SqlParameter
        {
          SqlDbType = SqlDbType.UniqueIdentifier,
          Direction = ParameterDirection.Input,
          ParameterName = "UserID",
          Value = UserID
        },
        new SqlParameter
        {
          SqlDbType = SqlDbType.Bit,
          Direction = ParameterDirection.Input,
          ParameterName = "IsGroupSec",
          Value = IsGroupSec
        }
      };
      var q = db.Database.SqlQuery<upGetRest_Result>("exec upGetRests @t, @dt, @withMD, @UserID, @IsGroupSec", parameters);
      return q;
    }

    public ObjectResult<upRepCheckDecl_Result> RepCheckDecl(DateTime? dt)
    {
      dt = dt ?? DateTime.Today;
      var q = db.upRepCheckDecl(dt);
      return q;
    }
    public ObjectResult<upCheckDeclModDeal_Result> CheckDeclModDeal(DateTime dt, Guid? userID)
    {
      return db.upCheckDeclModDeal(dt, userID);
    }
    public IEnumerable<dynamic> getTreatyByPortfs(List<int> id, string sort, string dir)
    {
      id = id == null ? new List<int>() : id;
      var q = from pt in db.tPortfolioTreaty
              where id.Contains((int)pt.PortfolioID)
              select new
              {
                pt.TreatyID,
                pt.tTreaty.Name,
                pt.tTreaty.FinInstID,
                ClientName = pt.tTreaty.tFinInst.Name,
                pt.tTreaty.IsDisabled,
                pt.DateStart,
                pt.DateFinish
              };
      q = q.Distinct();
      if (sort != null) q = q.OrderBy(sort + (dir == "DESC" ? " descending" : ""));
      return q;
    }
  }

  public class InvestDeclModule : NinjectModule
  {
    /// <summary>
    /// Loads the module into the kernel.
    /// </summary>
    public override void Load()
    {
      this.Bind<IInvestDeclRepository>().To<InvestDeclRepository>().InRequestScope();
    }
  }

  public class QtyNum
  {
    public double Num { get; set; }
    public double Qty { get; set; }
  }
}