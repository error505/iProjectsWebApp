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
    public class SupportTicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SupportTickets
        public ActionResult Index()
        {
            this.AddToastMessage("Welcome", "Here you can see all Support Tickets!", ToastType.Info);
            return View(db.SupportTickets.ToList());
        }

        [ChildActionOnly]
        public ActionResult Statistics()
        {
            var statsModel = new SupportTicket();
            statsModel.Id = (db.SupportTickets).Count();
            return View(statsModel);
        }
        // GET: SupportTickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportTicket supportTicket = db.SupportTickets.Find(id);
            if (supportTicket == null)
            {
                return HttpNotFound();
            }
            return View(supportTicket);
        }

        // GET: SupportTickets/Create
        public ActionResult Create()
        {
            this.AddToastMessage("Welcome", "Create Support Ticket!", ToastType.Info);
            return View();
        }

        // POST: SupportTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,From,ProjectName,Message,Status")] SupportTicket supportTicket)
        {
            if (ModelState.IsValid)
            {
                db.SupportTickets.Add(supportTicket);
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully created new Ticket!", ToastType.Success);
                return RedirectToAction("Index");
            }
            this.AddToastMessage("Warning", "You must enter all fields correctly!", ToastType.Warning);
            return View(supportTicket);
        }

        // GET: SupportTickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportTicket supportTicket = db.SupportTickets.Find(id);
            if (supportTicket == null)
            {
                return HttpNotFound();
            }
            return View(supportTicket);
        }

        // POST: SupportTickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,From,ProjectName,Message,Status")] SupportTicket supportTicket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supportTicket).State = EntityState.Modified;
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully edited your Ticket!", ToastType.Success);
                return RedirectToAction("Index");
            }
            this.AddToastMessage("Warning", "You must enter all fields correctly in order to edit Support Ticket!", ToastType.Warning);
            return View(supportTicket);
        }

        // GET: SupportTickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportTicket supportTicket = db.SupportTickets.Find(id);
            if (supportTicket == null)
            {
                return HttpNotFound();
            }
            this.AddToastMessage("Warning!", "You are deleting from database and if you delete it can not be undone!", ToastType.Warning);
            return View(supportTicket);
        }

        // POST: SupportTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupportTicket supportTicket = db.SupportTickets.Find(id);
            db.SupportTickets.Remove(supportTicket);
            db.SaveChanges();
            this.AddToastMessage("Success.", "You have successfully deleted Ticket!", ToastType.Success);
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
