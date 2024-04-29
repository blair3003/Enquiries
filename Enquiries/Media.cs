using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;

namespace Enquiries
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        public MediaType Type { get; set; }  // Enum for media type

        // Navigation properties
        public virtual ICollection<Enquiry>? Enquiries { get; set; }
        public virtual ICollection<Reporter>? Reporters { get; set; }
    }

    public enum MediaType
    {
        Local,
        National,
        Other
    }
}
