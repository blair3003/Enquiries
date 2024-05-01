using Enquiries.Models;
using Microsoft.EntityFrameworkCore;

namespace Enquiries.Services
{
    public class MediaService
    {
        private readonly AppDbContext _context;

        public MediaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Media>> GetAllMediaAsync()
        {
            return await _context.Media.ToListAsync();
        }

        public async Task<Media?> GetMediaByIdAsync(int mediaId)
        {
            return await _context.Media.FindAsync(mediaId);
        }

        public async Task<Media?> AddMediaAsync(Media newMedia)
        {
            try
            {
                await _context.Media.AddAsync(newMedia);
                await _context.SaveChangesAsync();
                return newMedia;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Media?> UpdateMediaAsync(int mediaId, Media updatedMedia)
        {
            try
            {
                var media = await _context.Media.FindAsync(mediaId) ?? throw new ArgumentException("Media not found.");

                media.Name = updatedMedia.Name;
                media.Type = updatedMedia.Type;

                await _context.SaveChangesAsync();
                return media;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
