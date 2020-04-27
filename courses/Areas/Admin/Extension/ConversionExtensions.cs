using courses.Areas.Admin.Models;
using courses.Entities;
using courses.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace courses.Areas.Admin.Extension
{
    public static class ConversionExtensions
    {
        #region Course
        public static async Task<IEnumerable<CourseModel>> Convert(
            this IEnumerable<Course> courses, ApplicationDbContext db)
        {
            if (courses.Count().Equals(0))
                return new List<CourseModel>();

            var texts = await db.CourseLinkTexts.ToListAsync();
            var types = await db.CourseTypes.ToListAsync();

            return from p in courses
                   select new CourseModel
                   {
                       Id = p.Id,
                       Title = p.Title,
                       Description = p.Description,
                       ImageUrl = p.ImageUrl,
                       CourseLinkTextId = p.CourseLinkTextId,
                       CourseTypeId = p.CourseTypeId,
                       CourseLinkTexts = texts,
                       CourseTypes = types
                   };
        }

        public static async Task<CourseModel> Convert(
        this Course courses, ApplicationDbContext db)
        {
            var text = await db.CourseLinkTexts.FirstOrDefaultAsync(
                p => p.Id.Equals(courses.CourseLinkTextId));
            var type = await db.CourseTypes.FirstOrDefaultAsync(
                p => p.Id.Equals(courses.CourseTypeId));

            var model = new CourseModel
            {
                Id = courses.Id,
                Title = courses.Title,
                Description = courses.Description,
                ImageUrl = courses.ImageUrl,
                CourseLinkTextId = courses.CourseLinkTextId,
                CourseTypeId = courses.CourseTypeId,
                CourseLinkTexts = new List<CourseLinkText>(),
                CourseTypes = new List<CourseType>()
            };

            model.CourseLinkTexts.Add(text);
            model.CourseTypes.Add(type);

            return model;
        }
        #endregion

        #region Course Module
        public static async Task<IEnumerable<CourseModuleModel>> Convert(
       this IQueryable<CourseModule> courseModules, ApplicationDbContext db)
        {
            if (courseModules.Count().Equals(0))
                return new List<CourseModuleModel>();

            return await (from pi in courseModules
                          select new CourseModuleModel
                          {
                              ModuleId = pi.ModuleId,
                              CourseId = pi.CourseId,
                              ModuleTitle = db.Modules.FirstOrDefault(
                                  i => i.Id.Equals(pi.ModuleId)).Title,
                              CourseTitle = db.Courses.FirstOrDefault(
                                  p => p.Id.Equals(pi.CourseId)).Title
                          }).ToListAsync();
        }

        public static async Task<CourseModuleModel> Convert(
        this CourseModule courseModule, ApplicationDbContext db, bool addListData = true)
        {


            var model = new CourseModuleModel
            {
                ModuleId = courseModule.ModuleId,
                CourseId = courseModule.CourseId,
                Modules = addListData ? await db.Modules.ToListAsync() : null,
            Courses = addListData ? await db.Courses.ToListAsync() : null,
            ModuleTitle =(await db.Modules.FirstOrDefaultAsync(i =>i.Id.Equals(courseModule.ModuleId))).Title,
            CourseTitle = (await db.Courses.FirstOrDefaultAsync(p=>p.Id.Equals(courseModule.CourseId))).Title
            };
            return model;
        }

        public static async Task<bool> CanChange(
            this CourseModule courseModule, ApplicationDbContext db)
        {
            var oldPI = await db.courseModules.CountAsync(pi =>
                pi.CourseId.Equals(courseModule.OldCourseId) &&
                pi.ModuleId.Equals(courseModule.OldModuleId));

            var newPI = await db.courseModules.CountAsync(pi =>
                pi.CourseId.Equals(courseModule.CourseId) &&
                pi.ModuleId.Equals(courseModule.ModuleId));

            return oldPI.Equals(1) && newPI.Equals(0);
        }

        public static async Task Change(
            this CourseModule courseModule, ApplicationDbContext db)
        {
            var oldCourseModule = await db.courseModules.FirstOrDefaultAsync(
                    pi => pi.CourseId.Equals(courseModule.OldCourseId) &&
                    pi.ModuleId.Equals(courseModule.OldModuleId));

            var newCourseModule = await db.courseModules.FirstOrDefaultAsync(
                pi => pi.CourseId.Equals(courseModule.CourseId) &&
                pi.ModuleId.Equals(courseModule.ModuleId));

            if (oldCourseModule != null && newCourseModule == null)
            {
                newCourseModule = new CourseModule
                {
                    ModuleId = courseModule.ModuleId,
                    CourseId = courseModule.CourseId
                };

                using (var transaction = new TransactionScope(
                    TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        db.courseModules.Remove(oldCourseModule);
                        db.courseModules.Add(newCourseModule);

                        await db.SaveChangesAsync();
                        transaction.Complete();
                    }
                    catch { transaction.Dispose(); }
                }
            }
        }
        #endregion

        #region Course Subscription
        public static async Task<IEnumerable<CourseSubscriptionModel>> Convert(
        this IQueryable<CourseSubscription> courseSubscriptions, ApplicationDbContext db)
        {
            if (courseSubscriptions.Count().Equals(0))
                return new List<CourseSubscriptionModel>();

            return await (from pi in courseSubscriptions
                          select new CourseSubscriptionModel
                          {
                              SubscriptionId = pi.SubscriptionId,
                              CourseId = pi.CourseId,
                              SubscriptionTitle = db.Subscriptions.FirstOrDefault(
                                  i => i.Id.Equals(pi.SubscriptionId)).Title,
                              CourseTitle = db.Courses.FirstOrDefault(
                                  p => p.Id.Equals(pi.CourseId)).Title
                          }).ToListAsync();
        }

        public static async Task<CourseSubscriptionModel> Convert(
        this CourseSubscription courseSubscription,
        ApplicationDbContext db, bool addListData = true)
        {
            var model = new CourseSubscriptionModel
            {
                SubscriptionId = courseSubscription.SubscriptionId,
                CourseId = courseSubscription.CourseId,
                Subscriptions = addListData ? await db.Subscriptions.ToListAsync() : null,
                courses = addListData ? await db.Courses.ToListAsync() : null,
                SubscriptionTitle = (await db.Subscriptions.FirstOrDefaultAsync(s =>
                   s.Id.Equals(courseSubscription.SubscriptionId))).Title,
                CourseTitle = (await db.Courses.FirstOrDefaultAsync(p =>
                   p.Id.Equals(courseSubscription.CourseId))).Title
            };

            return model;
        }

        public static async Task<bool> CanChange(
            this CourseSubscription courseSubscriptions,
            ApplicationDbContext db)
        {
            var oldCS = await db.CourseSubscriptions.CountAsync(sp =>
                sp.CourseId.Equals(courseSubscriptions.OldCourseId) &&
                sp.SubscriptionId.Equals(courseSubscriptions.OldSubscriptionId));

            var newCS = await db.CourseSubscriptions.CountAsync(sp =>
                sp.CourseId.Equals(courseSubscriptions.CourseId) &&
                sp.SubscriptionId.Equals(courseSubscriptions.SubscriptionId));

            return oldCS.Equals(1) && newCS.Equals(0);
        }

        public static async Task Change(
            this CourseSubscription courseSubscriptions,
            ApplicationDbContext db)
        {
            var oldCourseSubscription = await db.CourseSubscriptions.FirstOrDefaultAsync(
                    sp => sp.CourseId.Equals(courseSubscriptions.OldCourseId) &&
                    sp.SubscriptionId.Equals(courseSubscriptions.OldSubscriptionId));

            var newCourseSubscription = await db.CourseSubscriptions.FirstOrDefaultAsync(
                sp => sp.CourseId.Equals(courseSubscriptions.CourseId) &&
                sp.SubscriptionId.Equals(courseSubscriptions.SubscriptionId));

            if (oldCourseSubscription != null && newCourseSubscription == null)
            {
                newCourseSubscription = new CourseSubscription
                {
                    SubscriptionId = courseSubscriptions.SubscriptionId,
                    CourseId = courseSubscriptions.CourseId
                };

                using (var transaction = new TransactionScope(
                    TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        db.CourseSubscriptions.Remove(oldCourseSubscription);
                        db.CourseSubscriptions.Add(newCourseSubscription);

                        await db.SaveChangesAsync();
                        transaction.Complete();
                    }
                    catch { transaction.Dispose(); }
                }
            }
        }
        #endregion

    }
}