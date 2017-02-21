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
    public class BugReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: BugReports
        public ActionResult Index()
        {
            var bugReports = db.BugReports.Include(b => b.BugType);
            this.AddToastMessage("Welcome", "Here you can see all bug reports!", ToastType.Info);
            return View(bugReports.ToList());
        }

        public ActionResult List()
        {
            var bugReports = db.BugReports.Include(b => b.BugType);
            return View(bugReports.ToList());
        }

        public ActionResult DetProj()
        {
            var bugReports = db.BugReports.Include(b => b.BugType);
            return View(bugReports.ToList());
        }

        public ActionResult ListSprint()
        {
            var bugReports = db.BugReports.Include(b => b.BugType);
            return View(bugReports.ToList());
        }

        public ActionResult Backlog()
        {
            var bugReports = db.BugReports.Include(b => b.BugType);
            return View(bugReports.ToList());
        }

        [ChildActionOnly]
        public ActionResult Statistics()
        {
            var statsModel = new BugReport();
            statsModel.Id = (db.BugReports).Count();
            return View(statsModel);
        }
        // GET: BugReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugReport bugReport = db.BugReports.Find(id);
            if (bugReport == null)
            {
                return HttpNotFound();
            }
            return View(bugReport);
        }
        // GET: BugReports/Create
        public ActionResult Create()
        {
            ViewBag.ProjectsId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.ReleaseId = new SelectList(db.Releases, "Id", "Version");
            ViewBag.SprintsId = new SelectList(db.Sprints, "Id", "Name");
            ViewBag.BugTypeId = new SelectList(db.BugTypes, "Id", "TypeOfBug");
            return View();
        }

        // POST: BugReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,BugTypeId,Description,Creator,ProductName,BugDetails,Status,StatusId,ReleaseId,SprintsId,UserId,Backlog,ProjectsId")] BugReport bugReport)
        {
            if (ModelState.IsValid)
            {
                db.BugReports.Add(bugReport);
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully Created new Bug Report!", ToastType.Success);
                return RedirectToAction("Index");
            }
            ViewBag.ProjectsId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status", bugReport.StatusId);
            ViewBag.ReleaseId = new SelectList(db.Releases, "Id", "Version", bugReport.ReleaseId);
            ViewBag.SprintsId = new SelectList(db.Sprints, "Id", "Name", bugReport.SprintsId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", bugReport.UserId);
            ViewBag.BugTypeId = new SelectList(db.BugTypes, "Id", "TypeOfBug", bugReport.BugTypeId);
            this.AddToastMessage("ERROR", "You must enter all field correctly!", ToastType.Error);
            return View(bugReport);
        }
        // GET: BugReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugReport bugReport = db.BugReports.Find(id);
            if (bugReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectsId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status", bugReport.StatusId);
            ViewBag.ReleaseId = new SelectList(db.Releases, "Id", "Version", bugReport.ReleaseId);
            ViewBag.SprintsId = new SelectList(db.Sprints, "Id", "Name", bugReport.SprintsId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", bugReport.UserId);
            ViewBag.BugTypeId = new SelectList(db.BugTypes, "Id", "TypeOfBug", bugReport.BugTypeId);
            return View(bugReport);
        }

        // POST: BugReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,BugTypeId,Description,Created,Creator,ProductName,BugDetails,Status,StatusId,ReleaseId,SprintsId,UserId,Backlog,ProjectsId")] BugReport bugReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bugReport).State = EntityState.Modified;
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully edited this entry!", ToastType.Success);
                return RedirectToAction("Index");
            }
            ViewBag.ProjectsId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status", bugReport.StatusId);
            ViewBag.ReleaseId = new SelectList(db.Releases, "Id", "Version", bugReport.ReleaseId);
            ViewBag.SprintsId = new SelectList(db.Sprints, "Id", "Name", bugReport.SprintsId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", bugReport.UserId);
            ViewBag.BugTypeId = new SelectList(db.BugTypes, "Id", "TypeOfBug", bugReport.BugTypeId);
            this.AddToastMessage("ERROR", "You must enter all field corrctly!", ToastType.Error);
            return View(bugReport);
        }
        // GET: BugReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugReport bugReport = db.BugReports.Find(id);
            if (bugReport == null)
            {
                return HttpNotFound();
            }
            this.AddToastMessage("Warning!", "You are deleting from database and if you delete it can not be undone!", ToastType.Warning);
            return View(bugReport);
        }

        // POST: BugReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BugReport bugReport = db.BugReports.Find(id);
            db.BugReports.Remove(bugReport);
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
