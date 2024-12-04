using GenerationService.Models;

namespace GenerationService.Interfaces
{
    public interface IFileRepository
    {
        Task SaveFileMetaAsync(FileMeta fileMeta);
        Task<List<FileMeta>> GetAllFileAsync();
        Task<FileMeta?> GetFileByIdAsync(Guid id);
        Task DeleteFileAsync(Guid id);
    }
}
