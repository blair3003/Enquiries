using System.ComponentModel.DataAnnotations;

namespace Enquiries.Pages.Enquiries
{
    public class EnquiryViewModel
    {
        [Required(ErrorMessage = "The subject is required.")]
        [StringLength(255, ErrorMessage = "The subject must not exceed 255 characters.")]
        public string? Subject { get; set; }

        [StringLength(1000, ErrorMessage = "The description must not exceed 1000 characters.")]
        public string? Description { get; set; }

        public DateTime? Deadline { get; set; }

        public bool IsArchived { get; set; }

        public int? ReporterId { get; set; }

        public int? MediaId { get; set; }
    }
}
