using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iProjects.Controllers
{
    public class HeadlinesController : Controller
    {
        // GET: Headlines
        public ActionResult Index()
        {
            return View();
        }
    }
}