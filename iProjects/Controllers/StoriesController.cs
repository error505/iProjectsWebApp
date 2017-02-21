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
    public class StoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stories
        public ActionResult Index()
        {
            var stories = db.Stories.Include(s => s.Epic).Include(s => s.Sprint).Include(s => s.User);
            return View(stories.ToList());
        }

        public ActionResult List()
        {
            var stories = db.Stories.Include(s => s.Epic).Include(s => s.Sprint).Include(s => s.User);
            return View(stories.ToList());
        }

        public ActionResult BacklogList()
        {
            var stories = db.Stories.Include(s => s.Epic).Include(s => s.Sprint).Include(s => s.User);
            return View(stories.ToList());
        }

        public ActionResult ProjList()
        {
            var stories = db.Stories.Include(s => s.Epic).Include(s => s.Sprint).Include(s => s.User);
            return View(stories.ToList());
        }

        public ActionResult SprintList()
        {
            var stories = db.Stories.Include(s => s.Epic).Include(s => s.Sprint).Include(s => s.User);
            return View(stories.ToList());
        }

        // GET: Stories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // GET: Stories/Create
        public ActionResult Create()
        {
            ViewBag.ProjectsId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status");
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name");
            ViewBag.ReleaseId = new SelectList(db.Releases, "Id", "Version");
            ViewBag.EpicId = new SelectList(db.Epics, "Id", "Name");
            ViewBag.SprintsId = new SelectList(db.Sprints, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StoryId,Name,Description,AcceptanceCriteria,PriorityId,StatusId,TimeOfCreation,UserId,SprintsId,EpicId,ReleaseId,Backlog,Revision,TestReference,UnitTestReference,ProjectsId")] Story story)
        {
            if (ModelState.IsValid)
            {
                db.Stories.Add(story);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectsId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status", story.StatusId);
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", story.PriorityId);
            ViewBag.ReleaseId = new SelectList(db.Releases, "Id", "Version", story.ReleaseId);
            ViewBag.EpicId = new SelectList(db.Epics, "Id", "Name", story.EpicId);
            ViewBag.SprintsId = new SelectList(db.Sprints, "Id", "Name", story.SprintsId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", story.UserId);
            return View(story);
        }

        // GET: Stories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectsId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status", story.StatusId);
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", story.PriorityId);
            ViewBag.ReleaseId = new SelectList(db.Releases, "Id", "Version", story.ReleaseId);
            ViewBag.EpicId = new SelectList(db.Epics, "Id", "Name", story.EpicId);
            ViewBag.SprintsId = new SelectList(db.Sprints, "Id", "Name", story.SprintsId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", story.UserId);
            return View(story);
        }

        [ChildActionOnly]
        public ActionResult Statistics()
        {
            var statsModel = new Story();
            statsModel.StoryId = (db.Stories).Count();
            return View(statsModel);
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StoryId,Name,Description,AcceptanceCriteria,PriorityId,StatusId,TimeOfCreation,UserId,SprintsId,EpicId,ReleaseId,Backlog,Revision,TestReference,UnitTestReference,ProjectsId")] Story story)
        {
            if (ModelState.IsValid)
            {
                db.Entry(story).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectsId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status", story.StatusId);
            ViewBag.PriorityId = new SelectList(db.Priorities, "Id", "Name", story.PriorityId);
            ViewBag.ReleaseId = new SelectList(db.Releases, "Id", "Version", story.ReleaseId);
            ViewBag.EpicId = new SelectList(db.Epics, "Id", "Name", story.EpicId);
            ViewBag.SprintsId = new SelectList(db.Sprints, "Id", "Name", story.SprintsId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", story.UserId);
            return View(story);
        }

        // GET: Stories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
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
