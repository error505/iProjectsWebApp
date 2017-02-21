using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iProjects.Models;

namespace iProjects.Controllers
{
    public class ReleasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Releases
        public ActionResult Index()
        {
            var releases = db.Releases.Include(r => r.Project);
            return View(releases.ToList());
        }

        // GET: Releases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Releases releases = db.Releases.Find(id);
            if (releases == null)
            {
                return HttpNotFound();
            }
            return View(releases);
        }

        // GET: Releases/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status");
            return View();
        }

        // POST: Releases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ProjectId,TimeOfCreation,StartDate,ReleaseDate,Description,StatusId,Version")] Releases releases)
        {
            if (ModelState.IsValid)
            {
                db.Releases.Add(releases);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status", releases.StatusId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", releases.ProjectId);
            return View(releases);
        }

        // GET: Releases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Releases releases = db.Releases.Find(id);
            if (releases == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status", releases.StatusId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", releases.ProjectId);
            return View(releases);
        }

        // POST: Releases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ProjectId,TimeOfCreation,StartDate,ReleaseDate,Description,StatusId,Version")] Releases releases)
        {
            if (ModelState.IsValid)
            {
                db.Entry(releases).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status", releases.StatusId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", releases.ProjectId);
            return View(releases);
        }

        // GET: Releases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Releases releases = db.Releases.Find(id);
            if (releases == null)
            {
                return HttpNotFound();
            }
            return View(releases);
        }

        // POST: Releases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Releases releases = db.Releases.Find(id);
            db.Releases.Remove(releases);
            db.SaveChanges();
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
