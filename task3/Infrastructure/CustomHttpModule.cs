using System;
using System.Web;

namespace task3.Infrastructure
{
    public class CustomHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += Application_BeginRequest;
            context.EndRequest += Application_EndRequest;
        }

        public void Dispose() { }

        private void Application_BeginRequest(Object source,
        EventArgs e)
        {
            var application = (HttpApplication)source;
            HttpContext context = application.Context;
            string filePath = context.Request.FilePath;
            string fileExtension = VirtualPathUtility.GetExtension(filePath);
            //if (fileExtension.Equals(".aspx"))
            {
                context.Response.Write("<div>" + DateTime.Now + @"</div>");
            }
        }

        private void Application_EndRequest(Object source,
        EventArgs e)
        {
            var application = (HttpApplication)source;
            HttpContext context = application.Context;
            string filePath = context.Request.FilePath;
            string fileExtension = VirtualPathUtility.GetExtension(filePath);
            //if (fileExtension.Equals(".aspx"))
            {
                context.Response.Write(@"<div>EndRequest</div>");
            }
        }
    }
}