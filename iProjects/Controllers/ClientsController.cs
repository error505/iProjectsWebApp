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
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin")]
        // GET: Clients
        public ActionResult Index()
        {
            this.AddToastMessage("Welcome", "Here you can see all Clients!", ToastType.Info);
            return View(db.Clients.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }
        [Authorize(Roles = "Admin")]
        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,Name,Email,Password,Address,Phone,Company,Website,Skype,Facebook,LinkedIn,Twitter,Note")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(clients);
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully Created new a new Client!", ToastType.Success);
                return RedirectToAction("Index");
            }
            this.AddToastMessage("ERROR", "You must enter all field correctly!", ToastType.Error);
            return View(clients);
        }

        [ChildActionOnly]
        public ActionResult Statistics()
        {
            var statsModel = new Clients();           
                statsModel.ClientId = (db.Clients).Count();
                return View(statsModel);          
        }

        [Authorize(Roles = "Admin")]
        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,Name,Email,Password,Address,Phone,Company,Website,Skype,Facebook,LinkedIn,Twitter,Note")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clients).State = EntityState.Modified;
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully edited Client!", ToastType.Success);
                return RedirectToAction("Index");
            }
            this.AddToastMessage("ERROR", "You must enter all field correctly!", ToastType.Error);
            return View(clients);
        }
        [Authorize(Roles = "Admin")]
        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            this.AddToastMessage("Warning!", "You are deleting from database and if you delete it can not be undone!", ToastType.Warning);
            return View(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clients clients = db.Clients.Find(id);
            db.Clients.Remove(clients);
            db.SaveChanges();
            this.AddToastMessage("Congratulations", "You have successfully deleted Client!", ToastType.Success);
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
