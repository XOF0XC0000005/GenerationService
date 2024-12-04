using GenerationService.Models;
using FileResult = GenerationService.Models.FileResult;

namespace GenerationService.Interfaces
{
    public interface IFileService
    {
        Task<FileResult> GenerateExcelFileAsync(ExcelDataRequest request);
        Task<List<FileMeta>> GetFileAsync();
        Task<bool> DeleteFileAsync(Guid id);
    }
}
