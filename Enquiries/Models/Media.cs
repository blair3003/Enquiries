using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;

namespace Enquiries.Models
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }
        public string? Name { get; set; }
        public MediaType? Type { get; set; }  // Enum for media type

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
