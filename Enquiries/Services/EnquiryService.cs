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

        public async Task<Enquiry?> GetEnquiryByIdAsync(int enquiryId)
        {
            return await _context.Enquiries.FindAsync(enquiryId);
        }

        public async Task<Enquiry?> AddEnquiryAsync(Enquiry newEnquiry)
        {
            try
            {
                await _context.Enquiries.AddAsync(newEnquiry);
                await _context.SaveChangesAsync();
                return newEnquiry;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Enquiry?> UpdateEnquiryAsync(int enquiryId, Enquiry updatedEnquiry)
        {
            try
            {
                var enquiry = await _context.Enquiries.FindAsync(enquiryId) ?? throw new ArgumentException("Enquiry not found.");

                enquiry.MediaId = updatedEnquiry.MediaId;
                enquiry.ReporterId = updatedEnquiry.ReporterId;
                enquiry.Subject = updatedEnquiry.Subject;
                enquiry.Description = updatedEnquiry.Description;
                enquiry.Deadline = updatedEnquiry.Deadline;
                enquiry.IsArchived = updatedEnquiry.IsArchived;
                enquiry.UpdatedOn = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return enquiry;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
