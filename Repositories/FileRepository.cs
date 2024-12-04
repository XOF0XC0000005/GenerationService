using GenerationService.Interfaces;
using GenerationService.Models;
using GenerationService.Persisntence;
using Microsoft.EntityFrameworkCore;

namespace GenerationService.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteFileAsync(Guid id)
        {
            var file = await GetFileByIdAsync(id);

            if (file != null)
            {
                _context.FileMetas.Remove(file);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<FileMeta>> GetAllFileAsync()
        {
            return await _context.FileMetas.ToListAsync();
        }

        public async Task<FileMeta?> GetFileByIdAsync(Guid id)
        {
            return await _context.FileMetas.FindAsync(id);
        }

        public async Task SaveFileMetaAsync(FileMeta fileMeta)
        {
            _context.FileMetas.Add(fileMeta);
            await _context.SaveChangesAsync();
        }
    }
}
