using courses.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace courses.Areas.Admin.Models
{
    public class CourseSubscriptionModel
    {
        [DisplayName("Course Id")]
        public int CourseId { get; set; }
        [DisplayName("Subscription Id")]
        public int SubscriptionId { get; set; }
        [DisplayName("Course Title")]
        public string CourseTitle { get; set; }
        [DisplayName("Subscription Title")]
        public string SubscriptionTitle { get; set; }
        public ICollection<Course> courses { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}