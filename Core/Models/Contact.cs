using System.ComponentModel.DataAnnotations;

namespace vega.Core.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set;}
        [Required]
        [StringLength(255)]
        public string Phone { get; set; }
        [Required]
        [StringLength(25)]
        public string Email { get; set; }
    }
}