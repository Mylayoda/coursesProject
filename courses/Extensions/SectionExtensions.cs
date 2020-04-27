using courses.Comparers;
using courses.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace courses.Extensions
{
    public static class SectionExtensions
    {
        public static async Task<CourseSectionModel> GetCourseSectionsAsync(
        int courseId, string userId)
        {
            var db = ApplicationDbContext.Create();

            var sections = await (
                from p in db.Courses
                join pi in db.courseModules on p.Id equals pi.CourseId
                join i in db.Modules on pi.ModuleId equals i.Id
                join s in db.Sections on i.SectionId equals s.Id
                where p.Id.Equals(courseId)
                orderby s.Title
                select new CourseSection
                {
                    Id = s.Id,
                    ModuleTypeId = i.ModuleTypeId,
                    Title = s.Title
                }).ToListAsync();

            var result = sections.Distinct(new CourseSectionEqualityComparer()).ToList();

            var union = result.Where(r => !r.Title.ToLower().Contains("download"))
                        .Union(result.Where(r => r.Title.ToLower().Contains("download")));

            var model = new CourseSectionModel
            {
                Sections = union.ToList(),
                Title = await (from p in db.Courses
                               where p.Id.Equals(courseId)
                               select p.Title).FirstOrDefaultAsync()
            };

            return model;
        }
    }
}