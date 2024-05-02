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
            //return await _context.Media.ToListAsync();

            var data = new List<Media>
            {
                new() { MediaId = 1, Name = "Media 1" },
                new() { MediaId = 2, Name = "Media 2" },
                new() { MediaId = 3, Name = "Media 3" }
            };

            return data;
        }

        public async Task<Media?> GetMediaByIdAsync(int mediaId)
        {
            //return await _context.Media.FindAsync(mediaId);

            var data = new List<Media>
            {
                new() { MediaId = 1, Name = "Media 1" },
                new() { MediaId = 2, Name = "Media 2" },
                new() { MediaId = 3, Name = "Media 3" }
            };

            return data.FirstOrDefault(m => m.MediaId == mediaId);
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
