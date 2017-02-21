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
    public class HeadLineTwoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HeadLineTwoes
        public ActionResult Index()
        {
            return View(db.HeadLineTwos.ToList());
        }

        public ActionResult Two()
        {
            return View(db.HeadLineTwos.ToList());
        }

        // GET: HeadLineTwoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadLineTwo headLineTwo = db.HeadLineTwos.Find(id);
            if (headLineTwo == null)
            {
                return HttpNotFound();
            }
            return View(headLineTwo);
        }

        // GET: HeadLineTwoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HeadLineTwoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Message")] HeadLineTwo headLineTwo)
        {
            if (ModelState.IsValid)
            {
                db.HeadLineTwos.Add(headLineTwo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(headLineTwo);
        }

        // GET: HeadLineTwoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadLineTwo headLineTwo = db.HeadLineTwos.Find(id);
            if (headLineTwo == null)
            {
                return HttpNotFound();
            }
            return View(headLineTwo);
        }

        // POST: HeadLineTwoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Message")] HeadLineTwo headLineTwo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(headLineTwo).State = EntityState.Modified;
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully edited second news!", ToastType.Success);
                return RedirectToAction("Index", "Home");
            }
            return View(headLineTwo);
        }

        // GET: HeadLineTwoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeadLineTwo headLineTwo = db.HeadLineTwos.Find(id);
            if (headLineTwo == null)
            {
                return HttpNotFound();
            }
            return View(headLineTwo);
        }

        // POST: HeadLineTwoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HeadLineTwo headLineTwo = db.HeadLineTwos.Find(id);
            db.HeadLineTwos.Remove(headLineTwo);
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
