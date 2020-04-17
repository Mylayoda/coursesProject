using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace courses.Areas.Admin.Models
{
    public class EditButtonModel
    {
        
        public int ModuleId { get; set; }
        public int CourseId { get; set; }
        public int SubscriptionId { get; set; }
        public string Link
        {
            get
            {
                var s = new StringBuilder("?");
                if (ModuleId > 0) s.Append(String.Format("{0}={1}&", "moduleId", ModuleId));
                if (CourseId > 0) s.Append(String.Format("{0}={1}&", "courseId", CourseId));
                if (SubscriptionId > 0) s.Append(String.Format("{0}={1}&", "subscriptionId", SubscriptionId));
                return s.ToString().Substring(0, s.Length - 1);
            }
        }
    }
}
