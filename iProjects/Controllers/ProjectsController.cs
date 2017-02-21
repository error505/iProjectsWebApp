using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using iProjects.Models;
using iProjects.Toast;
using iProjects.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace iProjects.Controllers
{
    public class ProjectsController : Controller
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
        // GET: Projects
        public ActionResult Index()
        {
            IQueryable<Project> projects = db.Projects.Include(p => p.Client).Include(p => p.ProjectCategory).Include(p => p.User);
            this.AddToastMessage("Welcome", "Here you can see all Projects!", ToastType.Info);
            return View(projects.ToList());
        }

        public ActionResult List()
        {
            IQueryable<Project> projects = db.Projects.Include(p => p.Client).Include(p => p.ProjectCategory).Include(p => p.User);
            return View(projects.ToList());
        }

        [HttpPost]
        public async Task<ActionResult> List(Project model)
        {
            IQueryable<Project> projects = db.Projects.Include(p => p.Client).Include(p => p.ProjectCategory).Include(p => p.User);
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != db.Projects.Include(p => p.UserId))
            {
                return View("NotAssigned");
            }
            return View(projects.ToList());
        }

        [ChildActionOnly]
        public ActionResult Statistics()
        {
            Project statsModel = new Project();
            statsModel.ProjectId = (db.Projects).Count();
            return View(statsModel);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int id)
        {
            var Catlist = db.Projects.Include(c => c.ProjectCategory).ToList();
            Project project = Catlist.Single(i => i.ProjectId == id);
            var projectcat = db.ProjectCategories.FirstOrDefault(i => i.ProjectCategoryId == project.ProjectCategoryId);
            if (project == null)
            {
                return HttpNotFound();
            }
            project.ProjectCategory = projectcat;
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            Project project = new Project();
            
            Project proj = new Project();
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name");
            ViewBag.ProjectCategoryId = new SelectList(db.ProjectCategories, "ProjectCategoryId", "CategoryName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            this.AddToastMessage("Welcome", "Create your Project!", ToastType.Info);
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectId,ProjectTitle,Finished,Description,Url,ProjectCategoryId,ClientId,Budget,StartDate,EndDate,TotalTimeSpent,UserId")] Project project)
        {
            
            IEnumerable<ModelError> modelStateErrors = this.ModelState.Values.SelectMany(m => m.Errors);
            if (ModelState.IsValid)
            {
                if (project.UserOnProject != null)
                {
                    foreach (ApplicationUser userId in project.UserOnProject)
                    {
                        IQueryable<ApplicationUser> user = db.Users.Where(t => t.Id == userId.ToString());
                    }
                }
                db.Projects.Add(project);
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully created new project!", ToastType.Success);
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
            ViewBag.ProjectCategoryId = new SelectList(db.ProjectCategories, "ProjectCategoryId", "CategoryName", project.ProjectCategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", project.UserId);
            this.AddToastMessage("Warning", "You must enter all fields correctly!", ToastType.Warning);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            //ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            //if (project.UserId != user.Id)
            //{
            //    this.AddToastMessage("Warning", "You Dont Have Permision to edit this!", ToastType.Warning);
            //    return RedirectToAction("Index");
            //}
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
            ViewBag.ProjectCategoryId = new SelectList(db.ProjectCategories, "ProjectCategoryId", "CategoryName", project.ProjectCategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", project.UserId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ProjectTitle,Finished,Description,Url,ProjectCategoryId,ClientId,Budget,StartDate,EndDate,TotalTimeSpent,UserId")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully edited project!", ToastType.Success);
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Name", project.ClientId);
            ViewBag.ProjectCategoryId = new SelectList(db.ProjectCategories, "ProjectCategoryId", "CategoryName", project.ProjectCategoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", project.UserId);
            this.AddToastMessage("Warning", "You must enter all fields correctly in order to edit project!", ToastType.Warning);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            //ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            //if (project.UserId != user.Id)
            //{
            //    this.AddToastMessage("Warning", "You Dont Have Permision to edit this!", ToastType.Warning);
            //    return RedirectToAction("Index");
            //}
            this.AddToastMessage("Warning!", "You are deleting from database and if you delete it can not be undone!", ToastType.Warning);
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            this.AddToastMessage("Success.", "You have successfully deleted your invoice!", ToastType.Success);
            return RedirectToAction("Index");
        }

        public ActionResult CreationDateStatistic()
        {
            IQueryable<CreationDateProj> data = from project in db.Projects
                                                   group project by project.TimeOfCreation into dateGroup
                                                select new CreationDateProj()
                                                   {
                                                       CreationDate = dateGroup.Key,
                                                       ProjectCount = dateGroup.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult GetChartData()
        {
            IList<Project> projects;
            using (ApplicationDbContext db2 = new ApplicationDbContext())
            {
                projects = db2.Projects.ToList();
            }


            //var dataForChart = new object[projects.Count + 1];
            //dataForChart[0] = new object[]
            //{
            //    "date"
            //};
            //int j = 0;
            //foreach (var i in projects)
            //{
            //    j++;
            //    dataForChart[j] = new object[] { i.TimeOfCreation.ToString() };
            //}
           List<CreationDateProj> data = (from project in db.Projects
                                                 group project by project.TimeOfCreation into dateGroup
                                                 select new CreationDateProj
                                                 {
                                                     CreationDate = dateGroup.Key,
                                                     ProjectCount = dateGroup.Count()
                                                 }).Take(7).ToList();

            var dataForChart = new[]
            {
                new { Key = 1, Value = 101},
                new { Key = 2, Value = 102},
                new { Key = 3, Value = 103},
                new { Key = 4, Value = 104},
                new { Key = 5, Value = 105},
                new { Key = 6, Value = 106},
                new { Key = 7, Value = 107}
            };
            var result = Json(data, JsonRequestBehavior.AllowGet);
            return result;
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
