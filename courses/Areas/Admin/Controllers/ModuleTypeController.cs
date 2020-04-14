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
    public class ModuleTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ModuleType
        public async Task<ActionResult> Index()
        {
            return View(await db.ModuleTypes.ToListAsync());
        }

        // GET: Admin/ModuleType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleType moduleType = await db.ModuleTypes.FindAsync(id);
            if (moduleType == null)
            {
                return HttpNotFound();
            }
            return View(moduleType);
        }

        // GET: Admin/ModuleType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ModuleType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title")] ModuleType moduleType)
        {
            if (ModelState.IsValid)
            {
                db.ModuleTypes.Add(moduleType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(moduleType);
        }

        // GET: Admin/ModuleType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleType moduleType = await db.ModuleTypes.FindAsync(id);
            if (moduleType == null)
            {
                return HttpNotFound();
            }
            return View(moduleType);
        }

        // POST: Admin/ModuleType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title")] ModuleType moduleType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moduleType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(moduleType);
        }

        // GET: Admin/ModuleType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleType moduleType = await db.ModuleTypes.FindAsync(id);
            if (moduleType == null)
            {
                return HttpNotFound();
            }
            return View(moduleType);
        }

        // POST: Admin/ModuleType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ModuleType moduleType = await db.ModuleTypes.FindAsync(id);
            db.ModuleTypes.Remove(moduleType);
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
