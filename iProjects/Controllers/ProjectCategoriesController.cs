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
    public class ProjectCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProjectCategories
        public ActionResult Index()
        {
            this.AddToastMessage("Welcome", "Here you can see all Project Categories!", ToastType.Info);
            return View(db.ProjectCategories.ToList());
        }

        [ChildActionOnly]
        public ActionResult Statistics()
        {
            var statsModel = new ProjectCategory();
            statsModel.ProjectCategoryId = (db.ProjectCategories).Count();
            return View(statsModel);
        }
        // GET: ProjectCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCategory projectCategory = db.ProjectCategories.Find(id);
            if (projectCategory == null)
            {
                return HttpNotFound();
            }
            return View(projectCategory);
        }

        // GET: ProjectCategories/Create
        public ActionResult Create()
        {
            this.AddToastMessage("Welcome", "Create new project category!", ToastType.Info);
            return View();
        }

        // POST: ProjectCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectCategoryId,CategoryName,Description")] ProjectCategory projectCategory)
        {
            if (ModelState.IsValid)
            {
                db.ProjectCategories.Add(projectCategory);
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully Created new Project Category!", ToastType.Success);
                return RedirectToAction("Index");
                
            }
            this.AddToastMessage("Warning", "You must enter all field correctly!", ToastType.Warning);
            return View(projectCategory);
        }

        // GET: ProjectCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCategory projectCategory = db.ProjectCategories.Find(id);
            if (projectCategory == null)
            {
                return HttpNotFound();
            }
            return View(projectCategory);
        }

        // POST: ProjectCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectCategoryId,CategoryName,Description")] ProjectCategory projectCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectCategory).State = EntityState.Modified;
                db.SaveChanges();
                this.AddToastMessage("Congratulations", "You have successfully edited Project Category!", ToastType.Success);
                return RedirectToAction("Index");
            }
            this.AddToastMessage("Warning", "You must enter all field correctly!", ToastType.Warning);
            return View(projectCategory);
        }

        // GET: ProjectCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCategory projectCategory = db.ProjectCategories.Find(id);
            if (projectCategory == null)
            {
                return HttpNotFound();
            }
            this.AddToastMessage("Warning!", "You are deleting from database and if you delete it can not be undone!", ToastType.Warning);
            return View(projectCategory);
        }

        // POST: ProjectCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectCategory projectCategory = db.ProjectCategories.Find(id);
            db.ProjectCategories.Remove(projectCategory);
            db.SaveChanges();
            this.AddToastMessage("Success.", "You have successfully deleted project category!", ToastType.Success);
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
