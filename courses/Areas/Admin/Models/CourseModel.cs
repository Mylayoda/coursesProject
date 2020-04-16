using courses.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace courses.Areas.Admin.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        [MaxLength(1024)]
        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }
        public int CourseLinkTextId { get; set; }
        public int CourseTypeId { get; set; }
        [DisplayName("Course Link Text")]
        public ICollection<CourseLinkText> CourseLinkTexts { get; set; }
        [DisplayName("Course Type")]
        public ICollection<CourseType> CourseTypes { get; set; }
        public string CourseType
        {
            get
            {
                return CourseTypes == null || CourseTypes.Count.Equals(0) ?
                    String.Empty : CourseTypes.First(
                        pt => pt.Id.Equals(CourseTypeId)).Title;
            }
        }
        public string CourseLinkText
        {
            get
            {
                return CourseLinkTexts == null ||
                    CourseLinkTexts.Count.Equals(0) ?
                    String.Empty : CourseLinkTexts.First(
                        pt => pt.Id.Equals(CourseLinkTextId)).Title;
            }
        }
    }
}