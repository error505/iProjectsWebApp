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
    public class StatusOfStoriesTasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StatusOfStoriesTasks
        public ActionResult Index()
        {
            return View(db.StatusOfStoriesTasks.ToList());
        }

        // GET: StatusOfStoriesTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOfStoriesTasks statusOfStoriesTasks = db.StatusOfStoriesTasks.Find(id);
            if (statusOfStoriesTasks == null)
            {
                return HttpNotFound();
            }
            return View(statusOfStoriesTasks);
        }

        // GET: StatusOfStoriesTasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusOfStoriesTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Status")] StatusOfStoriesTasks statusOfStoriesTasks)
        {
            if (ModelState.IsValid)
            {
                db.StatusOfStoriesTasks.Add(statusOfStoriesTasks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statusOfStoriesTasks);
        }

        // GET: StatusOfStoriesTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOfStoriesTasks statusOfStoriesTasks = db.StatusOfStoriesTasks.Find(id);
            if (statusOfStoriesTasks == null)
            {
                return HttpNotFound();
            }
            return View(statusOfStoriesTasks);
        }

        // POST: StatusOfStoriesTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Status")] StatusOfStoriesTasks statusOfStoriesTasks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statusOfStoriesTasks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statusOfStoriesTasks);
        }

        // GET: StatusOfStoriesTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusOfStoriesTasks statusOfStoriesTasks = db.StatusOfStoriesTasks.Find(id);
            if (statusOfStoriesTasks == null)
            {
                return HttpNotFound();
            }
            return View(statusOfStoriesTasks);
        }

        // POST: StatusOfStoriesTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusOfStoriesTasks statusOfStoriesTasks = db.StatusOfStoriesTasks.Find(id);
            db.StatusOfStoriesTasks.Remove(statusOfStoriesTasks);
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
