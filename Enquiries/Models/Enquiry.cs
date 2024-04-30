using System.ComponentModel.DataAnnotations;

namespace Enquiries.Models
{
    public class Enquiry
    {
        [Key]
        public int EnquiryId { get; set; }

        [Required]
        public int MediaId { get; set; }

        [Required]
        public int ReporterId { get; set; }

        [Required]
        [StringLength(255)]
        public string? Subject { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public bool IsArchived { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Media? Media { get; set; }
        public virtual Reporter? Reporter { get; set; }
    }
}
