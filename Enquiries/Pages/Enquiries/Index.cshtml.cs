using Enquiries.Models;
using Enquiries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enquiries.Pages.Enquiries
{
    public class IndexModel : PageModel
    {
        private readonly EnquiryService _enquiryService;

        public IndexModel(EnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }

        public List<Enquiry> Enquiries { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            Enquiries = await _enquiryService.GetAllEnquiriesAsync();

            return Page();
        }
    }
}
