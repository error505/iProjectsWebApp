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
    public class EpicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Epics
        public ActionResult Index()
        {
            return View(db.Epics.ToList());
        }

        // GET: Epics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Epic epic = db.Epics.Find(id);
            if (epic == null)
            {
                return HttpNotFound();
            }
            return View(epic);
        }

        // GET: Epics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Epics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Epic epic)
        {
            if (ModelState.IsValid)
            {
                db.Epics.Add(epic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(epic);
        }

        // GET: Epics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Epic epic = db.Epics.Find(id);
            if (epic == null)
            {
                return HttpNotFound();
            }
            return View(epic);
        }

        // POST: Epics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description")] Epic epic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(epic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(epic);
        }

        // GET: Epics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Epic epic = db.Epics.Find(id);
            if (epic == null)
            {
                return HttpNotFound();
            }
            return View(epic);
        }

        // POST: Epics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Epic epic = db.Epics.Find(id);
            db.Epics.Remove(epic);
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
