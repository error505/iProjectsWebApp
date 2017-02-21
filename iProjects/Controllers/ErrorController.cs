using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iProjects.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error404(string url)
        {
            Response.StatusCode = 404;
            ViewData["url"] = url;
            return View();
        }

        public ActionResult Error500(string url)
        {
            Response.StatusCode = 500;
            ViewData["url"] = url;
            return View();
        }
    }
}