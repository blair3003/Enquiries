using System.ComponentModel.DataAnnotations;

namespace Enquiries.Models
{
    public class Enquiry
    {
        [Key]
        public int EnquiryId { get; set; }
        public int? MediaId { get; set; }
        public int? ReporterId { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsArchived { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Media? Media { get; set; }
        public virtual Reporter? Reporter { get; set; }
    }
}
