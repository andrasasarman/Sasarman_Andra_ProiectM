using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sasarman_Andra_Proiect.Models.LibraryViewModels
{
    public class DomainIndexData
    {
        public IEnumerable<Domain> Domains { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
