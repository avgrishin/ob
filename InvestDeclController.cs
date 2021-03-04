using MO5.Areas.Code.Models;
using MO5.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MO5.Models;
using ClosedXML.Excel;
using System.IO;

namespace MO5.Areas.Code.Controllers
{
  [Authorize(Roles = "decl")]
  public class InvestDeclController : Controller
  {
    public IInvestDeclRepository investDeclRepository;

    public InvestDeclController(IInvestDeclRepository _investDeclRepository)
    {
      investDeclRepository = _investDeclRepository;
    }

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult InvestDeclList(string sort, string dir, bool? enb, int? type)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetInvestDeclList(enb, type, sort, dir) } };
    }

    public ActionResult InvestDeclTypeList()
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetInvestDeclTypeList() } };
    }

    public ActionResult getInvestDeclGroupType()
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetInvestDeclGroupType() } };
    }

    public ActionResult addInvestDecl(List<tInvestDecl> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.AddInvestDecl(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult updInvestDecl(List<tInvestDecl> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.UpdInvestDecl(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult delInvestDecl(List<tInvestDecl> data)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.DelInvestDecl(data) } };
    }

    public ActionResult InvestDeclLinkList(int? InvDeclID, bool? enb, string sort, string dir)
    {
      if (InvDeclID.HasValue)
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetInvestDeclLinkList(InvDeclID.Value, enb, sort, dir) } };
      }
      return new JsonnResult { Data = new { success = false } };
    }

    public ActionResult addInvestDeclLink(List<tInvestDeclLink> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.AddInvestDeclLink(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult updInvestDeclLink(List<tInvestDeclLink> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.UpdInvestDeclLink(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult delInvestDeclLink(List<tInvestDeclLink> data)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.DelInvestDeclLink(data) } };
    }

    public ActionResult InvestDeclWhereList(int? InvDeclID, bool? enb, string sort, string dir)
    {
      if (InvDeclID.HasValue)
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetInvestDeclWhereList(InvDeclID.Value, enb, sort, dir) } };
      }
      return new JsonnResult { Data = new { success = false } };
    }

    public ActionResult addInvestDeclWhere(List<tInvestDeclWhere> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.AddInvestDeclWhere(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult updInvestDeclWhere(List<tInvestDeclWhere> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.UpdInvestDeclWhere(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult delInvestDeclWhere(List<tInvestDeclWhere> data)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.DelInvestDeclWhere(data) } };
    }

    public ActionResult InvestDeclSecList(int? InvDeclWhereID, int? div, string sort, string dir)
    {
      if (InvDeclWhereID.HasValue)
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetInvestDeclSecList(InvDeclWhereID.Value, div, sort, dir) } };
      }
      return new JsonnResult { Data = new { success = false } };
    }

    public ActionResult addInvestDeclSec(List<tInvestDeclSec> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.AddInvestDeclSec(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult updInvestDeclSec(List<tInvestDeclSec> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.UpdInvestDeclSec(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult delInvestDeclSec(List<tInvestDeclSec> data)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.DelInvestDeclSec(data) } };
    }

    public ActionResult getSecSecGrpList(int? SecGrpID, string filter, string sort, string dir)
    {
      if (SecGrpID.HasValue)
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetSecSecGrp(SecGrpID.Value, filter, sort, dir) } };
      }
      return new JsonnResult { Data = new { success = false } };
    }

    public ActionResult addSecSecGrp(List<tSecuritySecurityGroup> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.AddSecSecGrp(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult updSecSecGrp(List<tSecuritySecurityGroup> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.UpdSecSecGrp(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult delSecSecGrp(List<tSecuritySecurityGroup> data)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.DelSecSecGrp(data) } };
    }

    public ActionResult getSec(string filter, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetSecurity(filter, sort, dir) } };
    }

    public ActionResult SecGroup()
    {
      return View();
    }

    public ActionResult getSecGroup(string filter, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetSecurityGroup(filter, sort, dir) } };
    }

    public ActionResult addSecGroup(List<tSecurityGroup> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.AddSecurityGroup(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult updSecGroup(List<tSecurityGroup> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.UpdSecurityGroup(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult delSecGroup(List<tSecurityGroup> data)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.DelSecurityGroup(data) } };
    }

    public ActionResult getEmitentList(string filter, string sort, string dir)
    {
      return new JsonnResult
      {
        Data = new
        {
          success = true,
          data = investDeclRepository.GetEmitentList(filter, sort, dir)
        }
      };
    }

    public ActionResult getClientList(string filter, bool? all, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetClientList(filter, all, sort, dir) } };
    }

    public ActionResult addFinInst(List<tFinInst> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.AddFinInst(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult updFinInst(List<tFinInst> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.UpdFinInst(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult delFinInst(List<tFinInst> data)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.DelFinInst(data) } };
    }

    public ActionResult getTreatyList(string filter, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetTreatyList(filter, sort, dir) } };
    }

    public ActionResult addTreaty(List<tTreaty> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.AddTreaty(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult updTreaty(List<tTreaty> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.UpdTreaty(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult delTreaty(List<tTreaty> data)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.DelTreaty(data) } };
    }

    public ActionResult getCouponList(int id, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetCouponList(id, sort, dir) } };
    }

    public ActionResult getAmortList(int id, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetAmortList(id, sort, dir) } };
    }

    public ActionResult Treaty()
    {
      return View();
    }

    public ActionResult Portfolio()
    {
      return View();
    }

    public ActionResult getPortfolioList(string filter, int? TypeID, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetPortfolioList(filter, TypeID, sort ?? "Name", dir) } };
    }

    public ActionResult addPortfolio(List<tPortfolio> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.AddPortfolio(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult updPortfolio(List<tPortfolio> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.UpdPortfolio(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult delPortfolio(List<tPortfolio> data)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.DelPortfolio(data) } };
    }

    public ActionResult getPortfolioTypeList()
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetPortfolioTypeList() } };
    }

    public ActionResult getPortfolioTreatyList(int TreatyID, int PortfolioTypeID)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetPortfolioTreatyList(TreatyID, PortfolioTypeID) } };
    }

    public ActionResult addPortfolioTreaty(int TreatyID, int PortfolioID, DateTime DateStart)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.AddPortfolioTreaty(TreatyID, PortfolioID, DateStart) } };
    }

    public ActionResult delPortfolioTreaty(int id)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.DelPortfolioTreaty(id) } };
    }

    public ActionResult Report()
    {
      var q = investDeclRepository.RepRestDU(new DateTime(2018, 7, 4));
      var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add("Остатки");
      worksheet.Cell(1, 1).Value = "Период";
      var i = 3;
      foreach (var r in q)
      {
        worksheet.Cell(i, 1).Value = r.FinInstName;
        worksheet.Cell(i, 2).Value = r.TreatyName;
        worksheet.Cell(i, 3).Value = r.SecName;
        worksheet.Cell(i, 4).Value = r.ISIN;
        worksheet.Cell(i, 5).Value = r.Num;
        worksheet.Cell(i, 6).Value = r.Course;
        worksheet.Cell(i, 7).Value = r.Qty;
        worksheet.Cell(i, 8).Value = r.Coupon;
        worksheet.Cell(i, 9).Value = r.SecurityID;
        i++;
      }
      worksheet.Columns().AdjustToContents();
      MemoryStream ms = new MemoryStream();
      workbook.SaveAs(ms);
      return File(ms.ToArray(), "application/vnd.ms-excel", string.Format("rf{0}.xlsx", DateTime.Now.Second));
    }

    public ActionResult CheckDecl(int id, DateTime? dt)
    {
      var q = investDeclRepository.CheckDecl(id, dt);
      var q1 = investDeclRepository.GetInvDecl(id);
      var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add("Проверка");
      worksheet.ShowGridLines = false;
      worksheet.Style.Font.FontName = "Arial";
      worksheet.Style.Font.SetFontSize(8);
      worksheet.Column(1).Width = 74;
      worksheet.Column(2).Width = 11.5;
      worksheet.Column(3).Width = 1.3;
      worksheet.Column(4).Width = 11.5;
      worksheet.Column(5).Width = 1.3;
      worksheet.Column(6).Width = 21;
      worksheet.Column(7).Width = 21;

      worksheet.Cell(1, 1).Value = "Дата";
      worksheet.Cell(2, 1).SetValue(dt).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
      if (q1 != null)
        worksheet.Cell(4, 1).SetValue(q1.Name).Style.Font.SetBold(true);
      var i = 5;
      var fiName = "";
      int? InvestDeclWhereID = null;

      foreach (var r in q)
      {
        if (fiName != r.fiName)
        {
          fiName = r.fiName;
          i++;
          worksheet.Cell(i, 1).Value = r.fiName;
          var range = worksheet.Range(i, 1, i, 7);
          range.Style.Font.Bold = true;
          range.Merge(false);
          i += 2;
          worksheet.Cell(i, 2).Value = "MIN";
          worksheet.Range(i, 2, i, 3).Merge(true);
          worksheet.Cell(i, 4).Value = "MAX";
          worksheet.Range(i, 4, i, 5).Merge(true);
          worksheet.Cell(i, 6).Value = "Коэф";
          worksheet.Cell(i, 7).Value = "Числитель \nЗнаменатель";
          worksheet.Cell(i, 7).Style.Alignment.WrapText = true;
          worksheet.Range(i, 2, i, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right).Font.SetBold();
          i++;
        }
        if (InvestDeclWhereID != r.InvestDeclWhereID)
        {
          InvestDeclWhereID = r.InvestDeclWhereID;

          worksheet.Cell(i, 1).Value = r.NameWhere;
          worksheet.Cell(i, 1).Style.Alignment.WrapText = true;

          worksheet.Cell(i, 2).Value = r.StartValue;
          var range = worksheet.Range(i, 2, i + 1, 2);
          range.Merge(false);
          range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

          worksheet.Cell(i, 3).Value = r.FLAG_Calculation == 1 ? "%" : "";
          range = worksheet.Range(i, 3, i + 1, 3);
          range.Merge(false);
          range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

          worksheet.Cell(i, 4).Value = r.StopValue;
          range = worksheet.Range(i, 4, i + 1, 4);
          range.Merge(false);
          range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

          worksheet.Cell(i, 5).Value = r.FLAG_Calculation == 1 ? "%" : "";
          range = worksheet.Range(i, 5, i + 1, 5);
          range.Merge(false);
          range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

          worksheet.Cell(i, 6).Value = r.coef;
          range = worksheet.Range(i, 6, i + 1, 6);
          range.Style.NumberFormat.NumberFormatId = 4;
          range.Merge(false);
          range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

          if (r.error == 1)
          {
            range.Style.Font.FontColor = XLColor.Red;
          }
          worksheet.Cell(i, 7).SetValue(r.numerator).Style.NumberFormat.NumberFormatId = 4;
          i++;
          worksheet.Cell(i, 1).Value = r.FGName;
          var cell = worksheet.Cell(i, 1);
          cell.Style.Font.SetFontSize(7).Font.SetItalic(true).Font.SetFontColor(XLColor.Gray);

          worksheet.Cell(i, 7).SetValue(r.denominator).Style.NumberFormat.NumberFormatId = 4;
          SetBorderOut(worksheet, i - 1, 1, i, 7);

          i++;
          if (r.SecurityID.HasValue)
          {
            worksheet.Cell(i, 6).Value = "Кол-во";
            worksheet.Cell(i, 7).Value = "Стоимость";
            worksheet.Range(i, 6, i, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            worksheet.Cell(i, 1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Border.SetLeftBorderColor(XLColor.Gray);
            worksheet.Cell(i, 7).Style.Border.SetRightBorder(XLBorderStyleValues.Thin).Border.SetRightBorderColor(XLColor.Gray);
            i++;
          }
        }
        if (r.SecurityID.HasValue)
        {
          var s = r.SecName;
          worksheet.Cell(i, 1).SetValue(s).Style.Font.SetFontColor(XLColor.Red).Alignment.SetIndent(2);

          worksheet.Cell(i, 6).SetValue(r.Num).Style.Font.FontColor = XLColor.Green;

          var p = r.FLAG_Calculation == 1 ? "%" : "";
          s = (r.Qty ?? 0).ToString("N2");
          var s1 = string.Format(" ({0:N2}{1})", r.coefS, p);
          var cell = worksheet.Cell(i, 7).SetValue(s + s1);
          cell.RichText.Substring(0, s.Length).SetFontColor(XLColor.Green);
          cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

          worksheet.Cell(i, 1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Border.SetLeftBorderColor(XLColor.Gray);
          worksheet.Cell(i, 7).Style.Border.SetRightBorder(XLBorderStyleValues.Thin).Border.SetRightBorderColor(XLColor.Gray);
          i++;
        }
      }
      worksheet.Range(i, 1, i, 7).Style.Border.SetTopBorder(XLBorderStyleValues.Thin).Border.SetTopBorderColor(XLColor.Gray);
      //worksheet.Columns().AdjustToContents();
      MemoryStream ms = new MemoryStream();
      workbook.SaveAs(ms);
      return File(ms.ToArray(), "application/vnd.ms-excel", string.Format("rf{0}.xlsx", DateTime.Now.Second));
    }

    private void SetBorderOut(IXLWorksheet worksheet, int firstCellRow, int firstCellColumn, int lastCellRow, int lastCellColumn)
    {
      var range = worksheet.Range(firstCellRow, firstCellColumn, lastCellRow, lastCellColumn);
      range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
      range.Style.Border.OutsideBorderColor = XLColor.Gray;

      //range = worksheet.Range(firstCellRow, lastCellColumn, lastCellRow, lastCellColumn);
      //range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
      //range.Style.Border.RightBorderColor = XLColor.Gray;

      range = worksheet.Range(firstCellRow, firstCellColumn, firstCellRow, lastCellColumn);
      range.Style.Border.TopBorder = XLBorderStyleValues.Double;
      range.Style.Border.TopBorderColor = XLColor.Gray;

      //range = worksheet.Range(lastCellRow, firstCellColumn, lastCellRow, lastCellColumn);
      //range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
      //range.Style.Border.BottomBorderColor = XLColor.Gray;
    }
  }
}