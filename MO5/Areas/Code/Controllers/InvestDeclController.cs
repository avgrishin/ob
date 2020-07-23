using ClosedXML.Excel;
using MO5.Areas.Code.Models;
using MO5.Helpers;
using MO5.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Principal;
using System.Net.Mail;
using System.Configuration;

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

    public ActionResult getTreatyByPortfolioList(int PortfolioID, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetTreatyByPortfolioList(PortfolioID, sort, dir) } };
    }

    public ActionResult addPortfolioTreaty(int TreatyID, int PortfolioID, DateTime DateStart)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.AddPortfolioTreaty(TreatyID, PortfolioID, DateStart) } };
    }

    public ActionResult delPortfolioTreaty(int id)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.DelPortfolioTreaty(id) } };
    }

    public ActionResult delTreatyPortfolio(List<tPortfolioTreaty> data)
    {
      if (data != null && data.Count > 0)
        return new JsonnResult { Data = new { success = investDeclRepository.DelPortfolioTreaty(data[0].ID) } };
      else
        return new JsonnResult { Data = new { success = false } };
    }

    public ActionResult Rests()
    {
      return View();
    }

    public ActionResult RestsReport(DateTime dt)
    {
      var q = investDeclRepository.RepRestDU(dt);
      var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add("Остатки");
      worksheet.Cell(1, 1).Value = "Дата";
      worksheet.Cell(1, 2).SetValue(dt);
      var i = 2;
      worksheet.Cell(i, 1).Value = "Клиент";
      worksheet.Cell(i, 2).Value = "Договор";
      worksheet.Cell(i, 3).Value = "Актив";
      worksheet.Cell(i, 4).Value = "ISIN";
      worksheet.Cell(i, 5).Value = "Кол-во";
      worksheet.Cell(i, 6).Value = "Цена";
      worksheet.Cell(i, 7).Value = "Стоимость";
      worksheet.Cell(i, 8).Value = "НКД";
      worksheet.Cell(i, 9).Value = "SecurityID";
      worksheet.Cell(i, 10).Value = "Эмитент";
      worksheet.Cell(i, 11).Value = "ИНН эмитента";
      i++;
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
        worksheet.Cell(i, 10).Value = r.Issuer;
        worksheet.Cell(i, 11).Value = r.INN;
        i++;
      }
      worksheet.Range(2, 1, i - 1, 11).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin).Border.SetOutsideBorder(XLBorderStyleValues.Thin);
      worksheet.Columns().AdjustToContents();
      MemoryStream ms = new MemoryStream();
      workbook.SaveAs(ms);
      return File(ms.ToArray(), "application/vnd.ms-excel", string.Format("rf{0}.xlsx", DateTime.Now.Second));
    }

    public ActionResult CheckDecl(int id, DateTime? dt, bool? withMD)
    {
      var dtb = DateTime.Now;
      var q = investDeclRepository.CheckDecl(id, dt, withMD, (Guid)Membership.GetUser().ProviderUserKey);
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
      worksheet.Column(7).Width = 1.3;
      worksheet.Column(8).Width = 21;

      worksheet.Cell(1, 1).Value = "Дата";
      worksheet.Cell(2, 1).SetValue(dt).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
      if (q1 != null)
        worksheet.Cell(4, 1).SetValue(q1.Name).Style.Font.SetBold(true);
      var i = 5;
      int? FinInstID = null;
      int? InvestDeclWhereID = null;

      foreach (var r in q)
      {
        if (FinInstID != r.FinInstID)
        {
          if (InvestDeclWhereID.HasValue)
            worksheet.Range(i, 1, i, 8).Style.Border.SetTopBorder(XLBorderStyleValues.Thin).Border.SetTopBorderColor(XLColor.Gray);

          FinInstID = r.FinInstID;
          i++;
          worksheet.Cell(i, 1).SetValue(FinInstID == -1 ? "Все клиенты" : r.fiName).Style.Font.Bold = true;
          worksheet.Range(i, 1, i, 8).Style.Fill.SetBackgroundColor(XLColor.AntiqueWhite);
          i++;
          worksheet.Cell(i, 2).Value = "MIN";
          //worksheet.Range(i, 2, i, 3).Merge(false);
          worksheet.Cell(i, 4).Value = "MAX";
          //  worksheet.Range(i, 4, i, 5).Merge(true);
          worksheet.Cell(i, 6).Value = "Коэф";
          //  worksheet.Range(i, 6, i, 7).Merge(true);
          worksheet.Cell(i, 8).SetValue("Числитель \nЗнаменатель").Style.Alignment.SetWrapText(true);
          worksheet.Range(i, 2, i, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right).Font.SetBold();
          worksheet.Range(i, 1, i, 8).Style.Fill.SetBackgroundColor(XLColor.AntiFlashWhite);
          i++;
        }
        if (InvestDeclWhereID != r.InvestDeclWhereID)
        {
          InvestDeclWhereID = r.InvestDeclWhereID;

          worksheet.Cell(i, 1).SetValue(r.NameWhere).Style.Alignment.SetWrapText(true);
          worksheet.Cell(i, 2).Value = r.StartValue;
          worksheet.Cell(i, 4).Value = r.StopValue;
          worksheet.Cell(i, 6).SetValue(r.coef).Style.NumberFormat.SetNumberFormatId(4);
          worksheet.Cell(i, 3).Value = worksheet.Cell(i, 5).Value = worksheet.Cell(i, 7).Value = r.FLAG_Calculation == 1 ? "%" : "";

          //  //for (int j = 2; j <= 7; j++)
          //  //  worksheet.Range(i, j, i + 1, j).Merge(false).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

          if (r.error == 1)
          {
            worksheet.Range(i, 6, i, 7).Style.Font.SetFontColor(XLColor.Red);
          }
          worksheet.Cell(i, 8).SetValue(r.numerator).Style.NumberFormat.NumberFormatId = 4;
          i++;
          worksheet.Cell(i, 1).SetValue(r.FGName).Style.Font.SetFontSize(7).Font.SetItalic(true).Font.SetFontColor(XLColor.Gray);
          worksheet.Cell(i, 8).SetValue(r.denominator).Style.NumberFormat.NumberFormatId = 4;
          worksheet.Range(i - 1, 1, i, 1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Border.SetLeftBorderColor(XLColor.Gray);
          worksheet.Range(i - 1, 8, i, 8).Style.Border.SetRightBorder(XLBorderStyleValues.Thin).Border.SetRightBorderColor(XLColor.Gray);
          worksheet.Range(i - 1, 1, i - 1, 8).Style.Border.SetTopBorder(XLBorderStyleValues.Thin).Border.SetTopBorderColor(XLColor.Gray);

          i++;
          if (r.SecurityID.HasValue)
          {
            worksheet.Cell(i, 6).Value = "Кол-во";
            worksheet.Cell(i, 8).Value = "Стоимость";
            worksheet.Range(i, 6, i, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
            worksheet.Range(i, 1, i, 8).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin).Border.SetBottomBorderColor(XLColor.Gray);
            worksheet.Cell(i, 1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Border.SetLeftBorderColor(XLColor.Gray);
            worksheet.Cell(i, 8).Style.Border.SetRightBorder(XLBorderStyleValues.Thin).Border.SetRightBorderColor(XLColor.Gray);
            i++;
          }
        }
        if (r.SecurityID.HasValue)
        {
          var s = r.SecName;
          worksheet.Cell(i, 1).SetValue(s).Style.Font.SetFontColor(XLColor.Red).Alignment.SetIndent(1);
          worksheet.Cell(i, 6).SetValue(r.Num).Style.Font.FontColor = XLColor.Green;

          string s1 = "";
          if (r.FLAG_Calculation == 1)
          {
            s = (r.Qty ?? 0).ToString("N2");
            s1 = string.Format(" ({0:N2}{1})", r.coefS, "%");
          }
          s = (r.Qty ?? 0).ToString("N2");
          var cell = worksheet.Cell(i, 8).SetValue(s + s1);
          cell.RichText.Substring(0, s.Length).SetFontColor(XLColor.Green);
          cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

          worksheet.Cell(i, 1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin).Border.SetLeftBorderColor(XLColor.Gray);
          worksheet.Cell(i, 8).Style.Border.SetRightBorder(XLBorderStyleValues.Thin).Border.SetRightBorderColor(XLColor.Gray);
          i++;
        }
      }
      worksheet.Range(i, 1, i, 8).Style.Border.SetTopBorder(XLBorderStyleValues.Thin).Border.SetTopBorderColor(XLColor.Gray);
      //worksheet.Columns().AdjustToContents();
      MemoryStream ms = new MemoryStream();
      workbook.SaveAs(ms);

      return File(ms.ToArray(), "application/vnd.ms-excel", $"rf{dtb}_{DateTime.Now}_{(int)((DateTime.Now - dtb).TotalSeconds)}_{DateTime.Now.Second}.xlsx");
    }

    public ActionResult RepCheckDecl(DateTime? dt)
    {
      var dtb = DateTime.Now;
      var q = investDeclRepository.RepCheckDecl(dt);
      var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add("Мониторинг ИД");
      worksheet.ShowGridLines = false;
      worksheet.Style.Font.FontName = "Arial";
      worksheet.Style.Font.SetFontSize(8);

      worksheet.Cell(1, 1).Value = "Дата";
      worksheet.Cell(2, 1).SetValue(dt).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
      var i = 3;
      worksheet.Cell(i, 1).Value = "Клиент";
      worksheet.Cell(i, 2).Value = "Декларация";
      worksheet.Cell(i, 3).Value = "Условие декларации";
      worksheet.Cell(i, 4).Value = "Примечание";
      worksheet.Cell(i, 5).Value = "Минимальное значение";
      worksheet.Cell(i, 6).Value = "Максимальное значение";
      worksheet.Cell(i, 7).Value = "Фактическое значение";
      worksheet.Cell(i, 8).Value = "";
      i++;
      foreach (var r in q)
      {
        worksheet.Cell(i, 1).Value = r.clName;
        worksheet.Cell(i, 2).Value = r.idName;
        worksheet.Cell(i, 3).Value = r.NameWhere;
        worksheet.Cell(i, 4).Value = r.Comment;
        worksheet.Cell(i, 5).Value = r.StartValue;
        worksheet.Cell(i, 6).Value = r.StopValue;
        worksheet.Cell(i, 7).SetValue(r.coef).Style.NumberFormat.SetNumberFormatId(4);
        worksheet.Cell(i, 8).Value = r.IsNew;
        i++;
      }
      worksheet.Range(3, 1, i - 1, 8).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin).Border.SetOutsideBorder(XLBorderStyleValues.Thin);
      worksheet.Columns().AdjustToContents();
      MemoryStream ms = new MemoryStream();
      workbook.SaveAs(ms);
      ms.Position = 0;
      SmtpClient sc = new SmtpClient();
      MailMessage message = new MailMessage();
      message.From = new MailAddress(ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
      message.To.Add(((HttpContext.Request).Url.Authority.Contains("localhost")) ? "qbcontrol@qbfin.ru" : "vlada.bytkovskay@qbfin.ru,stanislav.matyukhin@qbfin.ru,Dmitriy.Levin@qbfin.ru,anastasia.koval@qbfin.ru");
      message.CC.Add("qbcontrol@qbfin.ru");
      message.Body = i == 4 ? "Нарушений не выявлено" : "";
      message.IsBodyHtml = true;
      message.Priority = MailPriority.High;
      message.Headers.Add("Importance", "High");
      message.IsBodyHtml = true;
      message.Subject = "Мониторинг ИД" + (i == 4 ? " Нарушений не выявлено" : "");
      message.Attachments.Add(new Attachment(ms, "md.xlsx"));
      try
      {
        sc.Send(message);
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
      return new JsonnResult { Data = new { success = true } };
      //return File(ms.ToArray(), "application/vnd.ms-excel", string.Format("md{0}.xlsx", DateTime.Now.Second));
    }
    public ActionResult ModDeal()
    {
      return View();
    }

    public ActionResult getModDeal(bool? All, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetModDeal((Guid)Membership.GetUser().ProviderUserKey, All ?? false, sort, dir) } };
    }

    public ActionResult addModDeal(List<tModDeal> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.AddModDeal(data, (Guid)Membership.GetUser().ProviderUserKey) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult updModDeal(List<tModDeal> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = investDeclRepository.UpdModDeal(data, (Guid)Membership.GetUser().ProviderUserKey) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    public ActionResult delModDeal(List<tModDeal> data)
    {
      return new JsonnResult { Data = new { success = investDeclRepository.DelModDeal(data) } };
    }

    public ActionResult getTreatyByPortfClientList(int? PortfolioID, int? FinInstID)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetTreatyByPortfClientList(PortfolioID, FinInstID) } };
    }

    public ActionResult getModDealQty(int SecurityID, double Num, DateTime DealDate, double Price, int FundID)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetModDealQty(SecurityID, DealDate, FundID, Price, Num) } };
    }

    public ActionResult getModDealSetNum(int SecurityID, double Qty, DateTime DealDate, double Price, double Num, int FundID)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetModDealSetNum(SecurityID, DealDate, FundID, Price, Num, Qty) } };
    }

    public ActionResult getFundList()
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetFunds() } };
    }

    public ActionResult Alloc()
    {
      return View();
    }

    public ActionResult getRests(List<typeID> id, Boolean? IsGroupSec)
    {
      //new typeID[] { new typeID { ID = 27943 }, new typeID { ID = 27942 } }
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.GetRests(id, DateTime.Today.AddDays(DateTime.Today.DayOfWeek == DayOfWeek.Monday ? -3 : -1), false, (Guid)Membership.GetUser().ProviderUserKey, IsGroupSec ?? true) } };
    }

    public ActionResult getTreatyByPortfs(List<int> id, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = investDeclRepository.getTreatyByPortfs(id, sort, dir) } };
    }

    public ActionResult CheckDeclModDeal(DateTime dt)
    {
      var q = investDeclRepository.CheckDeclModDeal(dt, (Guid)Membership.GetUser().ProviderUserKey);
      return new JsonnResult { Data = new { success = true, data = q } };
    }
  }
}