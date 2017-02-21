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
    public class ProjectFilesDbsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FileUploadDBModels
        public ActionResult Index()
        {
            var model = new ProjectFile();
            this.AddToastMessage("Welcome", "Upload files to database!", ToastType.Info);
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult Statistics()
        {
            var statsModel = new ProjectFilesDb();
            statsModel.Id = (db.ProjectFilesDbs).Count();
            return View(statsModel);
        }
        [HttpPost]
        public ActionResult Index(ProjectFile model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ProjectFilesDb fileUploadModel = new ProjectFilesDb();

            foreach (var item in model.File)  //3rd change
            {
                byte[] uploadFile = new byte[item.InputStream.Length];
                item.InputStream.Read(uploadFile, 0, uploadFile.Length);

                fileUploadModel.FileName = item.FileName;
                fileUploadModel.File = uploadFile;

                db.ProjectFilesDbs.Add(fileUploadModel);
                db.SaveChanges();
            }
            this.AddToastMessage("Success", "File has been uploaded!", ToastType.Success);
            return View("Index");
        }

        public ActionResult Download()
        {
            return View(db.ProjectFilesDbs.ToList());
        }

        public FileContentResult FileDownload(int? id)
        {
            byte[] fileData;
            string fileName;

            ProjectFilesDb fileRecord = db.ProjectFilesDbs.Find(id);

            fileData = (byte[])fileRecord.File.ToArray();
            fileName = fileRecord.FileName;
            var mimeType = MimeMapping.GetMimeMapping(fileRecord.FileName);
            return File(fileData, mimeType, fileName);
        }

        // GET: FileUploadDBModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectFilesDb fileUploadDBModel = db.ProjectFilesDbs.Find(id);
            if (fileUploadDBModel == null)
            {
                return HttpNotFound();
            }
            return View(fileUploadDBModel);
        }

        // GET: FileUploadDBModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileUploadDBModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FileName,File")] ProjectFilesDb fileUploadDBModel)
        {
            if (ModelState.IsValid)
            {
                db.ProjectFilesDbs.Add(fileUploadDBModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fileUploadDBModel);
        }

        //// GET: FileUploadDBModels/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ProjectFilesDb fileUploadDBModel = db.ProjectFilesDbs.Find(id);
        //    if (fileUploadDBModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(fileUploadDBModel);
        //}

        //// POST: FileUploadDBModels/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "UserId,ProjectsId,StoriesId,TasksId")] ProjectFilesDb fileUploadDBModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(fileUploadDBModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //     ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
        //    ViewBag.StoriesId = new SelectList(db.Stories, "StoryId", "Name");
        //    ViewBag.StoriesId = new SelectList(db.TasksToDoes, "Id", "Title");
        //    ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectTitle");
        //    return View(fileUploadDBModel);
        //}

        // GET: FileUploadDBModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectFilesDb fileUploadDBModel = db.ProjectFilesDbs.Find(id);
            if (fileUploadDBModel == null)
            {
                return HttpNotFound();
            }
            this.AddToastMessage("Warning!", "You are deleting file from database and if you delete it can not be undone!", ToastType.Warning);
            return View(fileUploadDBModel);
        }

        // POST: FileUploadDBModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectFilesDb fileUploadDBModel = db.ProjectFilesDbs.Find(id);
            db.ProjectFilesDbs.Remove(fileUploadDBModel);
            db.SaveChanges();
            this.AddToastMessage("Success.", "You have successfully deleted uploaded file!", ToastType.Success);
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