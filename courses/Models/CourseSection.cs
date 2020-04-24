using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace courses.Models
{
    public class CourseSection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ModuleTypeId { get; set; }
        public IEnumerable<CourseModuleRow> Items { get; set; }
    }
}