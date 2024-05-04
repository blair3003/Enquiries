using Enquiries.Services;
using MediaModel = Enquiries.Models.Media;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enquiries.Pages.Reporters
{
    public class EditModel : PageModel
    {
        private readonly MediaService _mediaService;
        private readonly ReporterService _reporterService;

        public EditModel(MediaService mediaService, ReporterService reporterService)
        {
            _mediaService = mediaService;
            _reporterService = reporterService;
        }

        [BindProperty(SupportsGet = true)]
        public int ReporterId { get; set; }
        [BindProperty]
        public ReporterViewModel ReporterInput { get; set; } = new ReporterViewModel();
        public List<MediaModel> Media { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            var reporterToUpdate = await _reporterService.GetReporterByIdAsync(ReporterId);
            if (reporterToUpdate == null)
            {
                return NotFound();
            }

            ReporterInput = new ReporterViewModel
            {
                Name = reporterToUpdate.Name,
                Email = reporterToUpdate.Email,
                Tel = reporterToUpdate.Tel,
                Mobile = reporterToUpdate.Mobile,
                IsActive = reporterToUpdate.IsActive,
                MediaId = reporterToUpdate.MediaId
            };

            Media = await _mediaService.GetAllMediaAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var reporterToUpdate = await _reporterService.GetReporterByIdAsync(ReporterId);
            if (reporterToUpdate == null)
            {
                return NotFound();
            }

            reporterToUpdate.Name = ReporterInput.Name;
            reporterToUpdate.Email = ReporterInput.Email;
            reporterToUpdate.Tel = ReporterInput.Tel;
            reporterToUpdate.Mobile = ReporterInput.Mobile;
            reporterToUpdate.IsActive = ReporterInput.IsActive;
            reporterToUpdate.MediaId = ReporterInput.MediaId;

            var updatedReporter = await _reporterService.UpdateReporterAsync(ReporterId, reporterToUpdate);
            if (updatedReporter == null)
            {
                ModelState.AddModelError("", "Unable to update Reporter.");
                return Page();
            }

            return RedirectToPage("./Details/", new { reporterId = ReporterId });
        }
    }
}
