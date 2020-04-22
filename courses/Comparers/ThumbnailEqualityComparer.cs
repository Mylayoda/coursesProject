using courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace courses.Comparers
{

    public class ThumbnailEqualityComparer : IEqualityComparer<ThumbnailModel>
    {
        public bool Equals(ThumbnailModel thumb1, ThumbnailModel thumb2)
        {
            return thumb1.CourseId.Equals(thumb2.CourseId);
        }

        public int GetHashCode(ThumbnailModel thumb)
        {
            return thumb.CourseId;
        }
    }
}