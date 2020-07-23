using MO5.Areas.Code.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MO5.Helpers;
using System.DirectoryServices;
using MO5.Models;
using System.Net.Mail;
using System.Configuration;
using MO5.Controllers;

namespace MO5.Areas.Code.Controllers
{
  public class EnvoiController : BaseController
  {
    public IEnvoiRepository envoiRepository;

    public EnvoiController(IEnvoiRepository _envoiRepository)
    {
      envoiRepository = _envoiRepository;
    }

    public ActionResult Index()
    {
      //envoiRepository.envoyerCourriels(null, (HttpContext.Request).Url.Authority);
      return View();
    }

    [Authorize(Roles = "envoi")]
    public ActionResult getEnvoiList(int? OwnerID, int TypeID, bool? isAuto, bool? IsActive, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.getEnvoiList(OwnerID, TypeID, isAuto, IsActive, sort, dir) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult addEnvoi(List<tEnvoi> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = envoiRepository.addEnvoi(data) } };
      }
      catch (Exception /*ex*/)
      {
        return new JsonnResult { Data = new { success = false } };
      }
    }

    [Authorize(Roles = "envoi")]
    public ActionResult updEnvoi(List<tEnvoi> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.updEnvoi(data) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult delEnvoi(List<tEnvoi> data)
    {
      return new JsonnResult { Data = new { success = envoiRepository.delEnvoi(data) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult envoyerCourriel(int? id, string Comment)
    {
      if (id.HasValue)
        return new JsonnResult { Data = new { success = envoiRepository.envoyerCourriel(id.Value, Comment, (HttpContext.Request).Url.Authority) } };
      else
        return new JsonnResult { Data = new { success = false } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult envoyerCourriels()
    {
      return new JsonnResult { Data = new { success = envoiRepository.envoyerCourriels(null, (HttpContext.Request).Url.Authority) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult getConseilList(DateTime? d1, DateTime? d2, int? type, Boolean? nopen, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.getConseilList(d1, d2, type, nopen, sort, dir) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult addConseil(List<tConseil> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = envoiRepository.addConseil(data) } };
      }
      catch (Exception /*ex*/)
      {
        return new JsonnResult { Data = new { success = false } };
      }
    }

    [Authorize(Roles = "envoi")]
    public ActionResult updConseil(List<tConseil> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.updConseil(data) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult delConseil(List<tConseil> data)
    {
      return new JsonnResult { Data = new { success = envoiRepository.delConseil(data) } };
    }

    public ActionResult getCPriorite()
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.getCPriorite() } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult getEnvoiHoraire(int? id)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.getEnvoiHoraire(id) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult addEnvoiHoraire(List<tEnvoiHoraire> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.addEnvoiHoraire(data) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult updEnvoiHoraire(List<tEnvoiHoraire> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.updEnvoiHoraire(data) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult delEnvoiHoraire(List<tEnvoiHoraire> data)
    {
      return new JsonnResult { Data = new { success = envoiRepository.delEnvoiHoraire(data) } };
    }

    public ActionResult getEnvoiHoraireType()
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.getEnvoiHoraireType() } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult getEnvoiExecList(int? OwnerID, int TypeID, DateTime? d1, DateTime? d2, bool? IsExec, string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.getEnvoiExecList(OwnerID, TypeID, d1, d2, IsExec, sort, dir) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult addEnvoiExec(List<tEnvoiExec> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.addEnvoiExec(data) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult updEnvoiExec(List<tEnvoiExec> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.updEnvoiExec(data) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult delEnvoiExec(List<tEnvoiExec> data)
    {
      return new JsonnResult { Data = new { success = envoiRepository.delEnvoiExec(data) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult envoiExecCourriel(int id)
    {
      return new JsonnResult { Data = new { success = envoiRepository.envoiExecCourriel(id, id == 26187/*ИК*/? " Dmitriy.Levin@qbfin.ru,stanislav.matyukhin@qbfin.ru,vlada.bytkovskay@qbfin.ru,anastasia.koval@qbfin.ru" : id == 26188/*УК*/? "Dmitriy.Levin@qbfin.ru,stanislav.matyukhin@qbfin.ru,vlada.bytkovskay@qbfin.ru,anastasia.koval@qbfin.ru" : "", (HttpContext.Request).Url.Authority) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult envoiExecRiCourriel(int id)
    {
      return new JsonnResult { Data = new { success = envoiRepository.envoiExecRiCourriel(id, id == 26187/*ИК*/? "Dmitriy.Levin@qbfin.ru,stanislav.matyukhin@qbfin.ru,vlada.bytkovskay@qbfin.ru,anastasia.koval@qbfin.ru" : id == 26188/*УК*/? "Dmitriy.Levin@qbfin.ru,stanislav.matyukhin@qbfin.ru,vlada.bytkovskay@qbfin.ru,anastasia.koval@qbfin.ru" : "", (HttpContext.Request).Url.Authority) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult conseilCourriel(int? id)
    {
      return new JsonnResult { Data = new { success = envoiRepository.conseilCourriel(id, (HttpContext.Request).Url.Authority) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult conseilCourriels()
    {
      return new JsonnResult { Data = new { success = envoiRepository.conseilCourriels((HttpContext.Request).Url.Authority) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult conseilCourrielAll()
    {
      return new JsonnResult { Data = new { success = envoiRepository.conseilCourrielAll() } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult conseilEnbCourriel()
    {
      return new JsonnResult { Data = new { success = envoiRepository.conseilEnabledCourriel() } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult getConseilHoraire(int? id)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.getConseilHoraire(id) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult addConseilHoraire(List<tConseilHoraire> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.addConseilHoraire(data) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult updConseilHoraire(List<tConseilHoraire> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.updConseilHoraire(data) } };
    }

    [Authorize(Roles = "envoi")]
    public ActionResult delConseilHoraire(List<tConseilHoraire> data)
    {
      return new JsonnResult { Data = new { success = envoiRepository.delConseilHoraire(data) } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult getRiskMapList(string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.getRiskMapList(sort, dir) } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult addRiskMap(List<tRiskMap> data)
    {
      try
      {
        return new JsonnResult { Data = new { success = true, data = envoiRepository.addRiskMap(data) } };
      }
      catch (Exception /*ex*/)
      {
        return new JsonnResult { Data = new { success = false } };
      }
    }

    [Authorize(Roles = "risk")]
    public ActionResult updRiskMap(List<tRiskMap> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.updRiskMap(data) } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult delRiskMap(List<tRiskMap> data)
    {
      return new JsonnResult { Data = new { success = envoiRepository.delRiskMap(data) } };
    }

    public ActionResult getRMLevel()
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.getRMLevel() } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult getRiskMapHoraire(int? id)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.getRiskMapHoraire(id) } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult addRiskMapHoraire(List<tRiskMapHoraire> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.addRiskMapHoraire(data) } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult updRiskMapHoraire(List<tRiskMapHoraire> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.updRiskMapHoraire(data) } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult delRiskMapHoraire(List<tRiskMapHoraire> data)
    {
      return new JsonnResult { Data = new { success = envoiRepository.delRiskMapHoraire(data) } };
    }

    [Authorize(Roles = "risk")]
    public ActionResult riskMapCourriel(List<int> id)
    {
      var host = (HttpContext.Request).Url.Authority;
      var q = envoiRepository.riskMapCourriel(id, (HttpContext.Request).Url.Authority);
      if (q != null)
      {
        SmtpClient sc = new SmtpClient();
        foreach (var r in q)
        {
          MailMessage message = new MailMessage();
          message.To.Add(host.Contains("localhost") || r.Key == null ? "avg@smtp.ru" : r.Key.Email);
          message.From = new MailAddress(ConfigurationManager.AppSettings["EMailFrom"], "Внутренний контроль");
          //var template = new MO5.Areas.Code.Views.Envoi.riskMapCourriel { q = r, i = r.Key.i };
          ViewBag.i = r.Key.i;
          message.Body = RenderViewToString(ControllerContext, "riskMapCourriel", r);
          message.IsBodyHtml = true;
          message.Priority = MailPriority.High;
          message.Headers.Add("Importance", "High");
          message.Subject = "Операционные контроли";
          sc.Send(message);
        }
        return new JsonnResult { Data = new { success = true } };
      }
      return new JsonnResult { Data = new { success = false } };
    }

    public ActionResult EMail()
    {
      return View("EMail");
    }

    public ActionResult getEMailList(string sort, string dir)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.getEMailList(sort, dir) } };
    }

    public ActionResult addEMail(List<EMailItem> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.addEMail(data) } };
    }

    public ActionResult updEMail(List<EMailItem> data)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.updEMail(data) } };
    }

    public ActionResult delEMail(List<EMailItem> data)
    {
      return new JsonnResult { Data = new { success = envoiRepository.delEMail(data) } };
    }

    public ActionResult addToUA(int id)
    {
      return new JsonnResult { Data = new { success = envoiRepository.AddToUAKM(id, 26194) } };
    }

    public ActionResult addToKM(int id)
    {
      return new JsonnResult { Data = new { success = envoiRepository.AddToUAKM(id, 26195) } };
    }

    public ActionResult findADInfo(string email)
    {
      var directory = new DirectorySearcher("(mail=" + email + ")");
      var result = directory.FindOne();
      if (result == null)
        return new JsonnResult { Data = new { success = false } };
      if (result.Properties["sn"].Count == 0)
        return new JsonnResult { Data = new { success = false } };
      var sn = result.Properties["sn"][0].ToString();
      var givenname = result.Properties["givenname"].Count > 0 ? result.Properties["givenname"][0].ToString() : "";
      var initials = result.Properties["initials"].Count > 0 ? result.Properties["initials"][0].ToString() : "";

      return new JsonnResult { Data = new { success = true, name = sn + (givenname.Length > 0 ? " " + givenname.Left(1) + "." + (initials.Length > 0 ? initials + "." : "") : "") } };
    }

    public ActionResult GetObjClsByParent(int id)
    {
      return new JsonnResult { Data = new { success = true, data = envoiRepository.GetObjClsByParent(id) } };
    }

  }
}