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

        public async Task<Media> GetMediaByIdAsync(int mediaId)
        {
            return null;
        }

        public async Task<Media> AddMediaAsync(Media media)
        {
            return null;
        }

        public async Task<Media> UpdateMediaAsync(int mediaId, Media updatedMedia)
        {
            return null;
        }
    }
}
