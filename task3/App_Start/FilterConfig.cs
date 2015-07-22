using System.Web;
using System.Web.Mvc;
using task3.Infrastructure;

namespace task3
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
        }
    }
}