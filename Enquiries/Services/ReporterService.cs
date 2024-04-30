using Enquiries.Models;
using Microsoft.EntityFrameworkCore;

namespace Enquiries.Services
{
    public class ReporterService
    {
        private readonly AppDbContext _context;

        public ReporterService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Reporter>> GetAllReportersAsync()
        {
            return await _context.Reporters.ToListAsync();
        }

        public async Task<Reporter> GetReporterByIdAsync(int reporterId)
        {
            return null;
        }

        public async Task<Reporter> AddReporterAsync(Reporter reporter)
        {
            return null;
        }

        public async Task<Reporter> UpdateReporterAsync(int reporterId, Reporter updatedReporter)
        {
            return null;
        }

    }
}
