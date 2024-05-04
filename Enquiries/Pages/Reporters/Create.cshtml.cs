using Enquiries.Models;
using MediaModel = Enquiries.Models.Media;
using Enquiries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enquiries.Pages.Reporters
{
    public class CreateModel : PageModel
    {
        private readonly MediaService _mediaService;
        private readonly ReporterService _reporterService;

        public CreateModel(MediaService mediaService, ReporterService reporterService)
        {
            _mediaService = mediaService;
            _reporterService = reporterService;

        }

        [BindProperty]
        public ReporterViewModel ReporterInput { get; set; } = new ReporterViewModel();
        public List<MediaModel> Media { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            Media = await _mediaService.GetAllMediaAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var createdReporter = await _reporterService.AddReporterAsync(
                new Reporter
                {
                    Name = ReporterInput.Name,
                    Email = ReporterInput.Email,
                    Tel = ReporterInput.Tel,
                    Mobile = ReporterInput.Mobile,
                    IsActive = ReporterInput.IsActive,
                    MediaId = ReporterInput.MediaId
                }
            );

            if (createdReporter == null)
            {
                return BadRequest("Unable to create Reporter.");
            }

            return RedirectToPage("./Details/", new { reporterId = createdReporter.ReporterId });
        }
    }
}
