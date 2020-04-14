using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace courses.Areas.Admin.Models
{
    public class ButtonModuleSmall
    {
        public string Action { get; set; }
        public string Text { get; set; }
        public string Glyph { get; set; }
        public string ButtonType { get; set; }
        public int? Id { get; set; }
        public int? ModuleId { get; set; }
        public int? CourseId { get; set; }
        public int? SubscriptionId { get; set; }
        public string UserId { get; set; }

        public string ActionParameters
        {
            get
            {
                var param = new StringBuilder("?");
                if (Id != null && Id > 0)
                    param.Append(String.Format("{0}={1}&", "id", Id));

                if (ModuleId != null && ModuleId > 0)
                    param.Append(String.Format("{0}={1}&", "moduleId", ModuleId));

                if (CourseId != null && CourseId > 0)
                    param.Append(String.Format("{0}={1}&", "courseId", CourseId));

                if (SubscriptionId != null && SubscriptionId > 0)
                    param.Append(String.Format("{0}={1}&", "subscriptionId", SubscriptionId));

                if (UserId != null && !UserId.Equals(string.Empty))
                    param.Append(string.Format("{0}={1}&", "userId", UserId));

                return param.ToString().Substring(0, param.Length - 1);
            }
        }

    }
}