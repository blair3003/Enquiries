using Enquiries.Models;
using Microsoft.EntityFrameworkCore;

namespace Enquiries.Services
{
    public class EnquiryService
    {
        private readonly AppDbContext _context;

        public EnquiryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Enquiry>> GetAllEnquiriesAsync()
        {
            return await _context.Enquiries.ToListAsync();
        }

        public async Task<Enquiry> GetEnquiryByIdAsync(int enquiryId)
        {
            return null;
        }

        public async Task<Enquiry> AddEnquiryAsync(Enquiry enquiry)
        {
            return null;
        }

        public async Task<Enquiry> UpdateEnquiryAsync(int enquiryId, Enquiry updatedEnquiry)
        {
            return null;
        }


    }
}
