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
    public class HeadLineOnesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HeadLineOnes
        public ActionResult Index()
        {
            return View(db.HeadLineOnes.ToList());
        }
        public ActionResult One()
        {
            return View(db.HeadLineOnes.ToList());
        }
        // GET: HeadLineOnes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadLineOne headLineOne = db.HeadLineOnes.Find(id);
            if (headLineOne == null)
            {
                return HttpNotFound();
            }
            return View(headLineOne);
        }

        // GET: HeadLineOnes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HeadLineOnes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Message")] HeadLineOne headLineOne)
        {
            if (ModelState.IsValid)
            {
                db.HeadLineOnes.Add(headLineOne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(headLineOne);
        }

        // GET: HeadLineOnes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadLineOne headLineOne = db.HeadLineOnes.Find(id);
            if (headLineOne == null)
            {
                return HttpNotFound();
            }
            return View(headLineOne);
        }

        // POST: HeadLineOnes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Message")] HeadLineOne headLineOne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(headLineOne).State = EntityState.Modified;
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully edited first news!", ToastType.Success);
                return RedirectToAction("Index", "Home");
            }
            return View(headLineOne);
        }

        // GET: HeadLineOnes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadLineOne headLineOne = db.HeadLineOnes.Find(id);
            if (headLineOne == null)
            {
                return HttpNotFound();
            }
            return View(headLineOne);
        }

        // POST: HeadLineOnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HeadLineOne headLineOne = db.HeadLineOnes.Find(id);
            db.HeadLineOnes.Remove(headLineOne);
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
