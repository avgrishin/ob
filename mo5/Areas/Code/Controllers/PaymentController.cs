using MO5.Areas.Code.Models;
using MO5.Controllers;
using MO5.Helpers;
using MO5.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MO5.Areas.Code.Controllers
{
  public class PaymentController : BaseController
  {
    private readonly IPaymentRepository _paymentRepository;
    private readonly IConfigurationProvider _configProvider;
    public PaymentController(IPaymentRepository paymentRepository, IConfigurationProvider configProvider)
    {
      _paymentRepository = paymentRepository;
      _configProvider = configProvider;
    }
    // GET: Code/Payment
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult GetPaymentList(DateTime? d1, DateTime? d2, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = _paymentRepository.GetPaymentList(d1, d2, UserName, sort, dir) } };
    }
    public ActionResult GetBankByBIC(string BIC)
    {
      return new JsonnResult { Data = new { success = true, data = _paymentRepository.GetBankByBIC(BIC) } };
    }

    public ActionResult addPayment(List<tPayment> data)
    {
      return new JsonnResult { Data = new { success = true, data = _paymentRepository.AddPayment(data, UserName) } };
    }
    [HttpPost]
    public ActionResult updPayment(List<tPayment> data)
    {
      return new JsonnResult { Data = new { success = true, data = _paymentRepository.UpdPayment(data, UserName) } };
    }
    public ActionResult delPayment(List<tPayment> data)
    {
      return new JsonnResult { Data = new { success = _paymentRepository.DelPayment(data, UserName) } };
    }

    public ActionResult Unload(List<int> data)
    {
      var q = _paymentRepository.Unload(data);
      using var ms = new MemoryStream();
      using var sw = new StreamWriter(ms, Encoding.GetEncoding(866));
      foreach (var r in q)
      {
        sw.WriteLine(r);
      }
      sw.Flush();
      ms.Position = 0;
      return File(ms.ToArray(), "application/text", $"Export{DateTime.Now:yyMMddhhmm}.dat");
    }

    public ActionResult getPlatList()
    {
      var s = _configProvider.GetValue<string>("plat");
      Response.ContentType = "application/json";
      return Content("{ success: true, data: "+s+"}", "application/json");
    }

  }
}