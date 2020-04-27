using courses.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace courses.Areas.Admin.Models
{
    public class CourseModuleModel
    {
        [DisplayName("Course Id")]
        public int CourseId { get; set; }
        [DisplayName("Module Id")]
        public int ModuleId { get; set; }
        [DisplayName("Course Title")]
        public string CourseTitle { get; set; }
        [DisplayName("Module Title")]
        public string ModuleTitle { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Module> Modules { get; set; }
    }
}