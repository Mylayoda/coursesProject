using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace courses.Entities
{
    [Table("CourseSubscription")]
    public class CourseSubscription
    {
        [Required]
        [Key, Column(Order = 1)]
        public int CourseId { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int SubscriptionId { get; set; }
        [NotMapped]
        public int OldCourseId { get; set; }
        [NotMapped]
        public int OldSubscriptionId { get; set; }

    }
}