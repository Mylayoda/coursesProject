using courses.Models;
using System.Collections.Generic;


namespace courses.Comparers
{
    public class CourseSectionEqualityComparer : IEqualityComparer<CourseSection>
    {
        public bool Equals(CourseSection section1, CourseSection section2)
        {
            return section1.Id.Equals(section2.Id);
        }

        public int GetHashCode(CourseSection section)
        {
            return (section.Id).GetHashCode();
        }
    }
}