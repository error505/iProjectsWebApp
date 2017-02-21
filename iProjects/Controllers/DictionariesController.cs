using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using iProjects.Models;
using PagedList;

namespace iProjects.Controllers
{
    public class DictionariesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dictionaries
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.WordSort = String.IsNullOrEmpty(sortOrder) ? "foreignword_desc" : "";
            ViewBag.GenderKindSort = String.IsNullOrEmpty(sortOrder) ? "genderkind_desc" : "";
            ViewBag.TranslateSort = sortOrder == "Translate" ? "translate_desc" : "Translate";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var dict = from s in db.Dictionaries select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                dict = dict.Where(s => s.ForeignWord.Contains(searchString)
                                       || s.Translate.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "foreignword_desc":
                    dict = dict.OrderByDescending(s => s.ForeignWord);
                    break;
                case "Translate":
                    dict = dict.OrderBy(s => s.Translate);
                    break;
                case "translate_desc":
                    dict = dict.OrderByDescending(s => s.Translate);
                    break;
                case "genderkind_desc":
                    dict = dict.OrderByDescending(s => s.GenderKind);
                    break;
                default:
                     dict = dict.OrderBy(s => s.ForeignWord);
                    break;
            }
            const int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(dict.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Export()
        {


            StringBuilder sb = new StringBuilder();
            //static file name, can be changes as per requirement
            string sFileName = DateTime.Now + "Dictionary";
            string type = ".xls";           
            //Bind data list from edmx
            var Data = db.Dictionaries.Take(20000).ToList();
            if (Data != null && Data.Any())
            {
                sb.Append("<table style='1px solid black; font-size:12px;'>");
                sb.Append("<tr>");
                sb.Append("<td style='width:120px;'><b>ForeignWord</b></td>");
                sb.Append("<td style='width:300px;'><b>Pronunciation</b></td>");
                sb.Append("<td style='width:120px;'><b>Translate</b></td>");
                sb.Append("<td style='width:300px;'><b>Example</b></td>");
                sb.Append("<td style='width:120px;'><b>Gender-Kind</b></td>");
                sb.Append("<td style='width:300px;'><b>Additional</b></td>");
                sb.Append("</tr>");

                foreach (var result in Data)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + result.ForeignWord + "</td>");
                    sb.Append("<td>" + result.Pronunciation + "</td>");
                    sb.Append("<td>" + result.Translate + "</td>");
                    sb.Append("<td>" + result.Example + "</td>");
                    sb.Append("<td>" + result.GenderKind + "</td>");
                    sb.Append("<td>" + result.Additional + "</td>");
                    sb.Append("</tr>");
                }
            }
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName + type);
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }

        [HttpPost]
        public ActionResult Index(Dictionary model)
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult Statistics()
        {
            var statsModel = new Dictionary();
            statsModel.Id = (db.Dictionaries).Count();
            return View(statsModel);
        }
        // GET: Dictionaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary dictionary = db.Dictionaries.Find(id);
            if (dictionary == null)
            {
                return HttpNotFound();
            }
            return View(dictionary);
        }

        // GET: Dictionaries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dictionaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ForeignWord,Pronunciation,Translate,Example,GenderKind,Additional")] Dictionary dictionary)
        {
            if (ModelState.IsValid)
            {
                db.Dictionaries.Add(dictionary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dictionary);
        }

        // GET: Dictionaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary dictionary = db.Dictionaries.Find(id);
            if (dictionary == null)
            {
                return HttpNotFound();
            }
            return View(dictionary);
        }

        // POST: Dictionaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ForeignWord,Pronunciation,Translate,Example,GenderKind,Additional")] Dictionary dictionary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dictionary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dictionary);
        }

        // GET: Dictionaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dictionary dictionary = db.Dictionaries.Find(id);
            if (dictionary == null)
            {
                return HttpNotFound();
            }
            return View(dictionary);
        }

        // POST: Dictionaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dictionary dictionary = db.Dictionaries.Find(id);
            db.Dictionaries.Remove(dictionary);
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
