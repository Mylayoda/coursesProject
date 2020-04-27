using courses.Comparers;
using courses.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace courses.Extensions
{
    public static class ThumbnailExtensions
    {
        private static async Task<List<int>> GetSubscriptionIdsAsync(
            string userId = null, ApplicationDbContext db = null)
        {
            try
            {
                if (userId == null) return new List<int>();
                if (db == null) db = ApplicationDbContext.Create();

                return await (
                    from us in db.StudentSubscriptions
                    where us.UserId.Equals(userId)
                    select us.SubscriptionId).ToListAsync();
            }
            catch { }

            return new List<int>();
        }

        public static async Task<IEnumerable<ThumbnailModel>> GetCourseThumbnailsAsync(
        this List<ThumbnailModel> thumbnails, string userId = null,
        ApplicationDbContext db = null)
        {
            try
            {
                if (userId == null) return new List<ThumbnailModel>();
                if (db == null) db = ApplicationDbContext.Create();

                var subscriptionIds = await GetSubscriptionIdsAsync(userId, db);

                thumbnails = await (
                    from ps in db.CourseSubscriptions
                    join p in db.Courses on ps.CourseId equals p.Id
                    join plt in db.CourseLinkTexts on p.CourseLinkTextId equals plt.Id
                    //join pt in db.courseModules on p.CourseModuleId equals pt.Id
                    where subscriptionIds.Contains(ps.SubscriptionId)
                    select new ThumbnailModel
                    {
                        CourseId = p.Id,
                        SubscriptionId = ps.SubscriptionId,
                        Title = p.Title,
                        Description = p.Description,
                        ImageUrl = p.ImageUrl,
                        Link = "/CourseContent/Index/" + p.Id,
                        TagText = plt.Title,
                        //ContentTag = pt.Title
                    }).ToListAsync();

            }
            catch { }
            return thumbnails.Distinct(new ThumbnailEqualityComparer()).OrderBy(o => o.Title);
        }

    }
}