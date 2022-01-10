using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sasarman_Andra_Proiect.Models
{
    public class PublishedCourse
    {
        public int DomainID { get; set; }
        public int CourseID { get; set; }
        public Domain Domain { get; set; }
        public Course Course { get; set; }
    }
}
