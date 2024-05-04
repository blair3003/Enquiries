using System.ComponentModel.DataAnnotations;

namespace Enquiries.Pages.Reporters
{
    public class ReporterViewModel
    {
        [Required(ErrorMessage = "The name is required.")]
        [StringLength(100, ErrorMessage = "The name must not exceed 100 characters.")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid Telephone Number")]
        public string? Tel { get; set; }

        [Phone(ErrorMessage = "Invalid Mobile Number")]
        public string? Mobile { get; set; }

        public int? MediaId { get; set; }

        public bool IsActive { get; set; }
    }
}
