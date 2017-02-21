using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using iProjects.Models;
using iProjects.Toast;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NHibernate.Mapping;

namespace iProjects.Controllers
{
    public class TasksToDoesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: TasksToDoes
        public ActionResult Index()
        {

            var tasksToDoes = db.TasksToDoes.Include(t => t.Project).Include(t => t.User);
            this.AddToastMessage("Welcome", "Here you can see all Task that need to be done!", ToastType.Info);
            return View(tasksToDoes.ToList());
        }
        
        public ActionResult List()
        {
            //TasksToDo tasksToDo = db.TasksToDoes.Find(id);
            //ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            //if (tasksToDo.UserId != user.Id)
            //{
            //    this.AddToastMessage("Warning", "You Dont Have Permision to edit this!", ToastType.Warning);
            //    return RedirectToAction("Index");
            //}
            var tasksToDoes = db.TasksToDoes.Include(t => t.Project).Include(t => t.User);
            return View(tasksToDoes.ToList());
        }

        public ActionResult BaklogList()
        {
            var tasksToDoes = db.TasksToDoes.Include(t => t.Project).Include(t => t.User);
            return View(tasksToDoes.ToList());
        }

        public ActionResult ProjList()
        {
            var tasksToDoes = db.TasksToDoes.Include(t => t.Project).Include(t => t.User);
            return View(tasksToDoes.ToList());
        }

        public ActionResult SprintList()
        {
            var tasksToDoes = db.TasksToDoes.Include(t => t.Project).Include(t => t.User);
            return View(tasksToDoes.ToList());
        }

        public ActionResult TasksNotyList()
        {
           var tasksToDoes = db.TasksToDoes.Include(t => t.Project).Include(t => t.User);
            return View(tasksToDoes.ToList());
        }

        [ChildActionOnly]
        public ActionResult Statistics()
        {
            var statsModel = new TasksToDo();
            statsModel.Id = (db.TasksToDoes).Count();
            return View(statsModel);
        }

      
        public ActionResult Notifications()
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            var statsModel = new TasksToDo();
            statsModel.Id = (db.TasksToDoes).Count();
            //var i = from task in db.TasksToDoes
            //        group task by task.IsDone == false
            //            into notyGrouop
            //            select new TasksToDo { UserId = user.Id, Id = statsModel.Id };
            ViewBag.Count = statsModel;
            var tasksToDoes = db.TasksToDoes.Include(t => t.User);            
            return View(tasksToDoes);
        }

        // GET: TasksToDoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasksToDo tasksToDo = db.TasksToDoes.Find(id);
            if (tasksToDo == null)
            {
                return HttpNotFound();
            }
            return View(tasksToDo);
        }

        // GET: TasksToDoes/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status");
            ViewBag.PrioritiesId = new SelectList(db.Priorities, "Id", "Name");
            ViewBag.ReleaseId = new SelectList(db.Releases, "Id", "Version");
            ViewBag.SprintsId = new SelectList(db.Sprints, "Id", "Name");
            ViewBag.StoriesId = new SelectList(db.Stories, "StoryId", "Name");
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            this.AddToastMessage("Welcome", "Create Task To Do!", ToastType.Info);
            return View();
        }

        // POST: TasksToDoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,IsDone,UserId,ProjectId,TestReference,UnitTestReference,StatusId,PrioritiesId,ReleaseId,SprintsId,StoriesId,Backlog")] TasksToDo tasksToDo)
        {
            if (ModelState.IsValid)
            {
                db.TasksToDoes.Add(tasksToDo);
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully created new Task!", ToastType.Success);
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status", tasksToDo.StatusId);
            ViewBag.PrioritiesId = new SelectList(db.Priorities, "Id", "Name", tasksToDo.PrioritiesId);
            ViewBag.ReleaseId = new SelectList(db.Releases, "Id", "Version", tasksToDo.ReleaseId);
            ViewBag.SprintsId = new SelectList(db.Sprints, "Id", "Name", tasksToDo.SprintsId);
            ViewBag.StoriesId = new SelectList(db.Stories, "StoryId", "Name", tasksToDo.StoriesId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", tasksToDo.ProjectId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", tasksToDo.UserId);
            this.AddToastMessage("Warning", "You must enter all fields correctly!", ToastType.Warning);
            return View(tasksToDo);
        }

        // GET: TasksToDoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasksToDo tasksToDo = db.TasksToDoes.Find(id);
            if (tasksToDo == null)
            {
                return HttpNotFound();
            }
            //ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            //if (tasksToDo.UserId != user.Id)
            //{
            //    this.AddToastMessage("Warning", "You Dont Have Permision to edit this!", ToastType.Warning);
            //    return RedirectToAction("Index");
            //}
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status", tasksToDo.StatusId);
            ViewBag.PrioritiesId = new SelectList(db.Priorities, "Id", "Name", tasksToDo.PrioritiesId);
            ViewBag.ReleaseId = new SelectList(db.Releases, "Id", "Version", tasksToDo.ReleaseId);
            ViewBag.SprintsId = new SelectList(db.Sprints, "Id", "Name", tasksToDo.SprintsId);
            ViewBag.StoriesId = new SelectList(db.Stories, "StoryId", "Name", tasksToDo.StoriesId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", tasksToDo.ProjectId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", tasksToDo.UserId);
            return View(tasksToDo);
        }

        // POST: TasksToDoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,IsDone,UserId,ProjectId,TestReference,UnitTestReference,StatusId,PrioritiesId,ReleaseId,SprintsId,StoriesId,Backlog")] TasksToDo tasksToDo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasksToDo).State = EntityState.Modified;
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully finished your Task!", ToastType.Success);
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(db.StatusOfStoriesTasks, "Id", "Status", tasksToDo.StatusId);
            ViewBag.PrioritiesId = new SelectList(db.Priorities, "Id", "Name", tasksToDo.PrioritiesId);
            ViewBag.ReleaseId = new SelectList(db.Releases, "Id", "Version", tasksToDo.ReleaseId);
            ViewBag.SprintsId = new SelectList(db.Sprints, "Id", "Name", tasksToDo.SprintsId);
            ViewBag.StoriesId = new SelectList(db.Stories, "StoryId", "Name", tasksToDo.StoriesId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle", tasksToDo.ProjectId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", tasksToDo.UserId);
            this.AddToastMessage("Warning", "You must enter all fields correctly in order to edit Support Task!", ToastType.Warning);
            return View(tasksToDo);
        }

        // GET: TasksToDoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasksToDo tasksToDo = db.TasksToDoes.Find(id);
            if (tasksToDo == null)
            {
                return HttpNotFound();
            }
            //ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            //if (tasksToDo.UserId != user.Id)
            //{
            //    this.AddToastMessage("Warning", "You Dont Have Permision to edit this!", ToastType.Warning);
            //    return RedirectToAction("Index");
            //}
            this.AddToastMessage("Warning!", "You are deleting from database and if you delete it can not be undone!", ToastType.Warning);
            return View(tasksToDo);
        }

        // POST: TasksToDoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TasksToDo tasksToDo = db.TasksToDoes.Find(id);
            db.TasksToDoes.Remove(tasksToDo);
            db.SaveChanges();
            this.AddToastMessage("Success.", "You have successfully deleted Task!", ToastType.Success);
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
