using MediaModel = Enquiries.Models.Media;
using Enquiries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enquiries.Pages.Media
{
    public class IndexModel : PageModel
    {
        private readonly MediaService _mediaService;

        public IndexModel(MediaService mediaService)
        {
            _mediaService = mediaService;
        }

        public List<MediaModel> Media { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            Media = await _mediaService.GetAllMediaAsync();

            return Page();
        }
    }
}
