using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iProjects.Models;
using iProjects.Toast;

namespace iProjects.Controllers
{
    public class CrashReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: CrashReports
        public ActionResult Index()
        {
            this.AddToastMessage("Welcome", "Here you can see all crash reports!", ToastType.Info);
            return View(db.CrashReports.ToList());
        }

        public ActionResult List()
        {
            return View(db.CrashReports.ToList());
        }

        [ChildActionOnly]
        public ActionResult Statistics()
        {
            var statsModel = new CrashReport();
            statsModel.Id = (db.CrashReports).Count();
            return View(statsModel);
        }

        // GET: CrashReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrashReport crashReport = db.CrashReports.Find(id);
            if (crashReport == null)
            {
                return HttpNotFound();
            }
            return View(crashReport);
        }
        // GET: CrashReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CrashReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Created,Creator,ProductName,Os,CrsdhDetails,Status")] CrashReport crashReport)
        {
            if (ModelState.IsValid)
            {
                db.CrashReports.Add(crashReport);
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully Created new crash Report!", ToastType.Success);
                return RedirectToAction("Index");
            }
            this.AddToastMessage("ERROR!", "You must enter all field correctly!", ToastType.Error);
            return View(crashReport);
        }

        // GET: CrashReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrashReport crashReport = db.CrashReports.Find(id);
            if (crashReport == null)
            {
                return HttpNotFound();
            }
            return View(crashReport);
        }

        // POST: CrashReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Created,Creator,ProductName,Os,CrsdhDetails,Status")] CrashReport crashReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crashReport).State = EntityState.Modified;
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully edited crash Report!", ToastType.Success);
                return RedirectToAction("Index");
            }
            this.AddToastMessage("Warning!", "You must enter all field correctly!", ToastType.Error);
            return View(crashReport);
        }

        // GET: CrashReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrashReport crashReport = db.CrashReports.Find(id);
            if (crashReport == null)
            {
                return HttpNotFound();
            }
            this.AddToastMessage("Warning!", "You are deleting from database and if you delete it can not be undone!", ToastType.Warning);
            return View(crashReport);
        }

        // POST: CrashReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CrashReport crashReport = db.CrashReports.Find(id);
            db.CrashReports.Remove(crashReport);
            db.SaveChanges();
            this.AddToastMessage("Success.", "You have successfully deleted this entry!", ToastType.Success);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
