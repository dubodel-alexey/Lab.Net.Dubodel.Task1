using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Mvc;
using task3.Infrastructure;
using task3.Infrastructure.Authentication;
using task3.Models;

namespace task3.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        private static List<Comment> comments = new List<Comment>();
        public async Task<ActionResult> Index(int page = 1)
        {
            Customers customers;
            var pageSize = 10;

            if (HttpContext.Cache["customers"] == null)
            {
                string fileData;
                using (TextReader tr = System.IO.File.OpenText(Server.MapPath("~/App_Data/data.json")))
                {
                    fileData = await tr.ReadToEndAsync();
                }
                customers = System.Web.Helpers.Json.Decode<Customers>(fileData);
                HttpContext.Cache.Insert("customers", customers, new CacheDependency(
                    Server.MapPath("~/App_Data/data.json")));
            }
            else
            {
                customers = (Customers)HttpContext.Cache["customers"];
            }

            var pagesCount = customers.items.Count / pageSize;
            pagesCount += customers.items.Count % pageSize > 0 ? 1 : 0;

            if (pagesCount < page || page < 1)
            {
                throw new Exception(); //make custom
            }

            var customersViewModel = new CustomersViewModel
            {
                CurrentPage = page,
                PagesCount = pagesCount,
                Items = customers.items.Skip(10 * (page - 1)).Take(10).ToList(),
                CommentList = comments.Select(x => new CommentViewModel { Body = x.Body, Id = x.Id }).ToList()
            };

            if (Request.IsAjaxRequest())
            {
                return Json(customersViewModel);
            }
            return View(customersViewModel);
        }

        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateAjax]
        public ActionResult CreateComment(CommentViewModel commentView)
        {
            if (ModelState.IsValid)
            {
                var lastComment = comments.LastOrDefault();
                int newId = lastComment == null ? 1 : lastComment.Id + 1;
                comments.Add(new Comment { Id = newId, Body = commentView.Body });

                if (Request.IsAjaxRequest())
                {
                    return Json(comments);
                }

                if (Request.UrlReferrer != null && Url.IsLocalUrl(Request.UrlReferrer.PathAndQuery))
                    return Redirect(Request.UrlReferrer.PathAndQuery);
                return RedirectToAction("Index");
            }
            return View("Index", new CustomersViewModel { CommentView = commentView }); // add another fields in vm
        }
    }
}
