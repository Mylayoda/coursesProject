using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace courses.Models
{
    public class CourseSectionModel
    {
        public string Title { get; set; }
        public List<CourseSection> Sections { get; set; }
    }
}