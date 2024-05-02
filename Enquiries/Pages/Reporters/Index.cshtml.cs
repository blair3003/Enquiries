using Enquiries.Models;
using Enquiries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enquiries.Pages.Reporters
{
    public class IndexModel : PageModel
    {
        private readonly ReporterService _reporterService;

        public IndexModel(ReporterService reporterService)
        {
            _reporterService = reporterService;
        }

        public List<Reporter> Reporters { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            Reporters = await _reporterService.GetAllReportersAsync();

            return Page();
        }
    }
}
