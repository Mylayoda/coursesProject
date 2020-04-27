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
    [Authorize(Roles = "Admin")]
    public class CourseLinkTextController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CourseLinkText
        public async Task<ActionResult> Index()
        {
            return View(await db.CourseLinkTexts.ToListAsync());
        }

        // GET: Admin/CourseLinkText/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLinkText courseLinkText = await db.CourseLinkTexts.FindAsync(id);
            if (courseLinkText == null)
            {
                return HttpNotFound();
            }
            return View(courseLinkText);
        }

        // GET: Admin/CourseLinkText/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CourseLinkText/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title")] CourseLinkText courseLinkText)
        {
            if (ModelState.IsValid)
            {
                db.CourseLinkTexts.Add(courseLinkText);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(courseLinkText);
        }

        // GET: Admin/CourseLinkText/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLinkText courseLinkText = await db.CourseLinkTexts.FindAsync(id);
            if (courseLinkText == null)
            {
                return HttpNotFound();
            }
            return View(courseLinkText);
        }

        // POST: Admin/CourseLinkText/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title")] CourseLinkText courseLinkText)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseLinkText).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(courseLinkText);
        }

        // GET: Admin/CourseLinkText/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLinkText courseLinkText = await db.CourseLinkTexts.FindAsync(id);
            if (courseLinkText == null)
            {
                return HttpNotFound();
            }
            return View(courseLinkText);
        }

        // POST: Admin/CourseLinkText/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CourseLinkText courseLinkText = await db.CourseLinkTexts.FindAsync(id);
            db.CourseLinkTexts.Remove(courseLinkText);
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
