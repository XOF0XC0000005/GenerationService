
using GenerationService.Interfaces;
using GenerationService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenerationService.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateExcel([FromBody] ExcelDataRequest request)
        {
            var result = await _fileService.GenerateExcelFileAsync(request);
            
            if (result.IsSuccess)
            {
                return Ok(new {status = "success", path = result.FilePath});
            }

            return BadRequest(new { status = "error", message = result.ErrorMessage });
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetFileList()
        {
            var files = await _fileService.GetFileAsync();

            if (files == null || !files.Any())
            {
                return NotFound("No files found.");
            }

            return Ok(files);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            var result = await _fileService.DeleteFileAsync(id);

            if(result)
            {
                return Ok(new {staus = "success" });
            }

            return NotFound(new { status = "error", message = "File not found" });
        }
    }
}
