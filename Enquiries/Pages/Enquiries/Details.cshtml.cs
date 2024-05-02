using Enquiries.Models;
using Enquiries.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Enquiries.Pages.Enquiries
{
    public class DetailsModel : PageModel
    {
        private readonly EnquiryService _enquiryService;

        public DetailsModel(EnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }

        [BindProperty(SupportsGet = true)]
        public int EnquiryId { get; set; }
        public Enquiry? Enquiry { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Enquiry = await _enquiryService.GetEnquiryByIdAsync(EnquiryId);

            if (Enquiry == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
