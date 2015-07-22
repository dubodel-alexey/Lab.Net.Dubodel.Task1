using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using task3.Infrastructure;
using task3.Infrastructure.Authentication;
using task3.Models;

namespace task3.Controllers
{
    public class AccountController : BaseController
    {
        DataContext Context = new DataContext();
        //
        // GET: /Account/
        public ActionResult Login()
        {
            if (Request.IsAjaxRequest())
                return PartialView("_Login");
            return View();
        }

        [ValidateAjax]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = Context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password) ??
                    Context.Users.FirstOrDefault(u => u.Email == model.Username && u.Password == model.Password);

                var roles = user.Roles.Select(m => m.RoleName).ToArray();

                var serializeModel = new CustomPrincipalSerializeModel { UserId = user.Id, Roles = roles };

                string userData = JsonConvert.SerializeObject(serializeModel);
                var authTicket = new FormsAuthenticationTicket(
                1,
                user.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(15),
                model.RememberMe,
                userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);

                if (Url.IsLocalUrl(returnUrl))
                {
                    if (Request.IsAjaxRequest())
                    {
                        return Json(returnUrl);
                    }
                    return Redirect(returnUrl);
                }

                if (Request.IsAjaxRequest())
                {
                    return Json(Url.Action("Index", "Home"));
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult SignUp()
        {
            if (Request.IsAjaxRequest())
                return PartialView("_SignUp");
            return View();
        }

        [ValidateAjax]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SignUp(SignUpViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    Roles = new List<Role>()
                };
                var roleName = "User";
                user.Roles.Add(Context.Roles.FirstOrDefault(r => r.RoleName == roleName));
                Context.Users.Add(user);
                Context.SaveChanges();

                var serializeModel = new CustomPrincipalSerializeModel { UserId = user.Id, Roles = new[] { roleName } };
                string userData = JsonConvert.SerializeObject(serializeModel);
                var authTicket = new FormsAuthenticationTicket(
                1,
                user.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(15),
                false,
                userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);

                if (Url.IsLocalUrl(returnUrl))
                {
                    if (Request.IsAjaxRequest())
                    {
                        return Json(returnUrl);
                    }
                    return Redirect(returnUrl);
                }


                if (Request.IsAjaxRequest())
                {
                    return Json(Url.Action("Index", "Home"));
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
