using System.ComponentModel.DataAnnotations;

namespace Enquiries.Models
{
    public class Reporter
    {
        [Key]
        public int ReporterId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Tel { get; set; }

        public string? Mobile { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Enquiry>? Enquiries { get; set; }
        public virtual Media? Media { get; set; }
    }
}
