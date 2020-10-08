using ClosedXML.Excel;
using MO5.Areas.Code.Models;
using MO5.Helpers;
using MO5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MO5.Areas.Code.Controllers
{
  public class RiskController : Controller
  {
    private readonly IRiskRepository _riskRepository;
    private readonly string _prefix = @"c:\data\Risk";
    public RiskController(IRiskRepository riskRepository)
    {
      _riskRepository = riskRepository;
    }

    public ActionResult Index()
    {
      return HttpNotFound();
    }

    [Authorize(Roles = "risk")]
    public ActionResult LimitList()
    {
      return View();
    }

    [Authorize(Roles = "risk")]
    public ActionResult getLimitList(int TypeID)
    {
      return new JsonnResult { Data = new { success = true, data = _riskRepository.GetLimitList(TypeID) } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult addLimitList(List<tLimitList> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = _riskRepository.AddLimitList(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    [Authorize(Roles = "risk")]
    public ActionResult updLimitList(List<tLimitList> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = _riskRepository.UpdLimitList(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    [Authorize(Roles = "risk")]
    public ActionResult delLimitList(List<tLimitList> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = _riskRepository.DelLimitList(data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    [Authorize(Roles = "risk")]
    public ActionResult getEmitentList(string filter, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = _riskRepository.GetEmitentList(filter, sort, dir) } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult getGrEmitList(string filter, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = _riskRepository.GetFinInstGroupList(10, filter, sort, dir) } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult addGrEmit(List<tFinInstGroup> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = _riskRepository.AddFinInstGroup(10, data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    [Authorize(Roles = "risk")]
    public ActionResult updGrEmit(List<tFinInstGroup> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = _riskRepository.UpdFinInstGroup(10, data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    [Authorize(Roles = "risk")]
    public ActionResult delGrEmit(List<tFinInstGroup> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = _riskRepository.DelFinInstGroup(10, data) } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    [Authorize(Roles = "risk")]
    public ActionResult addEmitGroupLink(int EmitentID, int GroupID)
    {
      return new JsonnResult { Data = new { success = _riskRepository.AddFinInstFinInstGroup(EmitentID, GroupID, 10) } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult GepLimit1(int TypeID, DateTime? dt)
    {
      return new JsonnResult { Data = new { success = _riskRepository.RepLimit1(TypeID, dt) } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult RepLimit1(int TypeID, DateTime? dt)
    {
      var dtb = DateTime.Now;
      var q = _riskRepository.RepLimit1(TypeID, dt);
      var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add("Контроль лимитов");
      var i = 1;
      worksheet.Cell(i, 1).SetValue(dt);
      i++;
      worksheet.Cell(i, 1).Value = "№";
      worksheet.Cell(i, 2).Value = "Наименование";
      worksheet.Cell(i, 3).Value = "Лимит ДУ";
      worksheet.Cell(i, 4).Value = "Утилизация лимита";
      worksheet.Cell(i, 5).Value = "% использования";
      worksheet.Cell(i, 6).Value = "Нарушение";
      worksheet.Range(i, 1, i, 6).Style.Font.SetBold(true);
      i++;
      foreach (var r in q)
      {
        worksheet.Cell(i, 1).Value = i - 1;
        worksheet.Cell(i, 2).Value = r.Name;
        worksheet.Cell(i, 3).Value = r.Value2;
        worksheet.Cell(i, 4).Value = r.Qty;
        worksheet.Cell(i, 5).Value = 100 * r.Qty / (r.Value2 == 0 ? null : r.Value2);
        worksheet.Cell(i, 6).Value = r.Qty > r.Value2 ? "Да" : "Нет";
        //worksheet.Cell(i, 5).FormulaR1C1 = "=100*rc[-1]/rc[-2]";
        i++;
      }
      worksheet.Range(1, 1, i - 1, 6).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin).Border.SetOutsideBorder(XLBorderStyleValues.Thin);
      worksheet.Range(2, 3, i - 1, 5).Style.NumberFormat.NumberFormatId = 4;
      worksheet.Columns().AdjustToContents();
      MemoryStream ms = new MemoryStream();
      workbook.SaveAs(ms);
      return File(ms.ToArray(), "application/vnd.ms-excel", string.Format("ll{0}.xlsx", DateTime.Now.Second));
    }

    [Authorize(Roles = "risk")]
    public ActionResult exportLimitList(int TypeID)
    {
      var q = _riskRepository.GetLimitList(TypeID);
      var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add(TypeID == 1 ? "ДУ" : "СС");
      var i = 1;
      worksheet.Cell(i, 1).Value = "Эмитент";
      worksheet.Cell(i, 2).Value = "ИНН";
      worksheet.Cell(i, 3).Value = "Общий лимит";
      worksheet.Cell(i, 4).Value = "Лимит на контрагента";
      worksheet.Cell(i, 5).Value = "Лимит на долговые инструменты";
      worksheet.Cell(i, 6).Value = "Лимит на репо прямое";
      worksheet.Cell(i, 7).Value = "Лимит на репо обратное";
      i++;
      var FinGroup = "-";
      var p = 0;
      foreach (var r in q)
      {
        if (r.FinGroup != FinGroup)
        {
          if (FinGroup != "-")
          {
            worksheet.Cell(i, 1).Value = "Итого по группе";
            worksheet.Cell(i, 3).FormulaR1C1 = $"=SUM(r{p}c:r{i - 1}c)";
            worksheet.Cell(i, 4).FormulaR1C1 = $"=SUM(r{p}c:r{i - 1}c)";
            worksheet.Cell(i, 5).FormulaR1C1 = $"=SUM(r{p}c:r{i - 1}c)";
            worksheet.Cell(i, 6).FormulaR1C1 = $"=SUM(r{p}c:r{i - 1}c)";
            worksheet.Cell(i, 7).FormulaR1C1 = $"=SUM(r{p}c:r{i - 1}c)";
            worksheet.Range(i, 1, i, 7).Style.Font.SetBold(true);
            i++;
          }
          FinGroup = r.FinGroup;
          worksheet.Cell(i, 1).Value = FinGroup;
          worksheet.Range(i, 1, i, 7).Merge().Style.Font.SetBold(true);
          i++;
          p = i;
        }
        worksheet.Cell(i, 1).Value = r.FinName;
        worksheet.Cell(i, 2).Value = r.INN;
        worksheet.Cell(i, 3).FormulaR1C1 = "=rc[1]+rc[2]+rc[3]+rc[4]";
        worksheet.Cell(i, 4).Value = r.Value1;
        worksheet.Cell(i, 5).Value = r.Value2;
        worksheet.Cell(i, 6).Value = r.Value3;
        worksheet.Cell(i, 7).Value = r.Value4;
        i++;
      }
      if (FinGroup != "-")
      {
        worksheet.Cell(i, 1).Value = "Итого по группе";
        worksheet.Cell(i, 3).FormulaR1C1 = $"=SUM(r{p}c:r{i - 1}c)";
        worksheet.Cell(i, 4).FormulaR1C1 = $"=SUM(r{p}c:r{i - 1}c)";
        worksheet.Cell(i, 5).FormulaR1C1 = $"=SUM(r{p}c:r{i - 1}c)";
        worksheet.Cell(i, 6).FormulaR1C1 = $"=SUM(r{p}c:r{i - 1}c)";
        worksheet.Cell(i, 7).FormulaR1C1 = $"=SUM(r{p}c:r{i - 1}c)";
        worksheet.Range(i, 1, i, 7).Style.Font.SetBold(true);
        i++;
      }

      worksheet.Range(1, 1, i - 1, 7).Style.Border.SetInsideBorder(XLBorderStyleValues.Thin).Border.SetOutsideBorder(XLBorderStyleValues.Thin);
      worksheet.Range(2, 3, i - 1, 7).Style.NumberFormat.NumberFormatId = 4;
      worksheet.Columns().AdjustToContents();
      MemoryStream ms = new MemoryStream();
      workbook.SaveAs(ms);
      return File(ms.ToArray(), "application/vnd.ms-excel", string.Format("ll{0}.xlsx", DateTime.Now.Second));
    }

    public ActionResult SendRiskLimits()
    {
      var dt = _riskRepository.GetPrevWorkDate(DateTime.Today, 2);
      var wc = new WebClient { Credentials = CredentialCache.DefaultCredentials };
      var path = Url.Action("RepLimit1", "Risk", new { TypeID = 1, dt }, protocol: Request.Url.Scheme);
      var b = wc.DownloadData(path);
      MemoryStream ms = new MemoryStream();
      ms.Write(b, 0, b.Length);
      ms.Position = 0;
      SmtpClient sc = new SmtpClient();
      MailMessage message = new MailMessage();
      message.From = new MailAddress(ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
      message.To.Add(((HttpContext.Request).Url.Authority.Contains("localhost")) ? "qbcontrol@qbfin.ru" : "georgiy.dandov@qbfin.ru,vlada.bytkovskay@qbfam.ru,stanislav.matyukhin@qbfin.ru,Dmitriy.Levin@qbfin.ru,anastasia.koval@qbfin.ru");
      message.CC.Add("qbcontrol@qbfin.ru");
      message.Body = "";
      message.IsBodyHtml = true;
      message.Priority = MailPriority.High;
      message.Headers.Add("Importance", "High");
      message.IsBodyHtml = true;
      message.Subject = "Контроль лимитов";
      message.Attachments.Add(new Attachment(ms, $"l" + $"{dt:dd-MM-yyyy}.xlsx"));
      try
      {
        sc.Send(message);
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
      return new JsonnResult { Data = new { success = true } };
    }

    public ActionResult market()
    {
      return View();
    }

    public class f
    {
      public string Name { get; set; }
    }
    public ActionResult getFileList()
    {
      var d = Directory.GetFiles(_prefix, "*.xlsx", SearchOption.TopDirectoryOnly).Select(p => new f { Name = Path.GetFileName(p) });
      return new JsonnResult { Data = new { data = d } };
    }
    [HttpPost]
    public ActionResult FileUpload(HttpPostedFileBase fn)
    {
      if (fn != null && fn.ContentLength > 0)
      {
        var path = Path.Combine(_prefix, fn.FileName);
        if (System.IO.File.Exists(path))
          System.IO.File.Delete(path);
        fn.SaveAs(path);
        return new JsonnResult { Data = new { success = true, file = fn.FileName }, ContentType = "text/html" };
      }
      return new JsonnResult { Data = new { success = false, message = "Нет файла" }, ContentType = "text/html" };
    }

    public ActionResult delFile(List<f> data)
    {
      try
      {
        foreach(var d in data)
        {
          System.IO.File.Delete(Path.Combine(_prefix, d.Name));
        }
        return new JsonnResult { Data = new { success = true } };
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
    }

    [ActionName("excel")]
    public ActionResult GetExcelFileStore(string FileName)
    {
      var workbook = new XLWorkbook(Path.Combine(_prefix, FileName));
      var ws = workbook.Worksheets.FirstOrDefault();
      var h = new List<string>();
      var lastColumn = ws.Row(1).LastCellUsed().Address.ColumnNumber;
      var lastRow = ws.Column(1).LastCellUsed().Address.RowNumber;
      for (var i = 1; i <= lastColumn; i++)
        h.Add(ws.Cell(1, i).GetString());
      var d = new List<string[]>();
      var row = new List<string>(lastColumn);
      for (var r = 2; r <= lastRow; r++)
      {
        for (var c = 1; c <= lastColumn; c++)
        {
          row.Add(ws.Cell(r, c).GetFormattedString());
        }
        d.Add(row.ToArray());
        row.Clear();
      }
      return new JsonnResult { Data = new { success = true, header = h.ToArray(), body = d.ToArray() } };
    }
  }
}