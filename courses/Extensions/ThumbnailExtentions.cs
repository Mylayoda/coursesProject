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
                    from ss in db.StudentSubscriptions
                    where ss.UserId.Equals(userId)
                    select ss.SubscriptionId).ToListAsync();
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
                    from cs in db.CourseSubscriptions
                    join c in db.Courses on cs.CourseId equals c.Id
                    join plt in db.CourseLinkText on c.CourseLinkTextId equals plt.Id
                    //join pt in db.CourseModules on c.CourseModuleId equals pt.Id
                    where subscriptionIds.Contains(cs.SubscriptionId)
                    select new ThumbnailModel
                    {
                        CourseId = c.Id,
                        SubscriptionId = cs.SubscriptionId,
                        Title = c.Title,
                        Description = c.Description,
                        ImageUrl = c.ImageUrl,
                        Link = "/CourseContent/Index/" + c.Id,
                        TagText = plt.Title,
                        //ContentTag = pt.Title
                    }).ToListAsync();

            }
            catch { }
            return thumbnails.Distinct(new ThumbnailEqualityComparer()).OrderBy(o => o.Title);
        }

    }
}