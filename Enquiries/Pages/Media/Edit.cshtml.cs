using Enquiries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enquiries.Pages.Media
{
    public class EditModel : PageModel
    {
        private readonly MediaService _mediaService;

        public EditModel(MediaService mediaService)
        {
            _mediaService = mediaService;

        }

        [BindProperty(SupportsGet = true)]
        public int MediaId { get; set; }

        [BindProperty]
        public MediaViewModel MediaInput { get; set; } = new MediaViewModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var mediaToUpdate = await _mediaService.GetMediaByIdAsync(MediaId);
            if (mediaToUpdate == null)
            {
                return NotFound();
            }

            MediaInput = new MediaViewModel
            {
                Name = mediaToUpdate.Name,
                Type = mediaToUpdate.Type
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var mediaToUpdate = await _mediaService.GetMediaByIdAsync(MediaId);
            if (mediaToUpdate == null)
            {
                return NotFound();
            }

            mediaToUpdate.Name = MediaInput.Name;
            mediaToUpdate.Type = MediaInput.Type;

            var updatedMedia = await _mediaService.UpdateMediaAsync(MediaId, mediaToUpdate);
            if (updatedMedia == null)
            {
                ModelState.AddModelError("", "Unable to update media.");
                return Page();
            }

            return RedirectToPage("./Details/", new { mediaId = MediaId });
        }
    }
}
