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
    public class HeadLineThreesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HeadLineThrees
        public ActionResult Index()
        {
            return View(db.HeadLineThrees.ToList());
        }

        public ActionResult Three()
        {
            return View(db.HeadLineThrees.ToList());
        }

        // GET: HeadLineThrees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadLineThree headLineThree = db.HeadLineThrees.Find(id);
            if (headLineThree == null)
            {
                return HttpNotFound();
            }
            return View(headLineThree);
        }

        // GET: HeadLineThrees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HeadLineThrees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Message")] HeadLineThree headLineThree)
        {
            if (ModelState.IsValid)
            {
                db.HeadLineThrees.Add(headLineThree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(headLineThree);
        }

        // GET: HeadLineThrees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadLineThree headLineThree = db.HeadLineThrees.Find(id);
            if (headLineThree == null)
            {
                return HttpNotFound();
            }
            return View(headLineThree);
        }

        // POST: HeadLineThrees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Message")] HeadLineThree headLineThree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(headLineThree).State = EntityState.Modified;
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully edited third news!", ToastType.Success);
                return RedirectToAction("Index", "Home");
            }
            return View(headLineThree);
        }

        // GET: HeadLineThrees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadLineThree headLineThree = db.HeadLineThrees.Find(id);
            if (headLineThree == null)
            {
                return HttpNotFound();
            }
            return View(headLineThree);
        }

        // POST: HeadLineThrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HeadLineThree headLineThree = db.HeadLineThrees.Find(id);
            db.HeadLineThrees.Remove(headLineThree);
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
