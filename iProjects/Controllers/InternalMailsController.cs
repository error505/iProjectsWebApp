using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iProjects.Models;
using Microsoft.AspNet.Identity;

namespace iProjects.Controllers
{
    public class InternalMailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InternalMails
        public ActionResult Index()
        {
            var internalMails = db.InternalMails.Include(i => i.User);
            return View(internalMails.ToList());
        }

        public ActionResult Archive()
        {
            var internalMails = db.InternalMails.Include(i => i.User);
            return View(internalMails.ToList());
        }

        public ActionResult Spam()
        {
            var internalMails = db.InternalMails.Include(i => i.User);
            return View(internalMails.ToList());
        }

        public ActionResult Sent()
        {
            var internalMails = db.InternalMails.Include(i => i.User);
            return View(internalMails.ToList());
        }

        // GET: InternalMails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InternalMail internalMail = db.InternalMails.Find(id);
            if (internalMail == null)
            {
                return HttpNotFound();
            }
            return View(internalMail);
        }

        // GET: InternalMails/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: InternalMails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TimeOfCreation,Subject,Message,From,UserId,AttachmentFile,IsRead,Archive,Spam")] InternalMail internalMail)
        {
            if (ModelState.IsValid)
            {
                db.InternalMails.Add(internalMail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", internalMail.UserId);
            return View(internalMail);
        }

        public ActionResult Notifications()
        {
            var internalMails = db.InternalMails.Include(i => i.User);
            return View(internalMails.ToList());
        }

        public ActionResult ListNotifications()
        {
            var internalMails = db.InternalMails.Include(i => i.User);
            return View(internalMails.ToList());
        }

        public ActionResult Statistic()
        {
            var internalMails = db.InternalMails.Include(i => i.User);
            return View(internalMails.ToList());
        }

        public ActionResult SpamStatistic()
        {
            var internalMails = db.InternalMails.Include(i => i.User);
            return View(internalMails.ToList());
        }

        public ActionResult ArchiveStatistic()
        {
            var internalMails = db.InternalMails.Include(i => i.User);
            return View(internalMails.ToList());
        }

        public ActionResult SentStatistic()
        {
            var internalMails = db.InternalMails.Include(i => i.User);
            return View(internalMails.ToList());
        }

        // GET: InternalMails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InternalMail internalMail = db.InternalMails.Find(id);
            if (internalMail == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", internalMail.UserId);
            return View(internalMail);
        }

        // POST: InternalMails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeOfCreation,Subject,Message,From,UserId,AttachmentFile,IsRead,Archive,Spam")] InternalMail internalMail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(internalMail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", internalMail.UserId);
            return View(internalMail);
        }

        // GET: InternalMails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InternalMail internalMail = db.InternalMails.Find(id);
            if (internalMail == null)
            {
                return HttpNotFound();
            }
            return View(internalMail);
        }

        // POST: InternalMails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InternalMail internalMail = db.InternalMails.Find(id);
            db.InternalMails.Remove(internalMail);
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
