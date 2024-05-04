using Enquiries.Models;
using System.ComponentModel.DataAnnotations;

namespace Enquiries.Pages.Media
{
    public class MediaViewModel
    {
        [Required(ErrorMessage = "The name is required.")]
        [StringLength(100, ErrorMessage = "The name must not exceed 100 characters.")]
        public string? Name { get; set; }

        [EnumDataType(typeof(MediaType), ErrorMessage = "Invalid media type.")]
        public MediaType? Type { get; set; }
    }
}
