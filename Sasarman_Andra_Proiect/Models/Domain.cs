using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sasarman_Andra_Proiect.Models
{
    public class Domain
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Domain Name")]
        [StringLength(50)]
        public string DomainName { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        public ICollection<PublishedCourse> PublishedCourses { get; set; }
    }
}
