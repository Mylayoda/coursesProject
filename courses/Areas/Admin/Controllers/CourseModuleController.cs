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
using courses.Areas.Admin.Models;
using courses.Areas.Admin.Extension;

namespace courses.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseModuleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CourseModule
        public async Task<ActionResult> Index()
        {
            return View(await db.courseModules.Convert(db));
        }

        // GET: Admin/CourseModule/Details/5
        public async Task<ActionResult> Details(int? moduleId, int? courseId)
        {
            if (moduleId == null || courseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModule courseModule = await GetCourseModule(moduleId, courseId);
            if (courseModule == null)
            {
                return HttpNotFound();
            }
            return View(await courseModule. Convert(db));
        }

        // GET: Admin/CourseModule/Create
        public async Task<ActionResult> Create()
        {
            var model = new CourseModuleModel
            {
                Modules = await db.Modules.ToListAsync(),
                Courses = await db.Courses.ToListAsync()
            };
            return View(model);
        }

        // POST: Admin/CourseModule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "CourseId,ModuleId")] CourseModule courseModule)
        {
            if (ModelState.IsValid)
            {
                db.courseModules.Add(courseModule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(courseModule);
        }

        // GET: Admin/CourseModule/Edit/5
        public async Task<ActionResult> Edit(int? moduleId, int? courseId)
        {
            if (moduleId == null || courseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModule courseModule = await GetCourseModule(moduleId, courseId);
            if (courseModule == null)
            {
                return HttpNotFound();
            }
            return View(await courseModule.Convert(db));
        }

        // POST: Admin/CourseModule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "CourseId,ModuleId,OldCourseId,OldModuleId")] 
        CourseModule courseModule)
        {
            if (ModelState.IsValid)
            {
                var canChange = await courseModule.CanChange(db);
                if (canChange)
                    await courseModule.Change(db);

                return RedirectToAction("Index");
            }
            return View(courseModule);
        }

        // GET: Admin/CourseModule/Delete/5
        public async Task<ActionResult> Delete(int? moduleId, int? courseId)
        {
            if (moduleId == null || courseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModule courseModule = await GetCourseModule(moduleId, courseId);
            if (courseModule == null)
            {
                return HttpNotFound();
            }
            return View(await courseModule.Convert(db));
        }

        // POST: Admin/CourseModule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int moduleId, int courseId)
        {
            CourseModule courseModule = await GetCourseModule(moduleId, courseId);
            db.courseModules.Remove(courseModule);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<CourseModule> GetCourseModule(int? moduleId,int? courseId)
        {
            try 
            {
                int modId = 0, cosId = 0;
                int.TryParse(moduleId.ToString(), out modId);
                int.TryParse(courseId.ToString(), out cosId);
                var courseModule = await db.courseModules.FirstOrDefaultAsync(
                    pi => pi.CourseId.Equals(cosId) && pi.ModuleId.Equals(modId));
                return courseModule;
            }
            catch { return null; }
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
