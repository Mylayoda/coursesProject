using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace courses.Entities
{
    [Table("Module")]
    public class Module
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        [MaxLength(1024)]
        public string Url { get; set; }
        [MaxLength(1024)]
        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }
        [AllowHtml]
        public string HTML { get; set; }
        [DefaultValue(0)]
        [DisplayName("Wait Days")]
        public int WaitDays { get; set; }
        public string HTMLShort
        {
            get
            {
                return HTML == null || HTML.Length < 50 ?
                  HTML : HTML.Substring(0, 50);
            }
        }
        public int CourseId { get; set; }
        public int ModuleTypeId { get; set; }
        public int SectionId { get; set; }
        public int PartId { get; set; }
        public bool IsFree { get; set; }
        [DisplayName("Module Types")]
        public ICollection<ModuleType> ModuleTypes { get; set; }
        [DisplayName("Sections")]
        public ICollection<Section> Sections { get; set; }
        [DisplayName("Parts")]
        public ICollection<Part> Parts { get; set; }
    }
}