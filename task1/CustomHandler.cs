using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;

namespace task1
{
    public class CustomHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                using (TextReader tr = File.OpenText(context.Server.MapPath(context.Request.FilePath)))
                {
                    string result = tr.ReadToEnd();
                    context.Response.ContentType = "application/json";
                    context.Response.Write(result);
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("Error");
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}