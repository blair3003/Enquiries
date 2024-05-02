using Enquiries.Models;
using Enquiries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enquiries.Pages.Reporters
{
    public class DetailsModel : PageModel
    {
        private readonly ReporterService _reporterService;

        public DetailsModel(ReporterService reporterService)
        {
            _reporterService = reporterService;
        }

        [BindProperty(SupportsGet = true)]
        public int ReporterId { get; set; }
        public Reporter? Reporter { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Reporter = await _reporterService.GetReporterByIdAsync(ReporterId);

            if (Reporter == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
