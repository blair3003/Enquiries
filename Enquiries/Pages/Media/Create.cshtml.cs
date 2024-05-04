using MediaModel = Enquiries.Models.Media;
using Enquiries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enquiries.Pages.Media
{
    public class CreateModel : PageModel
    {
        private readonly MediaService _mediaService;

        public CreateModel(MediaService mediaService)
        {
            _mediaService = mediaService;

        }

        [BindProperty]
        public MediaViewModel MediaInput { get; set; } = new MediaViewModel();

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var createdMedia = await _mediaService.AddMediaAsync(
                new MediaModel
                {
                    Name = MediaInput.Name,
                    Type = MediaInput.Type
                }
            );

            if (createdMedia == null)
            {
                return BadRequest("Unable to create Media.");
            }

            return RedirectToPage("./Details/", new { mediaId = createdMedia.MediaId });
        }
    }
}
