using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task3.Controllers
{
    public class UsersController : BaseController
    {
        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View();
        }

    }
}
