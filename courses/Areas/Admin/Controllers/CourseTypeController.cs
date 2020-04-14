using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using courses.Entities;
using courses.Models;

namespace courses.Areas.Admin.Controllers
{
    public class CourseTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CourseType
        public async Task<ActionResult> Index()
        {
            return View(await db.CourseTypes.ToListAsync());
        }

        // GET: Admin/CourseType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseType courseType = await db.CourseTypes.FindAsync(id);
            if (courseType == null)
            {
                return HttpNotFound();
            }
            return View(courseType);
        }

        // GET: Admin/CourseType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CourseType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title")] CourseType courseType)
        {
            if (ModelState.IsValid)
            {
                db.CourseTypes.Add(courseType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(courseType);
        }

        // GET: Admin/CourseType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseType courseType = await db.CourseTypes.FindAsync(id);
            if (courseType == null)
            {
                return HttpNotFound();
            }
            return View(courseType);
        }

        // POST: Admin/CourseType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title")] CourseType courseType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(courseType);
        }

        // GET: Admin/CourseType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseType courseType = await db.CourseTypes.FindAsync(id);
            if (courseType == null)
            {
                return HttpNotFound();
            }
            return View(courseType);
        }

        // POST: Admin/CourseType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CourseType courseType = await db.CourseTypes.FindAsync(id);
            db.CourseTypes.Remove(courseType);
            await db.SaveChangesAsync();
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
