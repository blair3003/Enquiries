﻿using Enquiries.Models;
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

        public async Task<Reporter?> GetReporterByIdAsync(int reporterId)
        {
            return await _context.Reporters.FindAsync(reporterId);
        }

        public async Task<Reporter?> AddReporterAsync(Reporter newReporter)
        {
            try
            {
                await _context.Reporters.AddAsync(newReporter);
                await _context.SaveChangesAsync();
                return newReporter;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Reporter?> UpdateReporterAsync(int reporterId, Reporter updatedReporter)
        {
            try
            {
                var reporter = await _context.Reporters.FindAsync(reporterId) ?? throw new ArgumentException("Reporter not found.");

                reporter.Name = updatedReporter.Name;
                reporter.MediaId = updatedReporter.MediaId;
                reporter.Email = updatedReporter.Email;
                reporter.Tel = updatedReporter.Tel;
                reporter.Mobile = updatedReporter.Mobile;
                reporter.IsActive = updatedReporter.IsActive;

                await _context.SaveChangesAsync();
                return reporter;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
