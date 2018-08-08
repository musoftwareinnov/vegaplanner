using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using vega.Core.Models.Generic;

namespace vega.Core.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set;}
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        [Required]
        [StringLength(255)]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        [StringLength(255)]
        public string Postcode { get; set; }
        [Required]
        [StringLength(255)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(255)]
        public string TelephoneHome { get; set; }

        [Required]
        [StringLength(255)]
        public string TelephoneMobile { get; set; }

        public string SearchCriteria { get; set; }

        [StringLength(1024)]
        public string Notes { get; set; }
        public ICollection<PlanningApp> planningApps { get; set; }
    }
}