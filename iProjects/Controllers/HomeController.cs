using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iProjects.Toast;

namespace iProjects.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            this.AddToastMessage("Welcome", "iProject management system!", ToastType.Info);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }

        public ActionResult Backlog()
        {
            this.AddToastMessage("Welcome", "iProject management system!", ToastType.Info);
            return View();
        }
    }
}