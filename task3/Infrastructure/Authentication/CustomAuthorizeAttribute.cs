using System;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace task3.Infrastructure.Authentication
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string UsersConfigKey { get; set; }
        public string RolesConfigKey { get; set; }

        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                var authorizedUsers = ConfigurationManager.AppSettings[UsersConfigKey];
                var authorizedRoles = ConfigurationManager.AppSettings[RolesConfigKey];

                Users = String.IsNullOrEmpty(Users) ? authorizedUsers : Users;
                Roles = String.IsNullOrEmpty(Roles) ? authorizedRoles : Roles;

                if (!String.IsNullOrEmpty(Roles))
                {
                    if (!CurrentUser.IsInRole(Roles))
                    {
                        if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                        {
                            filterContext.Result = new JsonResult
                            {
                                Data = new { Message = "You have no enough permissions to request this resource." },
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                        }
                        else
                        {
                            base.OnAuthorization(filterContext);
                        }
                    }
                }

                if (!String.IsNullOrEmpty(Users))
                {
                    if (!Users.Contains(CurrentUser.UserId.ToString()))
                    {
                        base.OnAuthorization(filterContext);
                    }
                }
            }
            else
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = new { Message = "Unauthorized" },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                    
                }
                else
                {
                    base.OnAuthorization(filterContext);
                }
            }
        }
    }
}