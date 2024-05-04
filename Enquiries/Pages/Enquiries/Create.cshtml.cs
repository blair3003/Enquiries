using Enquiries.Models;
using MediaModel = Enquiries.Models.Media;
using Enquiries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enquiries.Pages.Enquiries
{
    public class CreateModel : PageModel
    {
        private readonly EnquiryService _enquiryService;
        private readonly MediaService _mediaService;
        private readonly ReporterService _reporterService;

        public CreateModel(EnquiryService enquiryService, MediaService mediaService, ReporterService reporterService)
        {
            _enquiryService = enquiryService;
            _mediaService = mediaService;
            _reporterService = reporterService;
        }

        [BindProperty]
        public EnquiryViewModel EnquiryInput { get; set; } = new EnquiryViewModel();
        public List<MediaModel> Media { get; set; } = [];
        public List<Reporter> Reporters { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
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

            var createdEnquiry = await _enquiryService.AddEnquiryAsync(
                new Enquiry
                {
                    Subject = EnquiryInput.Subject,
                    Description = EnquiryInput.Description,
                    Deadline = EnquiryInput.Deadline,
                    IsArchived = EnquiryInput.IsArchived,
                    MediaId = EnquiryInput.MediaId,
                    ReporterId = EnquiryInput.ReporterId
                }
            );

            if (createdEnquiry == null)
            {
                return BadRequest("Unable to create Enquiry.");
            }

            return RedirectToPage("./Details/", new { enquiryId = createdEnquiry.EnquiryId });
        }
    }
}
