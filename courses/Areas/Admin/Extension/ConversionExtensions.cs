using courses.Areas.Admin.Models;
using courses.Entities;
using courses.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;



namespace courses.Areas.Admin.Extension
{
    public static class ConversionExtensions
    {

        public static async Task<IEnumerable<CourseModel>> Convert(
            this IEnumerable<Course> course, ApplicationDbContext db)
        {
            if (course.Count().Equals(0))
                return new List<CourseModel>();

            var texts = await db.CourseLinkText.ToListAsync();
            var types = await db.CourseTypes.ToListAsync();

            return from p in course
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
        this Course course, ApplicationDbContext db)
        {
            var text = await db.CourseLinkText.FirstOrDefaultAsync(
                p => p.Id.Equals(course.CourseLinkTextId));
            var type = await db.CourseTypes.FirstOrDefaultAsync(
                p => p.Id.Equals(course.CourseTypeId));

            var model = new CourseModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                ImageUrl = course.ImageUrl,
                CourseLinkTextId = course.CourseLinkTextId,
                CourseTypeId = course.CourseTypeId,
                CourseLinkTexts = new List<CourseLinkText>(),
                CourseTypes = new List<CourseType>()
            };

            model.CourseLinkTexts.Add(text);
            model.CourseTypes.Add(type);

            return model;
        }

    }
}