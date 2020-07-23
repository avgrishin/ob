using MO5.Helpers;
using MO5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MO5.Controllers
{
  public class AdminUser
  {
    public string OUN { get; set; }
    public string UN { get; set; }
    public string Email { get; set; }
  }
  //[Authorize(Roles = "admin")]
  public class AdminController : Controller
  {
    public ActionResult Index()
    {
      string[] r = new string[] { "admin", "jrpk", "jrpki", "jrpkm", "decl", "envoi", "jur", "regdoc" };
      foreach (var r1 in r)
      {
        if (!Roles.RoleExists(r1))
          Roles.CreateRole(r1);
      }
      return View();
    }

    public ActionResult getUserList()
    {
      var q = from u in Membership.GetAllUsers().OfType<MembershipUser>()
              select new AdminUser
              {
                OUN = u.UserName,
                UN = u.UserName,
                Email = u.Email
              };
      return new JsonnResult { Data = new { success = true, data = q } };
    }

    public ActionResult addUser(List<AdminUser> data)
    {
      if (!String.IsNullOrEmpty(data[0].UN))
      {
        if (Membership.FindUsersByName(data[0].UN).Count == 0)
        {
          var user = Membership.CreateUser(data[0].UN, "1", data[0].Email);
          var q = from u in Membership.FindUsersByName(data[0].UN).OfType<MembershipUser>()
                  select new AdminUser
                  {
                    OUN = u.UserName,
                    UN = u.UserName,
                    Email = u.Email
                  };
          return new JsonnResult { Data = new { success = true, data = q } };
        }
        return new JsonnResult { Data = new { success = false, message = "Юзер уже есть" } };
      }
      return new JsonnResult { Data = new { success = false, message = "Незаполнено поле Юзер" } };
    }

    public ActionResult updUser(List<AdminUser> data)
    {
      if (data.Count > 0)
      {
        if (!String.IsNullOrEmpty(data[0].UN))
        {
          var user = Membership.GetUser(data[0].OUN);
          if (user == null)
          {
            return new JsonnResult { Data = new { success = false, message = "Юзер не найден" } };
          }
          user.Email = data[0].Email;
          Membership.UpdateUser(user);
          var q = from u in Membership.FindUsersByName(data[0].UN).OfType<MembershipUser>()
                  select new AdminUser
                  {
                    OUN = u.UserName,
                    UN = u.UserName,
                    Email = u.Email
                  };
          return new JsonnResult { Data = new { success = true, data = q } };
        }
        return new JsonnResult { Data = new { success = false, message = "Незаполнено поле Юзер" } };
      }
      return new JsonnResult { Data = new { success = false, message = "Нет данных для обновления" } };
    }

    public ActionResult delUser(List<AdminUser> data)
    {
      try
      {
        foreach (var e in data)
        {
          Membership.DeleteUser(data[0].OUN);
        }
      }
      catch (Exception ex)
      {
        return new JsonnResult { Data = new { success = false, message = ex.Message } };
      }
      return new JsonnResult { Data = new { success = true } };
    }

    public ActionResult GetUserRoles(string UserName)
    {
      var q = from r in Roles.GetAllRoles()
              join ru in Roles.GetRolesForUser(UserName) on r equals ru into ru_
              from ru in ru_.DefaultIfEmpty()
              select new
              {
                RoleName = r,
                InRole = !string.IsNullOrEmpty(ru)
              };
      return new JsonnResult { Data = new { success = true, data = q } };
    }

    public class UserRolesList
    {
      public string RoleName { get; set; }
      public bool InRole { get; set; }
    }

    public ActionResult SetUserRoles(List<UserRolesList> data, string UserName)
    {
      var user = Membership.GetUser(UserName);
      if (user == null)
      {
        return new JsonnResult { Data = new { success = false, message = "Юзер не найден" } };
      }

      foreach (var role in data)
      {
        if (role.InRole)
        {
          if (!Roles.IsUserInRole(UserName, role.RoleName))
            Roles.AddUserToRole(UserName, role.RoleName);
        }
        else
        {
          if (Roles.IsUserInRole(UserName, role.RoleName))
            Roles.RemoveUserFromRole(UserName, role.RoleName);
        }
      }
      return Json(new { success = true, message = "Сохранено", data = data });
    }

    public ActionResult getRoleList()
    {
      var q = from r in Roles.GetAllRoles() orderby r select new { OName = r, Name = r };
      return new JsonnResult { Data = new { success = true, data = q } };
    }

    public class AdminRoles
    {
      public string Name { get; set; }
    }

    public ActionResult addRole(List<AdminRoles> data)
    {
      if (!String.IsNullOrEmpty(data[0].Name))
      {
        if (!Roles.RoleExists(data[0].Name))
        {
          Roles.CreateRole(data[0].Name);
          var q = from r in Roles.GetAllRoles().Where(p => p == data[0].Name) select new { OName = r, Name = r };
          return new JsonnResult { Data = new { success = true, data = q } };
        }
        return new JsonnResult { Data = new { success = false, message = "Роль уже есть" } };
      }
      return new JsonnResult { Data = new { success = false, message = "Незаполнено поле Наименование" } };
    }

  }
}