
using GenerationService.Interfaces;
using GenerationService.Models;
using Microsoft.AspNetCore.Mvc;
using FileResult = GenerationService.Models.FileResult;

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
                return Ok(new FileResult { IsSuccess = true, FilePath = result.FilePath});
            }

            return BadRequest(new FileResult { IsSuccess = false, ErrorMessage = result.ErrorMessage });
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
                return Ok("Success");
            }

            return NotFound("File not found");
        }
    }
}
