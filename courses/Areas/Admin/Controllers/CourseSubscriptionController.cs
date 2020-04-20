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
using courses.Areas.Admin.Extension;
using courses.Areas.Admin.Models;

namespace courses.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseSubscriptionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CourseSubscription
        public async Task<ActionResult> Index()
        {
            return View(await db.CourseSubscriptions.Convert(db));
        }

        // GET: Admin/CourseSubscription/Details/5
        public async Task<ActionResult> Details(int? SubscriptionId, int? CourseId)
        {
            if (SubscriptionId == null || CourseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSubscription courseSubscription = 
                await GetCourseSubscription(SubscriptionId, CourseId);
            if (courseSubscription == null)
            {
                return HttpNotFound();
            }
            return View(await courseSubscription.Convert(db));
        }

        // GET: Admin/CourseSubscription/Create
        public async Task<ActionResult> Create()
        {
            var model = new CourseSubscriptionModel
            {
                Subscriptions = await db.Subscriptions.ToListAsync(),
                courses = await db.Courses.ToListAsync()
            };
            return View(model);
        }

        // POST: Admin/CourseSubscription/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseId,SubscriptionId")] CourseSubscription courseSubscription)
        {
            if (ModelState.IsValid)
            {
                db.CourseSubscriptions.Add(courseSubscription);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(courseSubscription);
        }

        // GET: Admin/CourseSubscription/Edit/5
        public async Task<ActionResult> Edit(int? subscriptionId, int? courseId)
        {
            if (subscriptionId == null || courseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSubscription courseSubscription = await GetCourseSubscription(subscriptionId, courseId);
            if (courseSubscription == null)
            {
                return HttpNotFound();
            }
            return View(await courseSubscription.Convert(db));
        }

        // POST: Admin/CourseSubscription/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CourseId,SubscriptionId,OldCourseId,OldSubscriptionId")] CourseSubscription courseSubscription)
        {
            if (ModelState.IsValid)
            {
                var canChange = await courseSubscription.CanChange(db);
                if (canChange)
                    await courseSubscription.Change(db);
                return RedirectToAction("Index");
            }
            return View(courseSubscription);
        }

        // GET: Admin/CourseSubscription/Delete/5
        public async Task<ActionResult> Delete(int? subscriptionId, int? courseId)
        {
            if (subscriptionId == null || courseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSubscription courseSubscription = await GetCourseSubscription(subscriptionId,courseId);
            if (courseSubscription == null)
            {
                return HttpNotFound();
            }
            return View(await courseSubscription.Convert(db));
        }

        // POST: Admin/CourseSubscription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int subscriptionId, int courseId)
        {
            CourseSubscription courseSubscription = await GetCourseSubscription(subscriptionId, courseId);
            db.CourseSubscriptions.Remove(courseSubscription);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<CourseSubscription> GetCourseSubscription(int? SubscriptionId, int? courseId)
        {
            try
            {
                int SubpId = 0, cosId = 0;
                int.TryParse(SubscriptionId.ToString(), out SubpId);
                int.TryParse(courseId.ToString(), out cosId);
                var courseSubscription = await db.CourseSubscriptions.FirstOrDefaultAsync(
                    pi => pi.CourseId.Equals(cosId) && pi.SubscriptionId.Equals(SubpId));
                return courseSubscription;
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
