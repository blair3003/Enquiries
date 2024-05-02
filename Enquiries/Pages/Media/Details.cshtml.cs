using MediaModel = Enquiries.Models.Media;
using Enquiries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enquiries.Pages.Media
{
    public class DetailsModel : PageModel
    {
        private readonly MediaService _mediaService;

        public DetailsModel(MediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [BindProperty(SupportsGet = true)]
        public int MediaId { get; set; }
        public MediaModel? Media { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Media = await _mediaService.GetMediaByIdAsync(MediaId);

            if (Media == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
