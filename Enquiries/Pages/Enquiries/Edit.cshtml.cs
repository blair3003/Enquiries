using Enquiries.Models;
using MediaModel = Enquiries.Models.Media;
using Enquiries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enquiries.Pages.Enquiries
{
    public class EditModel : PageModel
    {
        private readonly EnquiryService _enquiryService;
        private readonly MediaService _mediaService;
        private readonly ReporterService _reporterService;

        public EditModel(EnquiryService enquiryService, MediaService mediaService, ReporterService reporterService)
        {
            _enquiryService = enquiryService;
            _mediaService = mediaService;
            _reporterService = reporterService;
        }

        [BindProperty(SupportsGet = true)]
        public int EnquiryId { get; set; }
        [BindProperty]
        public EnquiryViewModel EnquiryInput { get; set; } = new EnquiryViewModel();
        public List<MediaModel> Media { get; set; } = [];
        public List<Reporter> Reporters { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            var enquiryToUpdate = await _enquiryService.GetEnquiryByIdAsync(EnquiryId);
            if (enquiryToUpdate == null)
            {
                return NotFound();
            }

            EnquiryInput = new EnquiryViewModel
            {
                Subject = enquiryToUpdate.Subject,
                Description = enquiryToUpdate.Description,
                Deadline = enquiryToUpdate.Deadline,
                IsArchived = enquiryToUpdate.IsArchived,
                MediaId = enquiryToUpdate.MediaId,
                ReporterId = enquiryToUpdate.ReporterId
            };

            Media = await _mediaService.GetAllMediaAsync();
            Reporters = await _reporterService.GetAllReportersAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var enquiryToUpdate = await _enquiryService.GetEnquiryByIdAsync(EnquiryId);
            if (enquiryToUpdate == null)
            {
                return NotFound();
            }

            enquiryToUpdate.Subject = EnquiryInput.Subject;
            enquiryToUpdate.Description = EnquiryInput.Description;
            enquiryToUpdate.Deadline = EnquiryInput.Deadline;
            enquiryToUpdate.IsArchived = EnquiryInput.IsArchived;
            enquiryToUpdate.MediaId = EnquiryInput.MediaId;
            enquiryToUpdate.ReporterId = EnquiryInput.ReporterId;

            var updatedEnquiry = await _enquiryService.UpdateEnquiryAsync(EnquiryId, enquiryToUpdate);
            if (updatedEnquiry == null)
            {
                ModelState.AddModelError("", "Unable to update Enquiry.");
                return Page();
            }

            return RedirectToPage("./Details/", new { enquiryId = EnquiryId });
        }
    }
}
