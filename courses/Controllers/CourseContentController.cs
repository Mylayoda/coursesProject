using courses.Extensions;
using courses.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace courses.Controllers
{
    [Authorize]
    public class CourseContentController : Controller
    {
        public async Task<ActionResult> Index(int id)
        {
            var model = new CourseSectionModel
            {
                Title = "The Title",
                Sections = new List<CourseSection>()
            };
            var userId = Request.IsAuthenticated ? HttpContext.GetUserId() : null;
            var sections = await SectionExtensions.GetCourseSectionsAsync(id, userId);

            return View(model);
        }



    }
}